using CodeSmellsDemo.Data;
using CodeSmellsDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmellsDemo.GodClassSmell
{
    public class GodClassSmell
    {
        private readonly AppDbContext _context;

        public GodClassSmell(AppDbContext context)
        {
            _context = context;
        }

        public void CreateAndNotifyOrder(string customerName, List<OrderItem> items)
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

            // يرسل إشعار إلى العميل (جزء ليس من مسؤولية الطلب)
            //Console.WriteLine($"Order number {order.Id} has been created for the customer {customerName} and notified via SMS");

        }
    }
}
