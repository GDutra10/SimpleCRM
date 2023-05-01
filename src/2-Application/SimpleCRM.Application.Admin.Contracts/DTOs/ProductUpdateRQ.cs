namespace SimpleCRM.Application.Admin.Contracts.DTOs;

public class ProductUpdateRQ : BaseRQ
{
    public Guid ProductId { get; set; } = default!;
    public string Name { get; set; } = default!;
    public bool Active { get; set; } = default!;
}