using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure__Data_Access_.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Product>(b =>
            {
                b.Property(p => p.Name).IsRequired().HasMaxLength(100);
                b.Property(p => p.Price).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<Order>(b =>
            {
                b.Property(o => o.SubTotal).HasColumnType("decimal(18,2)");
                b.Property(o => o.DiscountPercentage).HasColumnType("decimal(5,2)");
                b.Property(o => o.DiscountAmount).HasColumnType("decimal(18,2)");
                b.Property(o => o.FinalTotal).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<OrderItem>(b =>
            {
                b.Property(i => i.UnitPrice).HasColumnType("decimal(18,2)");
                b.Property(i => i.LineTotal).HasColumnType("decimal(18,2)");
            });
        }
    }

}
