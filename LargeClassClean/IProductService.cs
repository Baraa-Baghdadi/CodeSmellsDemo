using CodeSmellsDemo.LargeClassSmell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmellsDemo.LargeClassClean
{
    public interface IProductService
    {
        bool AddProduct(string name, decimal price, int stock);
        bool UpdateStock(string productName, int newStock);
        List<Product> GetAvailableProducts();
        Product GetProduct(string name);
    }
}
