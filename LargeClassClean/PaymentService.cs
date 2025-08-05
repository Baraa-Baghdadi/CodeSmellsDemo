using CodeSmellsDemo.LargeClassSmell;

namespace CodeSmellsDemo.LargeClassClean
{
    public class PaymentService : IPaymentService
    {
        private readonly List<Payment> _payments = new();
        private readonly Dictionary<string, string> _paymentMethods = new();
        private readonly IOrderService _orderService;

        public PaymentService(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public bool ProcessPayment(string orderId, string paymentMethod, decimal amount)
        {
            var order = _orderService.GetOrder(orderId);
            if (order == null) return false;

            var payment = new Payment { OrderId = orderId, Amount = amount, Method = paymentMethod };
            _payments.Add(payment);
            _paymentMethods[orderId] = paymentMethod;

            return true;
        }

        public List<Payment> GetUserPayments(string userEmail)
        {
            // هنا نحتاج للحصول على orders للمستخدم من OrderService
            return _payments.Where(p => true).ToList(); // تبسيط للمثال
        }

        public decimal GetTotalRevenue()
        {
            return _payments.Sum(p => p.Amount);
        }
    }
}
