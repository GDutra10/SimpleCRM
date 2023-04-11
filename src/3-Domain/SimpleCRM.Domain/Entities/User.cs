using SimpleCRM.Domain.Contracts;

namespace SimpleCRM.Domain.Entities;

public class User : Record, IDbRecord
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    
    private User(){ }

    internal User(string name, string email, string password)
    {
        this.Name = name;
        this.Email = email;
        this.Password = password;
    }
}