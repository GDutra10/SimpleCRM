using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using SimpleCRM.Domain.Entities;
using SimpleCRM.Domain.Providers;

namespace SimpleCRM.Infra.MongoDB;

public class MongoDbMapper : IDbMapper
{
    public void Map()
    {
        BsonClassMap.RegisterClassMap<Record>(classMap => {
            classMap.SetIsRootClass(true);
            classMap.AutoMap();
            classMap.MapProperty(u => u.Id).SetIdGenerator(GuidGenerator.Instance);
        });
        
        BsonClassMap.RegisterClassMap<User>(classMap =>
        {
            classMap.AutoMap();
        });
    }
}