namespace SimpleCRM.Domain.Entities;

public abstract class Record
{
    public Guid Id { get; set; }
    
    public DateTime CreationTime { get; set; }
}