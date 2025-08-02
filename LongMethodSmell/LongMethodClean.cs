using CodeSmellsDemo.Data;
using CodeSmellsDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmellsDemo.LongMethodSmell
{
    public class LongMethodClean
    {
        public void ProcessBigOrder()
        {
            var order = CreateOrder();
            ApplyDiscount(order);
            SaveOrder(order);
            Notify(order);
        }

        private Order CreateOrder()
        {
            var order = new Order
            {
                CustomerName = "BigCustomer",
                CreatedDate = DateTime.Now
            };

            for (int i = 0; i < 100; i++)
            {
                order.Items.Add(new OrderItem
                {
                    ProductName = $"Product {i}",
                    Price = 10,
                    Quantity = 1
                });
            }
            order.Total = order.Items.Sum(x => x.Price * x.Quantity);
            return order;
        }

        private void ApplyDiscount(Order order)
        {
            if (order.Total > 500)
                order.Total *= 0.9m;
        }

        private void SaveOrder(Order order)
        {
            using var ctx = new AppDbContext();
            ctx.Orders.Add(order);
            ctx.SaveChanges();
        }

        private void Notify(Order order)
        {
            //Console.WriteLine($"تم إنشاء طلب بـ {order.Total} ريال");
        }
    }
}
