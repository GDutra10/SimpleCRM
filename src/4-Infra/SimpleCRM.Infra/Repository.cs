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

    public async Task<T?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return await DbProvider.GetAsync(id, cancellationToken);
    }
    
    public async Task SaveAsync(T record, CancellationToken cancellationToken)
    {
        await DbProvider.SaveAsync(record, cancellationToken);
    }

    public async Task<List<T>> GetAllAsync(ISpecification<T> specification, CancellationToken cancellationToken) 
        => await DbProvider.GetAllAsync(specification, cancellationToken);
}