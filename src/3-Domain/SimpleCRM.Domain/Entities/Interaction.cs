using SimpleCRM.Domain.Common.Enums;
using SimpleCRM.Domain.Common.System.Exceptions;
using SimpleCRM.Domain.Contracts;

namespace SimpleCRM.Domain.Entities;

public class Interaction : Record, IDbRecord
{
    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;
    public Guid CustomerId { get; private set; }
    public Customer Customer { get; private set; } = null!;

    public DateTime EndTime { get; internal set; }
    
    public InteractionState InteractionState { get; internal set; }
    
    private Interaction(){ }
    
    internal Interaction(User user, Customer customer)
    {
        if (user.Id == Guid.Empty)
            throw new ArgumentException("Invalid user");
        
        if (customer.Id == Guid.Empty)
            throw new ArgumentException("Invalid customer");
        
        UserId = user.Id;
        User = user;
        CustomerId = customer.Id;
        Customer = customer;
        InteractionState = InteractionState.InAttendance;
    }
}