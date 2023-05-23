using SimpleCRM.Domain.Common.System.Exceptions;
using SimpleCRM.Domain.Contracts.Repositories;
using SimpleCRM.Domain.Entities;
using SimpleCRM.Domain.Specifications;

namespace SimpleCRM.Domain.Managers;

public class ProductManager
{
    private readonly IRepository<Product> _productRepository;

    public ProductManager(IRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product> CreateProductAsync(string name, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new Product(name));
    }

    public async Task<Product> UpdateProductAsync(Guid id, string name, bool active, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetAsync(id, cancellationToken);

        if (product is null)
            throw new NotFoundException();

        product.Name = name;
        product.Active = active;

        return product;
    }

    public async Task<List<Product>> GetProductsAsync(bool onlyActive, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        return await _productRepository.GetAllAsync(new ProductSearchSpecification(onlyActive), pageNumber, pageSize, cancellationToken);
    }
}