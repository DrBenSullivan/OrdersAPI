using Microsoft.EntityFrameworkCore;
using OrdersAPI.Core.Models;

namespace OrdersAPI.Infrastructure
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
                var orderId = Guid.NewGuid();
                var seedOrderNumber = "SEED_ORDER_2024_1";

                modelBuilder.Entity<Order>().HasData(new Order()
                {
                    OrderId = orderId,
                    OrderNumber = seedOrderNumber,
                    CustomerName = "John Smith",
                    OrderDate = DateTime.Now,
                    TotalPrice = 40.00m
                });

                modelBuilder.Entity<OrderItem>().HasData(new OrderItem()
                {
                    OrderItemId = Guid.NewGuid(),
                    OrderId = orderId,
                    ProductName = "Hammer",
                    Quantity = 4,
                    UnitPrice = 10.00m,
                    TotalPrice = 40.00m
                });
            }
        }
    }
}
