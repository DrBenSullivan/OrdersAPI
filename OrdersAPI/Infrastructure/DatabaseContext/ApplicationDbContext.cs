#pragma warning disable 1591

using Microsoft.EntityFrameworkCore;
using OrdersAPI.Core.Models;

namespace OrdersAPI.Infrastructure.DatabaseContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public ApplicationDbContext()
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            if (!modelBuilder.Entity<Order>().Metadata.GetSeedData().Any())
            {
                Guid seedOrderId = Guid.NewGuid();
                var seedOrderItemId = Guid.NewGuid();

                var seedOrder = new Order()
                {
                    OrderId = seedOrderId,
                    OrderNumber = "SEED_ORDER_2024_1",
                    CustomerName = "John Smith",
                    OrderDate = new DateTime(2024, 9, 26),
                    TotalPrice = 40.00m
                };

                var seedOrderItem = new OrderItem()
                {
                    OrderItemId = seedOrderItemId,
                    OrderId = seedOrderId,
                    ProductName = "Hammer",
                    Quantity = 4,
                    UnitPrice = 10.00m,
                    TotalPrice = 40.00m
                };

                modelBuilder.Entity<Order>().HasData(seedOrder);
                modelBuilder.Entity<OrderItem>().HasData(seedOrderItem);
            }
        }
    }
}
