using CodeSmellsDemo.LargeClassSmell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmellsDemo.LargeClassClean
{
    public class ProductService : IProductService
    {
        private readonly List<Product> _products = new();
        private readonly Dictionary<string, int> _productStock = new();

        public bool AddProduct(string name, decimal price, int stock)
        {
            var product = new Product { Name = name, Price = price };
            _products.Add(product);
            _productStock[name] = stock;
            return true;
        }

        public bool UpdateStock(string productName, int newStock)
        {
            if (_productStock.ContainsKey(productName))
            {
                _productStock[productName] = newStock;
                return true;
            }
            return false;
        }

        public List<Product> GetAvailableProducts()
        {
            return _products.Where(p => _productStock.GetValueOrDefault(p.Name, 0) > 0).ToList();
        }

        public Product GetProduct(string name)
        {
            return _products.FirstOrDefault(p => p.Name == name);
        }
    }
}
