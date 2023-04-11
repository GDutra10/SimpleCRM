using SimpleCRM.Domain.Common.Models;

namespace SimpleCRM.Domain.Common.Repositories;

public interface IRepository<T> where T : IDbRecord
{
    Task SaveAsync(T record, CancellationToken cancellationToken);
}