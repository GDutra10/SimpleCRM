using SimpleCRM.Domain.Common.Enums;

namespace SimpleCRM.Application.Admin.Contracts.DTOs;

public class UserRegisterRQ : BaseRQ
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string Name { get; set; } = default!;
    public Role Role { get; set; }
}