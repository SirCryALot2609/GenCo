namespace GenCo.Application.Exceptions;


public class BusinessRuleValidationException : Exception
{
    public string Code { get; }

    public BusinessRuleValidationException(string message, string code = "BUSINESS_RULE_VIOLATION")
        : base(message)
    {
        Code = code;
    }
}