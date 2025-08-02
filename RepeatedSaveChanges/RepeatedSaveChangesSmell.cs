using CodeSmellsDemo.Data;
using CodeSmellsDemo.Models;
using System.Diagnostics;


namespace RepeatedSaveChanges.CodeSmells
{
    public class RepeatedSaveChangesSmell
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
                context.SaveChanges(); // خطأ - يتم داخل الحلقة
            }

            stopwatch.Stop();
            Console.WriteLine($"[Repeated SaveChanges] : {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
