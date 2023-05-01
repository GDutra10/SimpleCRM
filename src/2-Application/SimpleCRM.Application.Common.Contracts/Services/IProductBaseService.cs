using SimpleCRM.Application.Common.Contracts.DTOs;

namespace SimpleCRM.Application.Common.Contracts.Services;

public interface IProductBaseService
{
    Task<ProductSearchRS> SearchProductsAsync(ProductSearchRQ productSearchRQ, CancellationToken cancellationToken);
}