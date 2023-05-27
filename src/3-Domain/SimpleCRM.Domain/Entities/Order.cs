using SimpleCRM.Domain.Common.Enums;
using SimpleCRM.Domain.Contracts;

namespace SimpleCRM.Domain.Entities;

public class Order : Record, IDbRecord
{
    public Guid InteractionId { get; internal set; } = default!;
    
    public List<OrderItem> OrderItems { get; internal set; } = default!;

    public OrderState OrderState { get; set; } = OrderState.PreConfirmed;
    
    private Order(){ }

    internal Order(Interaction interaction)
    {
        this.InteractionId = interaction.Id;
        this.OrderItems = new List<OrderItem>();
    }
}