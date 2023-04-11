using System.Linq.Expressions;
using SimpleCRM.Domain.Entities;

namespace SimpleCRM.Domain.Specifications;

public class UserWithEmailSpecification : ISpecification<User>
{
    protected readonly string Email;
    
    public UserWithEmailSpecification(string email)
    {
        Email = email;
    }
    
    public Expression<Func<User, bool>> ToExpression()
    {
        return u => u.Email == Email;
    }
}