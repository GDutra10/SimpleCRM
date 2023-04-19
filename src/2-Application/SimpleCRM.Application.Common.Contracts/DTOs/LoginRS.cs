namespace SimpleCRM.Application.Common.Contracts.DTOs;

public class LoginRS : BaseRS
{
    public string AccessToken { get; set; } = default!;
    public int ExpiresIn { get; set; }
}