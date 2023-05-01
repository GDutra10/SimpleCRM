namespace SimpleCRM.Application.Attendant.Contracts.DTOs;

public class OrderItemDeleteRQ : BaseRQ
{
    public Guid OrderItemId { get; set; } = default!;
    public Guid InteractionId { get; set; } = default!;
}