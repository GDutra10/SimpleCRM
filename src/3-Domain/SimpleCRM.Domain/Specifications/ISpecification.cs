using System.Linq.Expressions;

namespace SimpleCRM.Domain.Specifications;

public interface ISpecification<T>
{
    Expression<Func<T, bool>> ToExpression();
}