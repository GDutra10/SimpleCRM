namespace SimpleCRM.Application.Common.Contracts.DTOs;

public class ProductRS : BaseRS
{
    public Guid Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public bool Active { get; set; } = default!;
}