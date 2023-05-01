using AutoMapper;
using Microsoft.Extensions.Logging;
using SimpleCRM.Application.Common.Contracts.Services;
using SimpleCRM.Domain.Contracts.Repositories;
using SimpleCRM.Domain.Entities;
using SimpleCRM.Domain.Managers;
using SimpleCRM.Domain.Specifications;

namespace SimpleCRM.Application.Common.Services;

public class ProductBaseService : BaseService, IProductBaseService
{
    private readonly IRepository<Product> _productRepository;
    private readonly ProductManager _productManager;

    public ProductBaseService(
        ILogger<ProductBaseService> logger, 
        IMapper mapper,
        TokenManager tokenManager, 
        UserManager userManager,
        IRepository<Product> productRepository,
        ProductManager productManager
        ) : base (logger, mapper, tokenManager, userManager)
    {
        _productRepository = productRepository;
        _productManager = productManager;
    }
    
    public async Task<ProductSearchRS> SearchProductsAsync(ProductSearchRQ productSearchRQ, CancellationToken cancellationToken)
    {
        var onlyActive = productSearchRQ.OnlyActive;
        var products = await _productManager.GetProductAsync(onlyActive, productSearchRQ.PageNumber, productSearchRQ.PageSize, cancellationToken);
        var totalRecord = await _productRepository.CountAsync(new ProductSearchSpecification(onlyActive), cancellationToken);
        var hasData = totalRecord > 0;
        var list = Mapper.Map<List<Product>, List<ProductRS>>(products);
        var totalPages = hasData ? Math.Ceiling((double)totalRecord / (double)productSearchRQ.PageSize) : 0;
        
        if (totalPages == 0)
            totalPages++;

        return new ProductSearchRS
        {
            Records = list,
            PageSize = productSearchRQ.PageSize,
            CurrentPage = productSearchRQ.PageNumber,
            TotalPages = (int)totalPages,
            TotalRecords = totalRecord
        };
    }
}