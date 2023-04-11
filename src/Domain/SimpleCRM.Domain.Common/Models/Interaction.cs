using MongoDB.Bson.Serialization.Attributes;
using SimpleCRM.Domain.Common.Models;

namespace SimpleCRM.Domain.Common.Models;

public class Interaction : Record, IDbRecord
{
    [BsonRequired]
    public User User { get; set; }
    
    [BsonRequired]
    public Customer Customer { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public Interaction(User user, Customer customer)
    {
        User = user;
        Customer = customer;
    }
}