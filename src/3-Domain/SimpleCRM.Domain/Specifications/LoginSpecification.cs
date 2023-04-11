using System.Linq.Expressions;
using SimpleCRM.Domain.Entities;

namespace SimpleCRM.Domain.Specifications;

public class LoginSpecification : ISpecification<User>
{
    private readonly string _email;
    private readonly string _password;
    
    public LoginSpecification(string email, string password)
    {
        _email = email;
        _password = password;
    }
    
    public Expression<Func<User, bool>> ToExpression()
    {
        return user => user.Email.ToLower().Equals(_email.ToLower()) &&
                       user.Password.Equals(_password);
    }
}