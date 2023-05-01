namespace SimpleCRM.Application.Attendant.Contracts.DTOs;

public class OrderItemAddRQ : BaseRQ
{
    public Guid ProductId { get; set; } = default!;
    public Guid InteractionId { get; set; } = default!;
}