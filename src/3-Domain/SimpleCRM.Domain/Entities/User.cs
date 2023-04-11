using SimpleCRM.Domain.Common.Enums;
using SimpleCRM.Domain.Contracts;

namespace SimpleCRM.Domain.Entities;

public class User : Record, IDbRecord
{
    public string Name { get; internal set; } = default!;
    public string Email { get; internal set; } = default!;
    public string Password { get; internal set; } = default!;
    public Role Role { get; internal set; }
    
    private User(){ }

    internal User(string name, string email, string password, Role role)
    {
        this.Name = name;
        this.Email = email;
        this.Password = password;
        this.Role = role;
    }
}