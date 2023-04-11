using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using SimpleCRM.Domain.Contracts;
using SimpleCRM.Domain.Providers;
using SimpleCRM.Domain.Specifications;

namespace SimpleCRM.Infra.MongoDB;

public class MongoDbProvider<T> : IDbProvider<T> where T : IDbRecord
{
    private readonly IMongoCollection<T> _collection;

    public MongoDbProvider(IConfiguration configuration)
    {
        var mongoUrl = MongoUrl.Create(configuration.GetConnectionString(ProviderConstants.ConnectionString));
        var client = new MongoClient(mongoUrl);
        _collection = client.GetDatabase(ProviderConstants.DataBase).GetCollection<T>(typeof(T).Name);
    }

    public async Task SaveAsync(T record, CancellationToken cancellationToken)
    {
        if (record.Id == Guid.Empty)
            record.Id = Guid.NewGuid();
        
        record.CreationTime = DateTime.Now;
        var filter = Builders<T>.Filter.Eq("_id", record.Id);
        await _collection.ReplaceOneAsync(filter, record, new ReplaceOptions() { IsUpsert = true }, cancellationToken);
    }

    public async Task<List<T>> GetAllAsync(ISpecification<T> specification)
    {
        var result = await _collection.FindAsync(specification.ToExpression());
        
        return result.ToList();
    }
}