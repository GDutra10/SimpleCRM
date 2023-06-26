using System.Linq.Expressions;
using SimpleCRM.Domain.Common.Enums;
using SimpleCRM.Domain.Entities;

namespace SimpleCRM.Domain.Specifications;

public class InteractionCustomerInAttendanceSpecification : ISpecification<Interaction>
{
    private readonly Guid _customerId;
    
    public InteractionCustomerInAttendanceSpecification(Guid customerId)
    {
        _customerId = customerId;
    }

    public Expression<Func<Interaction, bool>> ToExpression()
    {
        return i => i.CustomerId == _customerId && i.InteractionState == InteractionState.InAttendance;
    }
}