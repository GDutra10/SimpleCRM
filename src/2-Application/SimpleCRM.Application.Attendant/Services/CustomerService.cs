using AutoMapper;
using Microsoft.Extensions.Logging;
using SimpleCRM.Application.Attendant.Contracts;
using SimpleCRM.Application.Attendant.Contracts.DTOs;
using SimpleCRM.Application.Attendant.Contracts.Services;
using SimpleCRM.Domain.Contracts.Repositories;
using SimpleCRM.Domain.Entities;
using SimpleCRM.Domain.Managers;

namespace SimpleCRM.Application.Attendant.Services;

public class CustomerService : BaseService, ICustomerService
{
    private readonly IRepository<Customer> _customerRepository;
    private readonly CustomerManager _customerManager;

    public CustomerService(
        ILogger<CustomerService> logger, 
        IMapper mapper,
        IRepository<Customer> customerRepository,
        CustomerManager customerManager,
        TokenManager tokenManager, 
        UserManager userManager) 
        : base(logger, mapper, tokenManager, userManager)
    {
        _customerRepository = customerRepository;
        _customerManager = customerManager;
    }
    
    public async Task<CustomerRS> RegisterCustomerAsync(string token, CustomerRegisterRQ customerRegisterRQ, CancellationToken cancellationToken)
    {
        var user = await GetUserByToken(token, cancellationToken);
        var customer = await _customerManager.CreateCustomerAsync(
            customerRegisterRQ.Name ?? string.Empty, 
            customerRegisterRQ.Email ?? string.Empty,
            customerRegisterRQ.Telephone ?? string.Empty,
            user.Id,
            cancellationToken);

        await _customerRepository.SaveAsync(customer, cancellationToken);

        return Mapper.Map<Customer, CustomerRS>(customer);
    }

    // TODO: pagination
    public async Task<CustomerSearchRS> SearchCustomerAsync(CustomerSearchRQ customerSearchRQ,
        CancellationToken cancellationToken)
    {
        var customers = await _customerManager.SearchCustomerAsync(
            customerSearchRQ.Name ?? string.Empty,
            customerSearchRQ.Email ?? string.Empty,
            customerSearchRQ.Telephone ?? string.Empty,
            cancellationToken);

        var list = Mapper.Map<List<Customer>, List<CustomerRS>>(customers);

        return new CustomerSearchRS { Customers = list };
    }
}