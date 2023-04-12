using System.Linq.Expressions;
using SimpleCRM.Domain.Entities;

namespace SimpleCRM.Domain.Specifications;

public class CustomerSearchSpecification : ISpecification<Customer>
{
    private readonly string _name;
    private readonly string _email;
    private readonly string _telephone;
    
    public CustomerSearchSpecification(string name, string email, string telephone)
    {
        _name = name;
        _email = email;
        _telephone = telephone;
    }
    
    public Expression<Func<Customer, bool>> ToExpression()
    {
        return c =>
            c.Name.Contains(_name) &&
            c.Email.Contains(_email) &&
            c.Telephone.Contains(_telephone);
    }
}