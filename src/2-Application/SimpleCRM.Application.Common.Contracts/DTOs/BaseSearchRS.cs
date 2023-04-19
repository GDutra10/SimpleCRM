namespace SimpleCRM.Application.Common.Contracts.DTOs;

public abstract class BaseSearchRS<T> : BaseRS where T : class
{
    public List<T> Records { get; set; } = default!;
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public long TotalRecords { get; set; }
}