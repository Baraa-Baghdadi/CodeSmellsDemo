using CodeSmellsDemo.Data;
using CodeSmellsDemo.Models;
using System.Diagnostics;

namespace CodeSmellsDemo.RepeatedSaveChanges
{
    public class RepeatedSaveChangesClean
    {
        public static void Run(AppDbContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < 500; i++)
            {
                var order = new Order
                {
                    CustomerName = $"Customer {i}",
                    CreatedDate = DateTime.Now,
                    Total = 100
                };

                context.Orders.Add(order);
            }

            context.SaveChanges(); // خارج الحلقة - ممتاز

            stopwatch.Stop();
            Console.WriteLine($"[Clean SaveChanges] : {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
