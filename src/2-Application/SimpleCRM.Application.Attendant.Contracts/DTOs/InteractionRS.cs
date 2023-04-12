namespace SimpleCRM.Application.Attendant.Contracts.DTOs;

public class InteractionRS : BaseRS
{
    public Guid UserId { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime EndDate { get; set; }
    public UserRS User { get; set; } = default!;
    public CustomerRS Customer { get; set; } = default!;
}