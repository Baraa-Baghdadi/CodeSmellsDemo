using System.Text;
using CodeSmellsDemo.Models;

namespace CodeSmellsDemo.StringConcatenationClean
{
    public class StringConcatenationClean
    {
        // الحل: استخدام StringBuilder - سريع جداً!
        public string GenerateOrderReport(List<Order> orders)
        {
            var report = new StringBuilder(); // قابل للتغيير وسريع

            report.AppendLine("=== Order Report ===");
            report.AppendLine($"Generated on: {DateTime.Now}");
            report.AppendLine($"Total Orders: {orders.Count}");
            report.AppendLine();

            foreach (var order in orders)
            {
                report.AppendLine($"Order ID: {order.Id}");
                report.AppendLine($"Customer: {order.CustomerName}");
                report.AppendLine($"Date: {order.CreatedDate:yyyy-MM-dd}");
                report.AppendLine($"Total: {order.Total:C}");
                report.AppendLine("Items:");

                foreach (var item in order.Items)
                {
                    report.AppendLine($"  - {item.ProductName} x{item.Quantity} = {item.Price * item.Quantity:C}");
                }

                report.AppendLine("------------------------");
            }

            report.AppendLine($"\nTotal Revenue: {orders.Sum(o => o.Total):C}");
            report.AppendLine("=== End Report ===");

            return report.ToString();
        }

        // الحل المحسن: استخدام StringBuilder مع capacity محدد مسبقاً
        public string GenerateOrderReportOptimized(List<Order> orders)
        {
            // تقدير الحجم المطلوب لتجنب إعادة تخصيص الذاكرة
            int estimatedSize = orders.Count * 200; // تقدير تقريبي
            var report = new StringBuilder(estimatedSize);

            report.AppendLine("=== Order Report ===");
            report.AppendLine($"Generated on: {DateTime.Now}");
            report.AppendLine($"Total Orders: {orders.Count}");
            report.AppendLine();

            foreach (var order in orders)
            {
                report.AppendFormat("Order ID: {0}\n", order.Id);
                report.AppendFormat("Customer: {0}\n", order.CustomerName);
                report.AppendFormat("Date: {0:yyyy-MM-dd}\n", order.CreatedDate);
                report.AppendFormat("Total: {0:C}\n", order.Total);
                report.AppendLine("Items:");

                foreach (var item in order.Items)
                {
                    report.AppendFormat("  - {0} x{1} = {2:C}\n",
                        item.ProductName, item.Quantity, item.Price * item.Quantity);
                }

                report.AppendLine("------------------------");
            }

            report.AppendFormat("\nTotal Revenue: {0:C}\n", orders.Sum(o => o.Total));
            report.AppendLine("=== End Report ===");

            return report.ToString();
        }

        // الحل: استخدام StringBuilder للـ HTML
        public string GenerateHtmlTable(List<Order> orders)
        {
            var html = new StringBuilder();

            html.AppendLine("<table border='1'>");
            html.AppendLine("<tr><th>ID</th><th>Customer</th><th>Date</th><th>Total</th></tr>");

            foreach (var order in orders)
            {
                html.AppendLine("<tr>");
                html.AppendFormat("<td>{0}</td>", order.Id);
                html.AppendFormat("<td>{0}</td>", order.CustomerName);
                html.AppendFormat("<td>{0:yyyy-MM-dd}</td>", order.CreatedDate);
                html.AppendFormat("<td>{0:C}</td>", order.Total);
                html.AppendLine("</tr>");
            }

            html.AppendLine("</table>");
            return html.ToString();
        }

        // الحل الأفضل: استخدام string.Join للحالات البسيطة
        public string GenerateHtmlTableSimple(List<Order> orders)
        {
            var rows = orders.Select(order =>
                $"<tr><td>{order.Id}</td><td>{order.CustomerName}</td>" +
                $"<td>{order.CreatedDate:yyyy-MM-dd}</td><td>{order.Total:C}</td></tr>");

            return "<table border='1'>\n" +
                   "<tr><th>ID</th><th>Customer</th><th>Date</th><th>Total</th></tr>\n" +
                   string.Join("\n", rows) + "\n" +
                   "</table>";
        }

        // الحل: استخدام Span<char> للأداء الأقصى (C# 7.2+)
        public string GenerateReportWithSpan(List<Order> orders)
        {
            const int BufferSize = 1024;
            Span<char> buffer = stackalloc char[BufferSize];
            var result = new StringBuilder();

            foreach (var order in orders)
            {
                // استخدام Span لتجنب allocations إضافية
                var formatted = $"Order {order.Id}: {order.CustomerName} - {order.Total:C}".AsSpan();
                result.AppendLine(formatted.ToString());
            }

            return result.ToString();
        }

        // الحل: استخدام string interpolation مع arrays للحالات المعروفة
        public string GenerateFixedReport(Order[] orders)
        {
            if (orders.Length == 0) return "No orders found.";

            // للمصفوفات الصغيرة، يمكن استخدام string interpolation مباشرة
            if (orders.Length <= 10)
            {
                return string.Join("\n", orders.Select(o =>
                    $"Order {o.Id}: {o.CustomerName} - {o.Total:C}"));
            }

            // للمصفوفات الكبيرة، نستخدم StringBuilder
            return GenerateOrderReport(orders.ToList());
        }
    }
}
