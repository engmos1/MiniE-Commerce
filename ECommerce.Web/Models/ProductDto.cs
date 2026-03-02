namespace ECommerce.Web.Models;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public int AvailableQuantity { get; set; }
}
