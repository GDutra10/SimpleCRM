using SimpleCRM.Application.Attendant.Contracts.DTOs;

namespace SimpleCRM.Application.Attendant.Contracts.Services;

public interface ICustomerService
{
    Task<CustomerRS> RegisterCustomerAsync(string accessToken, CustomerRegisterRQ customerRegisterRQ, CancellationToken cancellationToken);
    Task<CustomerSearchRS> SearchCustomerAsync(CustomerSearchRQ customerSearchRQ, CancellationToken cancellationToken);
}