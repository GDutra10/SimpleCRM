using AutoMapper;
using Microsoft.Extensions.Logging;
using SimpleCRM.Application.Admin.Contracts.DTOs;
using SimpleCRM.Application.Admin.Contracts.Services;
using SimpleCRM.Application.Common.Contracts.DTOs;
using SimpleCRM.Application.Common.Services;
using SimpleCRM.Domain.Contracts.Repositories;
using SimpleCRM.Domain.Entities;
using SimpleCRM.Domain.Managers;

namespace SimpleCRM.Application.Admin.Services;

public class AdminProductService : BaseService, IAdminProductService
{
    private readonly ProductManager _productManager;
    private readonly IRepository<Product> _productRepository;

    public AdminProductService(
        ILogger<AdminProductService> logger, 
        IMapper mapper, 
        TokenManager tokenManager, 
        UserManager userManager,
        ProductManager productManager,
        IRepository<Product> productRepository) 
        : base(logger, mapper, tokenManager, userManager)
    {
        _productManager = productManager;
        _productRepository = productRepository;
    }
    
    public async Task<ProductRS> RegisterProductAsync(ProductRegisterRQ productRegisterRQ, CancellationToken cancellationToken)
    {
        var product = await _productManager.CreateProductAsync(productRegisterRQ.Name, cancellationToken);

        await _productRepository.SaveAsync(product, cancellationToken);
        var productRS = Mapper.Map<Product, ProductRS>(product);
        
        return productRS;
    }

    public async Task<ProductRS> UpdateProductAsync(ProductUpdateRQ productUpdateRQ, CancellationToken cancellationToken)
    {
        var product = await _productManager.UpdateProductAsync(productUpdateRQ.ProductId, productUpdateRQ.Name,
            productUpdateRQ.Active, cancellationToken);

        await _productRepository.SaveAsync(product, cancellationToken);
        var productRS = Mapper.Map<Product, ProductRS>(product);
        
        return productRS;
    }
}