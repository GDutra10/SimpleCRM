using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using SimpleCRM.Domain.Common.Enums;
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
            classMap.GetMemberMap(u => u.Role).SetSerializer(new EnumSerializer<Role>(BsonType.String));
        });

        BsonClassMap.RegisterClassMap<Customer>(classMap =>
        {
            classMap.AutoMap();
        });
        
        BsonClassMap.RegisterClassMap<Interaction>(classMap =>
        {
            classMap.AutoMap();
            classMap.GetMemberMap(u => u.InteractionState).SetSerializer(new EnumSerializer<InteractionState>(BsonType.String));
        });
        
        BsonClassMap.RegisterClassMap<Product>(classMap =>
        {
            classMap.AutoMap();
        });
        
        BsonClassMap.RegisterClassMap<Order>(classMap =>
        {
            classMap.AutoMap();
        });
        
        BsonClassMap.RegisterClassMap<OrderItem>(classMap =>
        {
            classMap.AutoMap();
        });
    }
}