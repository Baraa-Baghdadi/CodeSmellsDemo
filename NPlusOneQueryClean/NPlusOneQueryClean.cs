using CodeSmellsDemo.Data;
using CodeSmellsDemo.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmellsDemo.NPlusOneQueryClean
{
    public class NPlusOneQueryClean
    {
        private readonly AppDbContext _context;

        public NPlusOneQueryClean(AppDbContext context)
        {
            _context = context;
        }

        // الحل: استخدام Include لتحميل البيانات مرة واحدة
        public List<OrderSummary> GetOrderSummaries()
        {
            // استعلام واحد فقط يحضر Orders مع Items
            var orders = _context.Orders
                .Include(o => o.Items)
                .ToList(); // 1 Query only!

            var summaries = orders.Select(order => new OrderSummary
            {
                OrderId = order.Id,
                CustomerName = order.CustomerName,
                Total = order.Total,
                ItemCount = order.Items.Count,
                ItemsDetails = string.Join(", ", order.Items.Select(i => i.ProductName))
            }).ToList();

            return summaries;
        }

        // الحل: استخدام GroupBy في قاعدة البيانات
        public List<CustomerOrderInfo> GetCustomerOrderInfo()
        {
            // استعلام واحد فقط باستخدام GroupBy
            var result = _context.Orders
                .GroupBy(o => o.CustomerName)
                .Select(g => new CustomerOrderInfo
                {
                    CustomerName = g.Key,
                    OrderCount = g.Count(),
                    TotalSpent = g.Sum(o => o.Total)
                })
                .ToList(); // 1 Query only!

            return result;
        }

        // الحل المتقدم: استخدام Projection لتحسين الأداء أكثر
        public List<OrderSummary> GetOrderSummariesOptimized()
        {
            // نحضر البيانات المطلوبة فقط
            var summaries = _context.Orders
                .Select(o => new OrderSummary
                {
                    OrderId = o.Id,
                    CustomerName = o.CustomerName,
                    Total = o.Total,
                    ItemCount = o.Items.Count(),
                    ItemsDetails = string.Join(", ", o.Items.Select(i => i.ProductName))
                })
                .ToList(); // 1 Query with optimized data transfer

            return summaries;
        }
    }
}
