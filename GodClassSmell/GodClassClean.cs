using CodeSmellsDemo.Data;
using CodeSmellsDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CodeSmellsDemo.GodClassSmell
{
    public class GodClassClean
    {
        private readonly AppDbContext _context;

        public GodClassClean(AppDbContext context)
        {
            _context = context;
        }

        public int CreateOrder(string customerName, List<OrderItem> items)
        {

            var order = new Order
            {
                CustomerName = customerName,
                CreatedDate = DateTime.Now,
                Total = items.Sum(i => i.Price * i.Quantity),
                Items = items
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            return order.Id;
        }
    }

    public class NotificationService
    {
        public void SendOrderNotification(string customerName, int orderId)
        {
            //Console.WriteLine($"Order number {orderId} has been created for the customer {customerName} and notified via SMS");
        }
    }
}
