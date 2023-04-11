using SimpleCRM.Domain.Specifications;

namespace SimpleCRM.Domain.Contracts.Repositories;

public interface IRepository<T> where T : IDbRecord
{
    Task SaveAsync(T record, CancellationToken cancellationToken);
    Task<List<T>> GetAllAsync(ISpecification<T> specification);
}