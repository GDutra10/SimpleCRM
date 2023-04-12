using AutoMapper;
using Microsoft.Extensions.Logging;
using SimpleCRM.Application.Attendant.Contracts;
using SimpleCRM.Application.Attendant.Contracts.DTOs;
using SimpleCRM.Domain.Contracts.Repositories;
using SimpleCRM.Domain.Entities;
using SimpleCRM.Domain.Managers;

namespace SimpleCRM.Application.Attendant.Services;

public class CustomerService : ICustomerService
{
    private readonly ILogger<ICustomerService> _logger;
    private readonly IMapper _mapper;
    private readonly IRepository<Customer> _customerRepository;
    private readonly CustomerManager _customerManager;
    private readonly TokenManager _tokenManager;
    private readonly UserManager _userManager;

    public CustomerService(
        ILogger<CustomerService> logger, 
        IMapper mapper,
        IRepository<Customer> customerRepository,
        CustomerManager customerManager,
        TokenManager tokenManager, 
        UserManager userManager)
    {
        _logger = logger;
        _mapper = mapper;
        _customerRepository = customerRepository;
        _customerManager = customerManager;
        _tokenManager = tokenManager;
        _userManager = userManager;
    }
    
    public async Task<CustomerRS> RegisterCustomerAsync(string token, CustomerRegisterRQ customerRegisterRQ, CancellationToken cancellationToken)
    {
        var userId = _tokenManager.GetId(token);
        var user = await _userManager.GetUserAsync(userId, cancellationToken);
        var customer = await _customerManager.CreateCustomerAsync(
            customerRegisterRQ.Name ?? string.Empty, 
            customerRegisterRQ.Email ?? string.Empty,
            customerRegisterRQ.Telephone ?? string.Empty,
            user.Id,
            cancellationToken);

        await _customerRepository.SaveAsync(customer, cancellationToken);

        return _mapper.Map<Customer, CustomerRS>(customer);
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

        var list = _mapper.Map<List<Customer>, List<CustomerRS>>(customers);

        return new CustomerSearchRS { Customers = list };
    }
}