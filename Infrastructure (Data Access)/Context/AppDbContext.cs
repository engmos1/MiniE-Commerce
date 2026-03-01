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

        public override int SaveChanges()
        {
            GenerateIdsForNewEntities();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            GenerateIdsForNewEntities();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void GenerateIdsForNewEntities()
        {
            var newEntities = ChangeTracker.Entries<BaseEntity>()
                .Where(e => e.State == EntityState.Added && e.Entity.Id == Guid.Empty);

            foreach (var entity in newEntities)
            {
                entity.Entity.Id = Guid.NewGuid();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure ID for all entities inheriting from BaseEntity
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .Property("Id")
                        .ValueGeneratedOnAdd();
                }
            }

            modelBuilder.Entity<Product>(b =>
            {
                b.Property(p => p.Name).IsRequired().HasMaxLength(100);
                b.Property(p => p.Price).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<Order>(b =>
            {
                b.Property(o => o.SubTotal).HasColumnType("decimal(18,2)");
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
