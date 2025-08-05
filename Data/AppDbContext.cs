using CodeSmellsDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeSmellsDemo.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseInMemoryDatabase("CodeSmellsDemo");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(oi => oi.OrderId);

            // Seed some test data
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, Name = "Ahmed Ali", Email = "ahmed@test.com", RegistrationDate = DateTime.Now.AddYears(-2), TotalSpent = 5000m, OrderCount = 25, MembershipLevel = "Premium" },
                new Customer { Id = 2, Name = "Sara Mohammed", Email = "sara@test.com", RegistrationDate = DateTime.Now.AddYears(-1), TotalSpent = 2000m, OrderCount = 15, MembershipLevel = "Regular" },
                new Customer { Id = 3, Name = "Omar Hassan", Email = "omar@test.com", RegistrationDate = DateTime.Now.AddMonths(-6), TotalSpent = 800m, OrderCount = 8, MembershipLevel = "Regular" }
            );

            modelBuilder.Entity<Order>().HasData(
                new Order { Id = 1, CustomerName = "Ahmed Ali", CreatedDate = DateTime.Now.AddDays(-30), Total = 500m },
                new Order { Id = 2, CustomerName = "Sara Mohammed", CreatedDate = DateTime.Now.AddDays(-25), Total = 300m },
                new Order { Id = 3, CustomerName = "Omar Hassan", CreatedDate = DateTime.Now.AddDays(-20), Total = 150m },
                new Order { Id = 4, CustomerName = "Ahmed Ali", CreatedDate = DateTime.Now.AddDays(-15), Total = 800m },
                new Order { Id = 5, CustomerName = "Sara Mohammed", CreatedDate = DateTime.Now.AddDays(-10), Total = 250m }
            );

            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem { Id = 1, ProductName = "Laptop", Price = 1000m, Quantity = 1, OrderId = 1 },
                new OrderItem { Id = 2, ProductName = "Mouse", Price = 25m, Quantity = 2, OrderId = 1 },
                new OrderItem { Id = 3, ProductName = "Keyboard", Price = 75m, Quantity = 1, OrderId = 1 },
                new OrderItem { Id = 4, ProductName = "Monitor", Price = 300m, Quantity = 1, OrderId = 2 },
                new OrderItem { Id = 5, ProductName = "Headphones", Price = 150m, Quantity = 1, OrderId = 3 },
                new OrderItem { Id = 6, ProductName = "Tablet", Price = 500m, Quantity = 1, OrderId = 4 },
                new OrderItem { Id = 7, ProductName = "Phone Case", Price = 25m, Quantity = 3, OrderId = 4 },
                new OrderItem { Id = 8, ProductName = "Charger", Price = 30m, Quantity = 2, OrderId = 5 }
            );
        }
    }
}
