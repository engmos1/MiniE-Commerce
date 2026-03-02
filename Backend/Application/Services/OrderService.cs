using Application.DTOs;
using Application.Interfaces.RepoInterfaces;
using Application.Interfaces.ServiceInterfaces;
using Domain.Entities;

namespace Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }

    public async Task<OrderDto> CreateOrderAsync(CreateOrderDto dto)
    {
        var order = new Order
        {
            CustomerName = dto.CustomerName,
            CustomerEmail = dto.CustomerEmail,
            Items = new List<OrderItem>()
        };

        foreach (var itemDto in dto.Items)
        {
            var product = await _productRepository.GetByIdAsync(itemDto.ProductId)
                ?? throw new InvalidOperationException($"Product with ID {itemDto.ProductId} not found.");

            if (product.AvailableQuantity < itemDto.Quantity)
                throw new InvalidOperationException(
                    $"Insufficient stock for product '{product.Name}'. Available: {product.AvailableQuantity}, Requested: {itemDto.Quantity}");

            product.AvailableQuantity -= itemDto.Quantity;
            await _productRepository.UpdateAsync(product);

            var lineTotal = product.Price * itemDto.Quantity;

            order.Items.Add(new OrderItem
            {
                ProductId = product.Id,
                Quantity = itemDto.Quantity,
                UnitPrice = product.Price,
                LineTotal = lineTotal
            });
        }

        order.TotalItems = order.Items.Sum(i => i.Quantity);
        order.SubTotal = order.Items.Sum(i => i.LineTotal);

        order.DiscountPercentage = order.TotalItems >= 5 ? 10m :
                                   order.TotalItems >= 2 ? 5m : 0m;

        order.DiscountAmount = Math.Round(order.SubTotal * (order.DiscountPercentage / 100m), 2);
        order.FinalTotal = order.SubTotal - order.DiscountAmount;

        await _orderRepository.AddAsync(order);
        await _orderRepository.SaveChangesAsync();

        return MapToDto(order);
    }

    public async Task<OrderDto?> GetOrderAsync(Guid id)
    {
        var order = await _orderRepository.GetByIdWithItemsAsync(id);
        return order is null ? null : MapToDto(order);
    }

    private static OrderDto MapToDto(Order order) => new()
    {
        Id = order.Id,
        CustomerName = order.CustomerName,
        CustomerEmail = order.CustomerEmail,
        CreatedAt = order.CreatedAt,
        TotalItems = order.TotalItems,
        SubTotal = order.SubTotal,
        DiscountPercentage = order.DiscountPercentage,
        DiscountAmount = order.DiscountAmount,
        FinalTotal = order.FinalTotal,
        Items = order.Items.Select(i => new OrderItemDto
        {
            Id = i.Id,
            ProductId = i.ProductId,
            ProductName = i.Product?.Name ?? string.Empty,
            Quantity = i.Quantity,
            UnitPrice = i.UnitPrice,
            LineTotal = i.LineTotal
        }).ToList()
    };
}
