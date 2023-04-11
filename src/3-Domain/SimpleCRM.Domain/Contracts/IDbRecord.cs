namespace SimpleCRM.Domain.Contracts;

public interface IDbRecord
{
    public Guid Id { get; set; }
    
    public DateTime CreationTime { get; set; }
}