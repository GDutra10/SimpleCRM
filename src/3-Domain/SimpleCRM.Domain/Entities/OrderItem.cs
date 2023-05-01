using SimpleCRM.Domain.Contracts;

namespace SimpleCRM.Domain.Entities;

public class OrderItem : Record, IDbRecord
{
    public Guid OrderId { get; internal set; } = default!;
    
    public Product Product { get; internal set; } = default!;
    public Guid ProductId { get; internal set; } = default!;
    
    private OrderItem(){ }

    internal OrderItem(Guid orderId, Product product)
    {
        this.OrderId = orderId;
        this.Product = product;
        this.ProductId = product.Id;
    }
}