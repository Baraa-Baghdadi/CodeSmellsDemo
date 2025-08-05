using CodeSmellsDemo.LargeClassSmell;

namespace CodeSmellsDemo.LargeClassClean
{
    public class OrderService : IOrderService
    {
        private readonly List<Order> _orders = new();
        private readonly Dictionary<string, decimal> _orderTotals = new();
        private readonly IProductService _productService;

        public OrderService(IProductService productService)
        {
            _productService = productService;
        }

        public string CreateOrder(string userEmail, List<string> productNames)
        {
            var orderId = Guid.NewGuid().ToString();
            var order = new Order { Id = orderId, UserEmail = userEmail, ProductNames = productNames };
            _orders.Add(order);

            decimal total = 0;
            foreach (var productName in productNames)
            {
                var product = _productService.GetProduct(productName);
                if (product != null) total += product.Price;
            }
            _orderTotals[orderId] = total;

            return orderId;
        }

        public Order GetOrder(string orderId)
        {
            return _orders.FirstOrDefault(o => o.Id == orderId);
        }

        public decimal GetOrderTotal(string orderId)
        {
            return _orderTotals.GetValueOrDefault(orderId, 0);
        }
    }
}
