using SimpleCRM.Domain.Common.Enums;

namespace SimpleCRM.Application.Attendant.Contracts.DTOs;

public class InsertUserRQ : BaseRQ
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string Name { get; set; } = default!;
    public Role Role { get; set; }
}