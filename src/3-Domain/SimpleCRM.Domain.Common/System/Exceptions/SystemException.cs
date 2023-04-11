using System.Globalization;

namespace SimpleCRM.Domain.Common.System.Exceptions;

public abstract class SystemException : Exception
{
    public string Key { get; private set; }
    
    protected SystemException(string message) : base(message)
    {
        Key = "";
    }
    
    protected SystemException(string key, string message) : base(message)
    {
        Key = key;
    }

    protected SystemException(string message, params object[] args) 
        : base(String.Format(CultureInfo.CurrentCulture, message, args))
    {
        Key = "";
    }
    
    protected SystemException(string key, string message, params object[] args) 
        : base(String.Format(CultureInfo.CurrentCulture, message, args))
    {
        Key = key;
    }
    
}