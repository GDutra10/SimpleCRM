using SimpleCRM.Domain.Common.Models;
using SimpleCRM.Domain.Common.Providers;

namespace SimpleCRM.Domain.Common.Repositories;

public class Repository<T> : IRepository<T> where T : IDbRecord
{
    protected readonly IDbProvider<T> DbProvider;

    protected Repository(IDbProvider<T> dbProvider)
    {
        DbProvider = dbProvider;
    }

    public async Task SaveAsync(T record, CancellationToken cancellationToken)
    {
        await DbProvider.SaveAsync(record, cancellationToken);
    }
}