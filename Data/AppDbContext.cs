using CodeSmellsDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeSmellsDemo.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseInMemoryDatabase("CodeSmellsDemo");
    }
}
