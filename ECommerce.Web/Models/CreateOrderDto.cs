using System.ComponentModel.DataAnnotations;

namespace ECommerce.Web.Models;

public class CreateOrderDto
{
    [Required]
    public string CustomerName { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string CustomerEmail { get; set; } = null!;

    [Required]
    [MinLength(1, ErrorMessage = "Order must have at least one item.")]
    public List<CreateOrderItemDto> Items { get; set; } = new();
}

public class CreateOrderItemDto
{
    [Required]
    public Guid ProductId { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
    public int Quantity { get; set; }
}
