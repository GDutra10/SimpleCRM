using SimpleCRM.Domain.Contracts;

namespace SimpleCRM.Domain.Entities;

public class Interaction : Record, IDbRecord
{
    public User User { get; set; }
    public Customer Customer { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public Interaction(User user, Customer customer)
    {
        User = user;
        Customer = customer;
    }
}