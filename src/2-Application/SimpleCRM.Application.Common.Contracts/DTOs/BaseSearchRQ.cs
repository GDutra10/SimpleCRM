namespace SimpleCRM.Application.Common.Contracts.DTOs;

public abstract class BaseSearchRQ : BaseRQ
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}