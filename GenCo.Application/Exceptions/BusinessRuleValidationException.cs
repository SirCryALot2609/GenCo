namespace GenCo.Application.Exceptions;


public class BusinessRuleValidationException(string message, string code = "BUSINESS_RULE_VIOLATION")
    : Exception(message)
{
    public string Code { get; } = code;
}