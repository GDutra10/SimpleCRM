using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SimpleCRM.Domain.Common.Models;

public abstract class Record
{
    [BsonId()]
    public ObjectId Id { get; set; }
    
    public DateTime RegisterDate { get; set; }
}