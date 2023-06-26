using System.Linq.Expressions;
using SimpleCRM.Domain.Common.Enums;
using SimpleCRM.Domain.Entities;

namespace SimpleCRM.Domain.Specifications;

public class InteractionUserInAttendanceSpecification : ISpecification<Interaction>
{
    private readonly User _user;
    
    public InteractionUserInAttendanceSpecification(User user)
    {
        _user = user;
    }
    
    public Expression<Func<Interaction, bool>> ToExpression()
    {
        return i => i.UserId == _user.Id && i.InteractionState == InteractionState.InAttendance;
    }
}