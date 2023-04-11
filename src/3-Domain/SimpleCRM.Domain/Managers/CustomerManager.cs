using SimpleCRM.Domain.Entities;

namespace SimpleCRM.Domain.Managers;

public class CustomerManager
{
    public async Task<Customer> CreateCustomerAsync(string name, string email, string telephone, Guid userId, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new Customer(name, email, telephone, userId));
    }
}