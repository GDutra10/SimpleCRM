using System.Linq.Expressions;
using SimpleCRM.Domain.Entities;

namespace SimpleCRM.Domain.Specifications;

public class ProductSearchSpecification : ISpecification<Product>
{
    private readonly bool _onlyActive;

    public ProductSearchSpecification(bool onlyActive)
    {
        _onlyActive = onlyActive;
    }

    public Expression<Func<Product, bool>> ToExpression()
    {
        if (_onlyActive)
            return p => p.Active == true;

        return p => true;
    }
}