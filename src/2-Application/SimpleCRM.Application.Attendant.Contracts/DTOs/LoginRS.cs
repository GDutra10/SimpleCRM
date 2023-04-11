namespace SimpleCRM.Application.Attendant.Contracts.DTOs;

public class LoginRS : BaseRS
{
    public string AccessToken { get; set; } = default!;
}