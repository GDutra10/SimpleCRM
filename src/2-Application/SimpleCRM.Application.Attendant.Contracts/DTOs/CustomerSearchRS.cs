namespace SimpleCRM.Application.Attendant.Contracts.DTOs;

public class CustomerSearchRS : BaseRS
{
    public List<CustomerRS>? Customers { get; set; }
}