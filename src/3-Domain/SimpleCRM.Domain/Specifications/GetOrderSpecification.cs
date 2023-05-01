using System.Linq.Expressions;
using SimpleCRM.Domain.Entities;

namespace SimpleCRM.Domain.Specifications;

public class GetOrderSpecification : ISpecification<Order>
{
    private readonly Guid _interactionId;
    
    public GetOrderSpecification(Guid interactionId)
    {
        _interactionId = interactionId;
    }
    
    public Expression<Func<Order, bool>> ToExpression()
    {
        return o => o.InteractionId == _interactionId;
    }
}