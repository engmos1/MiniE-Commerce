using System.Net.Http.Json;
using ECommerce.Web.Models;

namespace ECommerce.Web.Services;

public class ProductApiClient
{
    private readonly HttpClient _http;

    public ProductApiClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<PaginatedResult<ProductDto>?> GetProductsAsync(int page = 1, int pageSize = 10)
    {
        return await _http.GetFromJsonAsync<PaginatedResult<ProductDto>>(
            $"api/Products/GetAll?page={page}&pageSize={pageSize}");
    }

    public async Task<ProductDto?> CreateProductAsync(CreateProductDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/Products/Create", dto);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ProductDto>();
    }
}
