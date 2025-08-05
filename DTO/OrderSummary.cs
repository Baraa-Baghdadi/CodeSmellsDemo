using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmellsDemo.DTO
{
    public class OrderSummary
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public decimal Total { get; set; }
        public int ItemCount { get; set; }
        public string ItemsDetails { get; set; }
    }
}
