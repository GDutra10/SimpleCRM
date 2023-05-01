using SimpleCRM.Domain.Contracts;
using SimpleCRM.Domain.Contracts.Repositories;
using SimpleCRM.Domain.Providers;
using SimpleCRM.Domain.Specifications;

namespace SimpleCRM.Infra;

public class Repository<T> : IRepository<T> where T : IDbRecord
{
    protected readonly IDbProvider<T> DbProvider;

    public Repository(IDbProvider<T> dbProvider)
    {
        DbProvider = dbProvider;
    }

    public async Task DeleteAsync(T record, CancellationToken cancellationToken)
    {
        await DbProvider.DeleteAsync(record, cancellationToken);
    }

    public async Task SaveAsync(T record, CancellationToken cancellationToken)
    {
        await DbProvider.SaveAsync(record, cancellationToken);
    }
    
    public async Task<T?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return await DbProvider.GetAsync(id, cancellationToken);
    }

    public async Task<List<T>> GetAllAsync(ISpecification<T> specification, CancellationToken cancellationToken) 
        => await DbProvider.GetAllAsync(specification, cancellationToken);

    public async Task<List<T>> GetAllAsync(ISpecification<T> specification, int pageNumber, int pageSize, CancellationToken cancellationToken) 
        => await DbProvider.GetAllAsync(specification, pageNumber > 0 ? pageNumber - 1 : pageNumber, pageSize, cancellationToken);
    
    public async Task<long> CountAsync(ISpecification<T> specification, CancellationToken cancellationToken) 
        => await DbProvider.CountAsync(specification, cancellationToken);
}