namespace SimpleCRM.Application.Attendant.Contracts.DTOs;

public class InteractionStartRQ : BaseRQ
{
    public Guid CustomerId { get; set; }
}