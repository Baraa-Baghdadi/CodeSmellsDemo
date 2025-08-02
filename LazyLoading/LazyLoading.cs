using CodeSmellsDemo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmellsDemo.LazyLoading
{
    public class LazyLoading
    {
        private readonly AppDbContext _context;

        public LazyLoading(AppDbContext context)
        {
            _context = context;
        }

        public void ApplyLazyLoading() {
            
            var orders = _context.Orders.ToList();

            foreach (var order in orders)
            {
                foreach (var item in order.Items)
                {
                    //Console.WriteLine(item.ProductName);
                }
            }
        }
    }
}

