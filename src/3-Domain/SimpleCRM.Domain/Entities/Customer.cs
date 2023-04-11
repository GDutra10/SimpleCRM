using SimpleCRM.Domain.Contracts;

namespace SimpleCRM.Domain.Entities;

public class Customer : Record, IDbRecord
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Telephone { get; set; } = default!;
    
    private Customer(){ }

    public Customer(string name, string email, string telephone)
    {
        this.Name = name;
        this.Email = email;
        this.Telephone = telephone;
    }
}