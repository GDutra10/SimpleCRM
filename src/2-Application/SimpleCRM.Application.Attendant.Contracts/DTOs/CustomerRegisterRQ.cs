using SimpleCRM.Application.Common.Contracts.DTOs;

namespace SimpleCRM.Application.Attendant.Contracts.DTOs;

public class CustomerRegisterRQ : BaseRQ
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Telephone { get; set; }
}