namespace SimpleCRM.Application.Attendant.Contracts.DTOs;

public class CustomerRS : BaseRS
{
    public Guid Id { get; set; }
    public DateTime CreationTime { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Telephone { get; set; } = default!;
}