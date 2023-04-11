using SimpleCRM.Domain.Common.Models;

namespace SimpleCRM.Domain.Common.Models;

public class User : Record, IDbRecord
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}