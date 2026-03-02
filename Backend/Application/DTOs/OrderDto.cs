namespace Application.DTOs;

public class OrderDto
{
    public Guid Id { get; set; }
    public string CustomerName { get; set; } = null!;
    public string CustomerEmail { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public int TotalItems { get; set; }
    public decimal SubTotal { get; set; }
    public decimal DiscountPercentage { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal FinalTotal { get; set; }
    public List<OrderItemDto> Items { get; set; } = new();
}
