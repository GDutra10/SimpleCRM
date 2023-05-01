namespace SimpleCRM.Application.Admin.Contracts.DTOs;

public class ProductRegisterRQ : BaseRQ
{
    public string Name { get; set; } = default!;
}