using SimpleCRM.Domain.Contracts;

namespace SimpleCRM.Domain.Entities;

public class Product : Record, IDbRecord
{
    public string Name { get; internal set; } = default!;
    public bool Active { get; internal set; } = default!;
    
    private Product(){ }

    internal Product(string name)
    {
        this.Name = name;
        this.Active = true;
    }
}