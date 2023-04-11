namespace SimpleCRM.Domain.Common.System.Exceptions;

public class NotFoundException : SystemException
{
    public NotFoundException() : base("") {}
    
    public NotFoundException(string message) : base(message) {}

    public NotFoundException(string key, string message) : base(key, message) { }

    public NotFoundException(string message, params object[] args) : base(message, args) {}

    public NotFoundException(string key, string message, params object[] args) : base(key, message, args) { }
}