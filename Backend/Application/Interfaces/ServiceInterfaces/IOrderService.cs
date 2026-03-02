using Application.DTOs;

namespace Application.Interfaces.ServiceInterfaces;

public interface IOrderService
{
    Task<OrderDto> CreateOrderAsync(CreateOrderDto dto);
    Task<OrderDto?> GetOrderAsync(Guid id);
}
