namespace SimpleCRM.Application.Common.Contracts.DTOs;

public class OrderRS : BaseRS
{
    public Guid Id { get; set; } = default!;
    public DateTime CreationTime { get; set; }
    public Guid InteractionId { get; set; } = default!;
    public List<OrderItemRS> OrderItems { get; set; } = default!;
}