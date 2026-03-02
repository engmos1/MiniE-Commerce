using System.ComponentModel.DataAnnotations;

namespace ECommerce.Web.Models;

public class CreateProductDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
    public decimal Price { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Available quantity must be 0 or more.")]
    public int AvailableQuantity { get; set; }
}
