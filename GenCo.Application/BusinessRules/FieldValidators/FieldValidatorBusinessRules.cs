using System.Text.Json;
using System.Text.RegularExpressions;
using GenCo.Application.Exceptions;
using GenCo.Application.Persistence.Contracts.Common;
using GenCo.Application.Specifications.Fields;
using GenCo.Application.Specifications.FieldValidators;
using GenCo.Domain.Entities;
using GenCo.Domain.Enum;

namespace GenCo.Application.BusinessRules.FieldValidators;

public class FieldValidatorBusinessRules(
    IGenericRepository<Field> fieldRepository,
    IGenericRepository<FieldValidator> validatorRepository)
    : IFieldValidatorBusinessRules
{
    public async Task EnsureFieldExistsAsync(Guid fieldId, CancellationToken cancellationToken)
    {
        var spec = new FieldByIdSpec(fieldId);
        if (!await fieldRepository.ExistsAsync(spec, cancellationToken))
            throw new BusinessRuleValidationException(
                $"Field with Id {fieldId} does not exist.",
                "FIELD_NOT_FOUND");
    }

    public async Task EnsureValidatorExistsAsync(Guid validatorId, CancellationToken cancellationToken)
    {
        var spec = new FieldValidatorByIdSpec(validatorId);
        if (!await validatorRepository.ExistsAsync(spec, cancellationToken))
            throw new BusinessRuleValidationException(
                $"Validator with Id {validatorId} does not exist.",
                "VALIDATOR_NOT_FOUND");
    }

    public async Task EnsureValidatorBelongsToFieldAsync(Guid validatorId, Guid fieldId, CancellationToken cancellationToken)
    {
        var spec = new FieldValidatorByIdSpec(validatorId);
        var validator = await validatorRepository.FirstOrDefaultAsync(spec, cancellationToken: cancellationToken);

        if (validator is null)
            throw new BusinessRuleValidationException(
                $"Validator with Id {validatorId} not found.",
                "VALIDATOR_NOT_FOUND");

        if (validator.FieldId != fieldId)
            throw new BusinessRuleValidationException(
                $"Validator {validatorId} does not belong to Field {fieldId}.",
                "VALIDATOR_FIELD_MISMATCH");
    }

    public Task EnsureValidatorTypeValidAsync(string type)
    {
        if (string.IsNullOrWhiteSpace(type))
            throw new BusinessRuleValidationException(
                "Validator type cannot be null or empty.",
                "VALIDATOR_TYPE_REQUIRED");

        if (!Enum.TryParse(typeof(ValidatorType), type, true, out var parsed) || !Enum.IsDefined(typeof(ValidatorType), parsed))
            throw new BusinessRuleValidationException(
                $"Validator type '{type}' is not valid.",
                "VALIDATOR_TYPE_INVALID");

        return Task.CompletedTask;
    }

    public async Task EnsureValidatorConfigValidAsync(FieldValidator validator)
    {
        ArgumentNullException.ThrowIfNull(validator);

        // Ép enum -> string để dùng chung hàm validate type
        await EnsureValidatorTypeValidAsync(validator.Type.ToString()).ConfigureAwait(false);

        var parsedType = Enum.Parse<ValidatorType>(validator.Type.ToString(), true);

        if (string.IsNullOrWhiteSpace(validator.ConfigJson))
            return;

        try
        {
            var config = JsonSerializer.Deserialize<FieldValidatorConfig>(validator.ConfigJson);

            switch (config)
            {
                case null:
                    throw new BusinessRuleValidationException(
                        $"Invalid configuration JSON for validator {validator.Id}.",
                        "VALIDATOR_CONFIG_DESERIALIZATION_FAILED");

                case { Min: not null, Max: not null } when config.Min > config.Max:
                    throw new BusinessRuleValidationException(
                        "Validator config has invalid range: Min cannot be greater than Max.",
                        "VALIDATOR_CONFIG_RANGE_INVALID");

                case { MinLength: not null, MaxLength: not null } when config.MinLength > config.MaxLength:
                    throw new BusinessRuleValidationException(
                        "Validator config has invalid length range: MinLength cannot be greater than MaxLength.",
                        "VALIDATOR_CONFIG_LENGTH_RANGE_INVALID");
            }

            if (!string.IsNullOrWhiteSpace(config.RegexPattern) && config.RegexPattern.Length > 512)
                throw new BusinessRuleValidationException(
                    "Validator regex pattern is too long (max 512 chars).",
                    "VALIDATOR_PATTERN_TOO_LONG");

            if (!string.IsNullOrWhiteSpace(config.CustomMessage) && config.CustomMessage.Length > 512)
                throw new BusinessRuleValidationException(
                    "Validator custom message is too long (max 512 chars).",
                    "VALIDATOR_MESSAGE_TOO_LONG");

            // ✅ Validate logic theo từng loại validator
            switch (parsedType)
            {
                case ValidatorType.Unknown:
                    throw new BusinessRuleValidationException(
                        "Validator type 'Unknown' is not allowed.",
                        "VALIDATOR_TYPE_UNKNOWN");

                case ValidatorType.Required:
                    // Required không cần config thêm
                    break;

                case ValidatorType.Email:
                    // Email không có config đặc biệt nhưng có thể hỗ trợ custom regex
                    if (!string.IsNullOrWhiteSpace(config.RegexPattern) &&
                        !RegexIsValid(config.RegexPattern))
                    {
                        throw new BusinessRuleValidationException(
                            "Email validator regex pattern is invalid.",
                            "VALIDATOR_EMAIL_REGEX_INVALID");
                    }
                    break;

                case ValidatorType.MinLength:
                    if (config.MinLength is null || config.MinLength < 0)
                        throw new BusinessRuleValidationException(
                            "MinLength validator must define a non-negative MinLength.",
                            "VALIDATOR_MINLENGTH_REQUIRED");
                    break;

                case ValidatorType.MaxLength:
                    if (config.MaxLength is null || config.MaxLength <= 0)
                        throw new BusinessRuleValidationException(
                            "MaxLength validator must define a positive MaxLength.",
                            "VALIDATOR_MAXLENGTH_REQUIRED");
                    break;

                case ValidatorType.Range:
                    if (config.Min is null || config.Max is null)
                        throw new BusinessRuleValidationException(
                            "Range validator must define both Min and Max.",
                            "VALIDATOR_CONFIG_RANGE_REQUIRED");
                    if (config.Min > config.Max)
                        throw new BusinessRuleValidationException(
                            "Range validator Min cannot be greater than Max.",
                            "VALIDATOR_CONFIG_RANGE_INVALID");
                    break;

                case ValidatorType.Regex:
                    if (string.IsNullOrWhiteSpace(config.RegexPattern))
                        throw new BusinessRuleValidationException(
                            "Regex validator must have a valid RegexPattern.",
                            "VALIDATOR_CONFIG_PATTERN_REQUIRED");
                    if (!RegexIsValid(config.RegexPattern))
                        throw new BusinessRuleValidationException(
                            "Regex validator pattern is not a valid regular expression.",
                            "VALIDATOR_REGEX_INVALID");
                    break;

                case ValidatorType.Custom:
                    // Custom validator cần có logic cụ thể hoặc script định nghĩa
                    if (string.IsNullOrWhiteSpace(config.CustomMessage))
                        throw new BusinessRuleValidationException(
                            "Custom validator must include a CustomScript definition.",
                            "VALIDATOR_CUSTOM_SCRIPT_REQUIRED");
                    if (config.CustomMessage.Length > 2000)
                        throw new BusinessRuleValidationException(
                            "Custom validator script is too long (max 2000 chars).",
                            "VALIDATOR_CUSTOM_SCRIPT_TOO_LONG");
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(parsedType), parsedType, "Unsupported validator type.");
            }
        }
        catch (JsonException ex)
        {
            throw new BusinessRuleValidationException(
                $"Validator configuration JSON is invalid: {ex.Message}",
                "VALIDATOR_CONFIG_JSON_INVALID");
        }
    }

    // Helper: Kiểm tra regex hợp lệ
    private static bool RegexIsValid(string pattern)
    {
        try
        {
            _ = new Regex(pattern);
            return true;
        }
        catch
        {
            return false;
        }
    }


    public async Task EnsureValidatorUniqueOnCreateAsync(Guid fieldId, string type, CancellationToken cancellationToken)
    {
        if (!Enum.TryParse(typeof(ValidatorType), type, true, out var parsed) || !Enum.IsDefined(typeof(ValidatorType), parsed))
            throw new BusinessRuleValidationException(
                $"Validator type '{type}' is not valid.",
                "VALIDATOR_TYPE_INVALID");

        var validatorType = (ValidatorType)parsed;

        var spec = new FieldValidatorByFieldAndTypeSpec(fieldId, validatorType);
        if (await validatorRepository.ExistsAsync(spec, cancellationToken))
            throw new BusinessRuleValidationException(
                $"Validator of type '{type}' already exists for Field {fieldId}.",
                "VALIDATOR_DUPLICATE_ON_CREATE");
    }

    public async Task EnsureValidatorUniqueOnUpdateAsync(
        Guid fieldId,
        Guid validatorId,
        string type,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(type))
            throw new BusinessRuleValidationException(
                "Validator type cannot be null or empty.",
                "VALIDATOR_TYPE_REQUIRED");

        if (!Enum.TryParse<ValidatorType>(type, true, out var parsedType) 
            || !Enum.IsDefined(typeof(ValidatorType), parsedType))
            throw new BusinessRuleValidationException(
                $"Validator type '{type}' is not valid.",
                "VALIDATOR_TYPE_INVALID");

        var spec = new FieldValidatorByFieldAndTypeSpec(fieldId, parsedType, excludeValidatorId: validatorId);

        if (await validatorRepository.ExistsAsync(spec, cancellationToken))
            throw new BusinessRuleValidationException(
                $"Another validator of type '{parsedType}' already exists for Field {fieldId}.",
                "VALIDATOR_DUPLICATE_ON_UPDATE");
    }


    public async Task EnsureCanDeleteAsync(Guid validatorId, CancellationToken cancellationToken)
    {
        var spec = new FieldValidatorByIdSpec(validatorId, includeField: true);
        var validator = await validatorRepository.FirstOrDefaultAsync(spec, cancellationToken: cancellationToken);

        if (validator is null)
            throw new BusinessRuleValidationException(
                $"Validator with Id {validatorId} not found.",
                "VALIDATOR_NOT_FOUND");

        // Ví dụ logic chặn xóa nếu field đang bị dùng trong constraint hoặc bắt buộc
        if (validator.Field.IsRequired && validator.Type == ValidatorType.Required)
            throw new BusinessRuleValidationException(
                "Cannot delete required validator from a required field.",
                "VALIDATOR_REQUIRED_DELETE_FORBIDDEN");
    }
}