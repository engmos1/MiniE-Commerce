using Application.DTOs;
using Application.Interfaces.RepoInterfaces;
using Application.Interfaces.ServiceInterfaces;
using Domain.Entities;

namespace Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductDto> CreateProductAsync(CreateProductDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Price = dto.Price,
            AvailableQuantity = dto.AvailableQuantity
        };

        await _productRepository.AddAsync(product);
        await _productRepository.SaveChangesAsync();

        return MapToDto(product);
    }

    public async Task<PaginatedResult<ProductDto>> GetProductsAsync(int page, int pageSize)
    {
        var (items, totalCount) = await _productRepository.GetPaginatedAsync(page, pageSize);

        return new PaginatedResult<ProductDto>
        {
            Items = items.Select(MapToDto).ToList(),
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }

    private static ProductDto MapToDto(Product product) => new()
    {
        Id = product.Id,
        Name = product.Name,
        Price = product.Price,
        AvailableQuantity = product.AvailableQuantity
    };
}
