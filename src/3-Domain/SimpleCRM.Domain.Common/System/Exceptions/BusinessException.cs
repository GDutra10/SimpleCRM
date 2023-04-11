namespace SimpleCRM.Domain.Common.System.Exceptions;

public class BusinessException : SystemException
{
    public BusinessException(string message) : base(message) {}

    public BusinessException(string key, string message) : base(key, message) { }

    public BusinessException(string message, params object[] args) : base(message, args) {}

    public BusinessException(string key, string message, params object[] args) : base(key, message, args) { }
}