using Application.DTOs;

namespace Application.Interfaces.ServiceInterfaces;

public interface IProductService
{
    Task<ProductDto> CreateProductAsync(CreateProductDto dto);
    Task<PaginatedResult<ProductDto>> GetProductsAsync(int page, int pageSize);
}
