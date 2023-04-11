namespace SimpleCRM.Application.Attendant.Contracts.DTOs;

public class ErrorRS : BaseRS
{
    public string Error { get; set; }

    public ErrorRS()
    {
        Error = "Internal Server Error";
    }
    
    public ErrorRS(Exception exception)
    {
        Error = $"{exception.Message}\r\nStackTrace: {exception.StackTrace}";
    }
}