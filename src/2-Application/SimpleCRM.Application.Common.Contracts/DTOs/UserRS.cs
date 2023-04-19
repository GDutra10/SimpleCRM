using SimpleCRM.Domain.Common.Enums;

namespace SimpleCRM.Application.Common.Contracts.DTOs;

public class UserRS : BaseRS
{
    public Guid Id { get; set; }
    public DateTime CreationTime { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public Role Role { get; set; }
}