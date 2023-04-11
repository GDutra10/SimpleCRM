using MongoDB.Bson;
using SimpleCRM.Domain.Common.Models;

namespace SimpleCRM.Domain.Common.Providers;

public interface IDbProvider<T> where T : IDbRecord
{
    public Task SaveAsync(T record, CancellationToken cancellationToken);

    Task<T?> GetAsync(ObjectId id, CancellationToken cancellationToken);
}