using SimpleCRM.Application.Admin.Contracts.DTOs;

namespace SimpleCRM.Application.Admin.Contracts.Services;

public interface IAdminProductService
{
    Task<ProductRS> RegisterProductAsync(ProductRegisterRQ productRegisterRQ, CancellationToken cancellationToken);
    Task<ProductRS> UpdateProductAsync(ProductUpdateRQ productUpdateRQ, CancellationToken cancellationToken);
}