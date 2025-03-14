using AutoMapper;
using Microsoft.Extensions.Logging;
using SimpleCRM.Application.Attendant.Contracts;
using SimpleCRM.Application.Attendant.Contracts.DTOs;
using SimpleCRM.Application.Attendant.Contracts.Services;
using SimpleCRM.Application.Common.Services;
using SimpleCRM.Domain.Contracts.Repositories;
using SimpleCRM.Domain.Entities;
using SimpleCRM.Domain.Managers;
using SimpleCRM.Domain.Specifications;

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
    
    public async Task<CustomerRS> RegisterCustomerAsync(string accessToken, CustomerRegisterRQ customerRegisterRQ, CancellationToken cancellationToken)
    {
        var user = await GetUserByTokenAsync(accessToken, cancellationToken);
        var customer = await _customerManager.CreateCustomerAsync(
            customerRegisterRQ.Name ?? string.Empty, 
            customerRegisterRQ.Email ?? string.Empty,
            customerRegisterRQ.Telephone ?? string.Empty,
            user.Id,
            cancellationToken);

        await _customerRepository.SaveAsync(customer, cancellationToken);

        return Mapper.Map<Customer, CustomerRS>(customer);
    }
    
    public async Task<CustomerSearchRS> SearchCustomerAsync(CustomerSearchRQ customerSearchRQ,
        CancellationToken cancellationToken)
    {
        var name = customerSearchRQ.Name ?? string.Empty;
        var email = customerSearchRQ.Email ?? string.Empty;
        var telephone = customerSearchRQ.Telephone ?? string.Empty;
        var customers = await _customerManager.SearchCustomerAsync(
            name, email, telephone, customerSearchRQ.PageNumber, customerSearchRQ.PageSize, cancellationToken);
        var customerSearchSpecification = new CustomerSearchSpecification(name, email, telephone);
        var totalRecord = await _customerRepository.CountAsync(customerSearchSpecification, cancellationToken);
        var hasData = totalRecord > 0;
        var list = Mapper.Map<List<Customer>, List<CustomerRS>>(customers);
        var totalPages = hasData ? Math.Ceiling((double)totalRecord / (double)customerSearchRQ.PageSize) : 0;

        if (totalPages == 0)
            totalPages++;
        
        return new CustomerSearchRS
        {
            Records = list,
            PageSize = customerSearchRQ.PageSize,
            CurrentPage = customerSearchRQ.PageNumber,
            TotalPages = (int)totalPages,
            TotalRecords = totalRecord
        };
    }
}