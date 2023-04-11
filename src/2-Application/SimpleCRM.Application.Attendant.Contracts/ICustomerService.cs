using SimpleCRM.Application.Attendant.Contracts.DTOs;

namespace SimpleCRM.Application.Attendant.Contracts;

public interface ICustomerService
{
    Task<CustomerRS> CustomerRegisterAsync(string accessToken, CustomerRegisterRQ customerRegisterRQ, CancellationToken cancellationToken);
}