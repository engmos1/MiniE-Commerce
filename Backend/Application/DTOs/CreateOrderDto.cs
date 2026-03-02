using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

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


