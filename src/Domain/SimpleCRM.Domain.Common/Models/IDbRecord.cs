using MongoDB.Bson;

namespace SimpleCRM.Domain.Common.Models;

public interface IDbRecord
{
    public ObjectId Id { get; set; }
    
    public DateTime RegisterDate { get; set; }
}