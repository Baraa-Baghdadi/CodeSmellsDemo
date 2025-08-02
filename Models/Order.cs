using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmellsDemo.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal Total { get; set; }
        public List<OrderItem> Items { get; set; } = new();
    }
}
