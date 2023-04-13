using System.Linq.Expressions;
using SimpleCRM.Domain.Common.Enums;
using SimpleCRM.Domain.Entities;

namespace SimpleCRM.Domain.Specifications;

public class InteractionInAttendanceSpecification : ISpecification<Interaction>
{
    private Guid InteractionId;
    
    public InteractionInAttendanceSpecification(Guid interactionId)
    {
        InteractionId = interactionId;
    }

    public Expression<Func<Interaction, bool>> ToExpression()
    {
        return i => i.CustomerId == InteractionId && i.InteractionState == InteractionState.InAttendance;
    }
}