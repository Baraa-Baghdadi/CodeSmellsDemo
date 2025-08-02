using CodeSmellsDemo.Data;
using CodeSmellsDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmellsDemo.LongMethodSmell
{
    public class LongMethodSmell
    {
        public void ProcessBigOrder()
        {
            var order = new Order
            {
                CustomerName = "BigCustomer",
                CreatedDate = DateTime.Now,
                Total = 0
            };

            for (int i = 0; i < 100; i++)
            {
                var item = new OrderItem
                {
                    ProductName = $"Product {i}",
                    Price = 10,
                    Quantity = 1
                };
                order.Items.Add(item);
                order.Total += item.Price * item.Quantity;
            }

            // خصومات
            if (order.Total > 500)
            {
                order.Total *= 0.9m;
            }

            // إرسال إشعار
            //Console.WriteLine($"تم إنشاء طلب بـ {order.Total} ريال");

            // حفظ
            using var ctx = new AppDbContext();
            ctx.Orders.Add(order);
            ctx.SaveChanges();
        }
    }
}
