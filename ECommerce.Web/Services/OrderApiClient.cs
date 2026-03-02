using System.Net.Http.Json;
using ECommerce.Web.Models;

namespace ECommerce.Web.Services;

public class OrderApiClient
{
    private readonly HttpClient _http;

    public OrderApiClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<OrderDto?> GetOrderAsync(Guid id)
    {
        return await _http.GetFromJsonAsync<OrderDto>($"api/Orders/GetById/{id}");
    }

    public async Task<OrderDto?> CreateOrderAsync(CreateOrderDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/Orders/Create", dto);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception(error);
        }

        return await response.Content.ReadFromJsonAsync<OrderDto>();
    }
}
