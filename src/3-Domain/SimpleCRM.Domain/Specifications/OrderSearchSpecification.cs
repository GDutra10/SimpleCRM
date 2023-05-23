using System.Linq.Expressions;
using SimpleCRM.Domain.Common.Enums;
using SimpleCRM.Domain.Entities;

namespace SimpleCRM.Domain.Specifications;

public class OrderSearchSpecification: ISpecification<Order>
{
    private readonly OrderState _orderState;
    
    public OrderSearchSpecification(OrderState orderState)
    {
        _orderState = orderState;
    }
    
    public Expression<Func<Order, bool>> ToExpression()
    {
        return o => o.OrderState == _orderState;
    }
}