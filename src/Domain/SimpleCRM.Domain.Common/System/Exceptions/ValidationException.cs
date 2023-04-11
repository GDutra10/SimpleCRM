namespace SimpleCRM.Domain.Common.System.Exceptions;

public class ValidationException : SystemException
{
    public ValidationException(string message) : base(message) {}

    public ValidationException(string key, string message) : base(key, message) { }

    public ValidationException(string message, params object[] args) : base(message, args) {}

    public ValidationException(string key, string message, params object[] args) : base(key, message, args) { }
}