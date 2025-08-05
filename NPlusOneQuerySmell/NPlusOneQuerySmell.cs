using CodeSmellsDemo.Data;
using CodeSmellsDemo.DTO;

namespace CodeSmellsDemo.NPlusOneQuerySmell
{
    public class NPlusOneQuerySmell
    {
        private readonly AppDbContext _context;

        public NPlusOneQuerySmell(AppDbContext context)
        {
            _context = context;
        }

        // مشكلة N+1: استعلام واحد للحصول على Orders + N استعلام للحصول على Items لكل Order
        public List<OrderSummary> GetOrderSummaries()
        {
            var orders = _context.Orders.ToList(); // 1 Query

            var summaries = new List<OrderSummary>();
            foreach (var order in orders) // N Queries (واحد لكل order)
            {
                // هذا سيؤدي لاستعلام منفصل لكل order!
                var items = _context.OrderItems
                    .Where(i => i.OrderId == order.Id)
                    .ToList(); // Query إضافي لكل order!

                summaries.Add(new OrderSummary
                {
                    OrderId = order.Id,
                    CustomerName = order.CustomerName,
                    Total = order.Total,
                    ItemCount = items.Count,
                    ItemsDetails = string.Join(", ", items.Select(i => i.ProductName))
                });
            }

            return summaries;
        }

        // مشكلة أخرى: تحميل بيانات غير ضرورية
        public List<CustomerOrderInfo> GetCustomerOrderInfo()
        {
            var customers = _context.Customers.ToList(); // 1 Query
            var result = new List<CustomerOrderInfo>();

            foreach (var customer in customers)
            {
                // استعلام منفصل لكل customer
                var orderCount = _context.Orders
                    .Where(o => o.CustomerName == customer.Name)
                    .Count(); // N Queries

                var totalSpent = _context.Orders
                    .Where(o => o.CustomerName == customer.Name)
                    .Sum(o => o.Total); // N Queries أخرى!

                result.Add(new CustomerOrderInfo
                {
                    CustomerName = customer.Name,
                    OrderCount = orderCount,
                    TotalSpent = totalSpent
                });
            }

            return result;
        }
    }
}
