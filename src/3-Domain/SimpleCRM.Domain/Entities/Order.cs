using SimpleCRM.Domain.Contracts;

namespace SimpleCRM.Domain.Entities;

public class Order : Record, IDbRecord
{
    public Interaction Interaction { get; internal set; } = default!;
    public Guid InteractionId { get; internal set; } = default!;
    
    public List<OrderItem> OrderItems { get; internal set; } = default!;
    
    private Order(){ }

    internal Order(Interaction interaction)
    {
        this.Interaction = interaction;
        this.InteractionId = interaction.Id;
        this.OrderItems = new List<OrderItem>();
    }
}