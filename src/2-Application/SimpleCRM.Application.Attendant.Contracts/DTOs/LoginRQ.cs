namespace SimpleCRM.Application.Attendant.Contracts.DTOs;

public class LoginRQ : BaseRQ
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}