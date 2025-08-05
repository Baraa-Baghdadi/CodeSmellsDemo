namespace CodeSmellsDemo.LargeClassClean
{
    // Facade pattern لتوحيد الواجهة إذا لزم الأمر
    public class ECommerceSystem
    {
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IPaymentService _paymentService;
        private readonly IEmailService _emailService;

        public ECommerceSystem()
        {
            _userService = new UserService();
            _productService = new ProductService();
            _orderService = new OrderService(_productService);
            _paymentService = new PaymentService(_orderService);
            _emailService = new EmailService();
        }

        public bool RegisterUser(string name, string email, string password)
        {
            var success = _userService.RegisterUser(name, email, password);
            if (success) _emailService.SendWelcomeEmail(email);
            return success;
        }

        public string PlaceOrder(string userEmail, List<string> productNames, string paymentMethod)
        {
            var orderId = _orderService.CreateOrder(userEmail, productNames);
            var total = _orderService.GetOrderTotal(orderId);

            if (_paymentService.ProcessPayment(orderId, paymentMethod, total))
            {
                _emailService.SendOrderConfirmation(userEmail, orderId);
                return orderId;
            }

            return null;
        }
    }
}
