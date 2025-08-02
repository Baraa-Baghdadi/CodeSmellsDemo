using CodeSmellsDemo.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmellsDemo.LazyLoading
{
    public class EagerLoading
    {
        private readonly AppDbContext _context;

        public EagerLoading(AppDbContext context)
        {
            _context = context;
        }

        public void ApplyEagerLoading()
        {
            var orders = _context.Orders.Include(o => o.Items).ToList();

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
