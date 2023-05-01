using SimpleCRM.Application.Common.Contracts.DTOs;

namespace SimpleCRM.Application.Common.Contracts.DTOs;

public class ProductSearchRQ : BaseSearchRQ
{
    public bool OnlyActive { get; set; }
}