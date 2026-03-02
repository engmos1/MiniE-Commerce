namespace Domain.Entities
{
    public class Order : BaseEntity
    {
        public string CustomerName { get; set; } = null!;
        public string CustomerEmail { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int TotalItems { get; set; }
        public decimal SubTotal { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal FinalTotal { get; set; }

        public List<OrderItem> Items { get; set; } = new();
    }

}
