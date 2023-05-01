namespace SimpleCRM.Application.Common.Contracts.DTOs;

public class OrderItemRS : BaseRS
{
    public Guid Id { get; set; } = default!;
    public DateTime CreationTime { get; set; }
    public Guid OrderId { get; set; } = default!;
    public Guid ProductId { get; set; } = default!;
    public ProductRS Product { get; set; } = default!;
}