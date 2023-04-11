namespace SimpleCRM.Domain.Common.DTOs;

public class InsertUserRQ : BaseRQ
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string Name { get; set; } = default!;
}