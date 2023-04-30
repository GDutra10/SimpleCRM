using SimpleCRM.Domain.Common.Enums;
using SimpleCRM.Domain.Contracts;

namespace SimpleCRM.Domain.Entities;

public class Customer : Record, IDbRecord
{
    public string Name { get; internal set; } = default!;
    public string Email { get; internal set; } = default!;
    public string Telephone { get; internal set; } = default!;
    public Guid UserId { get; internal set; }
    public InteractionState? State { get; internal set; }
    
    private Customer(){ }

    internal Customer(string name, string email, string telephone, Guid userId)
    {
        this.Name = name;
        this.Email = email;
        this.Telephone = telephone;
        this.UserId = userId;
        this.State = null;
    }
}