using SimpleCRM.Domain.Contracts;
using SimpleCRM.Domain.Specifications;

namespace SimpleCRM.Domain.Providers;

public interface IDbProvider<T> where T : IDbRecord
{
    Task SaveAsync(T record, CancellationToken cancellationToken);
    Task<List<T>> GetAllAsync(ISpecification<T> specification, CancellationToken cancellationToken);

    // Task<T?> GetAsync(Guid id, CancellationToken cancellationToken);
}