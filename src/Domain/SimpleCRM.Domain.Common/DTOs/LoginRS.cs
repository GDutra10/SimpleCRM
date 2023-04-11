namespace SimpleCRM.Domain.Common.DTOs;

public class LoginRS : BaseRS
{
    public string AccessToken { get; set; } = default!;
}