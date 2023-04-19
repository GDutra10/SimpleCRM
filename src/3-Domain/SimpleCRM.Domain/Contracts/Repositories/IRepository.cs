using SimpleCRM.Domain.Specifications;

namespace SimpleCRM.Domain.Contracts.Repositories;

public interface IRepository<T> where T : IDbRecord
{
    Task<T?> GetAsync(Guid id, CancellationToken cancellationToken);
    Task SaveAsync(T record, CancellationToken cancellationToken);
    Task<long> CountAsync(ISpecification<T> specification, CancellationToken cancellationToken);
    Task<List<T>> GetAllAsync(ISpecification<T> specification, CancellationToken cancellationToken);
    Task<List<T>> GetAllAsync(ISpecification<T> specification, int pageNumber, int pageSize, CancellationToken cancellationToken);
}