using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using SimpleCRM.Domain.Common.Models;
using SimpleCRM.Domain.Common.Providers;

namespace SimpleCRM.Infra.Repository.Providers;

public class MongoDbProvider<T> : IDbProvider<T> where T : IDbRecord
{
    private readonly IMongoCollection<T> _collection;

    public MongoDbProvider(IConfiguration configuration)
    {
        var mongoUrl = MongoUrl.Create(configuration.GetConnectionString("SimpleCRM"));
        var client = new MongoClient(mongoUrl);
        _collection = client.GetDatabase("simple-crm").GetCollection<T>(typeof(T).Name);
    }

    public async Task SaveAsync(T record, CancellationToken cancellationToken)
    {
        if (record.Id == ObjectId.Empty)
            record.Id = ObjectId.GenerateNewId();
        
        record.RegisterDate = DateTime.Now;
        var filter = Builders<T>.Filter.Eq("_id", record.Id);
        await _collection.ReplaceOneAsync(filter, record, new ReplaceOptions() { IsUpsert = true }, cancellationToken);
    }

    public async Task<T?> GetAsync(ObjectId id, CancellationToken cancellationToken)
    {
        var collections = await _collection
            .FindAsync(Builders<T>.Filter.Eq("_id", id), cancellationToken: cancellationToken); 
        
        return collections.FirstOrDefault(cancellationToken: cancellationToken);
    }

    public async Task<List<T>> GetAsync()
    {
        throw new NotImplementedException();
    }
}