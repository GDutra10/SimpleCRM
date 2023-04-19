using SimpleCRM.Domain.Contracts.Repositories;
using SimpleCRM.Domain.Entities;
using SimpleCRM.Domain.Specifications;

namespace SimpleCRM.Domain.Managers;

public class CustomerManager
{
    private readonly IRepository<Customer> _customerRepository;

    public CustomerManager(IRepository<Customer> customerRepository)
    {
        _customerRepository = customerRepository;
    }
    
    public async Task<Customer> CreateCustomerAsync(string name, string email, string telephone, Guid userId, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new Customer(name, email, telephone, userId));
    }

    public async Task<List<Customer>> SearchCustomerAsync(string name, string email, string telephone, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var customerSearchSpecification = new CustomerSearchSpecification(name, email, telephone);
        
        return await _customerRepository.GetAllAsync(customerSearchSpecification, pageNumber, pageSize, cancellationToken);
    }
}