using CodeSmellsDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmellsDemo.StringConcatenationSmell
{
    public class StringConcatenationSmell
    {
        // مشكلة: استخدام + في الحلقات - بطيء جداً!
        public string GenerateOrderReport(List<Order> orders)
        {
            string report = ""; // String غير قابل للتغيير

            report += "=== Order Report ===\n";
            report += $"Generated on: {DateTime.Now}\n";
            report += $"Total Orders: {orders.Count}\n\n";

            foreach (var order in orders)
            {
                // كل += ينشئ string جديد في الذاكرة!
                report += $"Order ID: {order.Id}\n";
                report += $"Customer: {order.CustomerName}\n";
                report += $"Date: {order.CreatedDate:yyyy-MM-dd}\n";
                report += $"Total: {order.Total:C}\n";
                report += "Items:\n";

                foreach (var item in order.Items)
                {
                    // المزيد من الـ concatenation البطيء
                    report += $"  - {item.ProductName} x{item.Quantity} = {item.Price * item.Quantity:C}\n";
                }

                report += "------------------------\n";
            }

            report += $"\nTotal Revenue: {orders.Sum(o => o.Total):C}\n";
            report += "=== End Report ===";

            return report;
        }

        // مشكلة أخرى: بناء HTML بـ string concatenation
        public string GenerateHtmlTable(List<Order> orders)
        {
            string html = "";

            html += "<table border='1'>";
            html += "<tr><th>ID</th><th>Customer</th><th>Date</th><th>Total</th></tr>";

            foreach (var order in orders)
            {
                html += "<tr>";
                html += $"<td>{order.Id}</td>";
                html += $"<td>{order.CustomerName}</td>";
                html += $"<td>{order.CreatedDate:yyyy-MM-dd}</td>";
                html += $"<td>{order.Total:C}</td>";
                html += "</tr>";
            }

            html += "</table>";
            return html;
        }

        // مشكلة: بناء JSON يدوياً
        public string GenerateJsonReport(List<Order> orders)
        {
            string json = "";

            json += "{\n";
            json += "  \"orders\": [\n";

            for (int i = 0; i < orders.Count; i++)
            {
                var order = orders[i];
                json += "    {\n";
                json += $"      \"id\": {order.Id},\n";
                json += $"      \"customer\": \"{order.CustomerName}\",\n";
                json += $"      \"date\": \"{order.CreatedDate:yyyy-MM-dd}\",\n";
                json += $"      \"total\": {order.Total}\n";
                json += "    }";

                if (i < orders.Count - 1)
                    json += ",";

                json += "\n";
            }

            json += "  ]\n";
            json += "}";

            return json;
        }

        // مشكلة: إنشاء SQL queries بـ string concatenation
        public string BuildDynamicQuery(Dictionary<string, object> filters)
        {
            string query = "SELECT * FROM Orders WHERE 1=1";

            foreach (var filter in filters)
            {
                query += $" AND {filter.Key} = '{filter.Value}'";
            }

            query += " ORDER BY CreatedDate DESC";
            return query;
        }
    }
}
