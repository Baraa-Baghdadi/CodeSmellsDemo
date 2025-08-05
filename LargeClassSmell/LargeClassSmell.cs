namespace CodeSmellsDemo.LargeClassSmell
{
    public class LargeClassSmell
    {
        // User Management
        public List<User> Users { get; set; } = new();
        public List<string> UserSessions { get; set; } = new();

        // Product Management
        public List<Product> Products { get; set; } = new();
        public Dictionary<string, int> ProductStock { get; set; } = new();

        // Order Management
        public List<Order> Orders { get; set; } = new();
        public Dictionary<string, decimal> OrderTotals { get; set; } = new();

        // Payment Management
        public List<Payment> Payments { get; set; } = new();
        public Dictionary<string, string> PaymentMethods { get; set; } = new();

        // Email Management
        public List<string> EmailQueue { get; set; } = new();
        public Dictionary<string, bool> EmailStatus { get; set; } = new();

        // User Methods
        public bool RegisterUser(string name, string email, string password)
        {
            if (Users.Any(u => u.Email == email)) return false;
            Users.Add(new User { Name = name, Email = email, Password = password });
            return true;
        }

        public User LoginUser(string email, string password)
        {
            var user = Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user != null) UserSessions.Add(user.Email);
            return user;
        }

        public void LogoutUser(string email)
        {
            UserSessions.Remove(email);
        }

        // Product Methods
        public bool AddProduct(string name, decimal price, int stock)
        {
            var product = new Product { Name = name, Price = price };
            Products.Add(product);
            ProductStock[name] = stock;
            return true;
        }

        public bool UpdateStock(string productName, int newStock)
        {
            if (ProductStock.ContainsKey(productName))
            {
                ProductStock[productName] = newStock;
                return true;
            }
            return false;
        }

        public List<Product> GetAvailableProducts()
        {
            return Products.Where(p => ProductStock.GetValueOrDefault(p.Name, 0) > 0).ToList();
        }

        // Order Methods
        public string CreateOrder(string userEmail, List<string> productNames)
        {
            var orderId = Guid.NewGuid().ToString();
            var order = new Order { Id = orderId, UserEmail = userEmail, ProductNames = productNames };
            Orders.Add(order);

            decimal total = 0;
            foreach (var productName in productNames)
            {
                var product = Products.FirstOrDefault(p => p.Name == productName);
                if (product != null) total += product.Price;
            }
            OrderTotals[orderId] = total;

            return orderId;
        }

        public Order GetOrder(string orderId)
        {
            return Orders.FirstOrDefault(o => o.Id == orderId);
        }

        // Payment Methods
        public bool ProcessPayment(string orderId, string paymentMethod, decimal amount)
        {
            if (!OrderTotals.ContainsKey(orderId)) return false;

            var payment = new Payment { OrderId = orderId, Amount = amount, Method = paymentMethod };
            Payments.Add(payment);
            PaymentMethods[orderId] = paymentMethod;

            return true;
        }

        public List<Payment> GetUserPayments(string userEmail)
        {
            var userOrders = Orders.Where(o => o.UserEmail == userEmail).Select(o => o.Id);
            return Payments.Where(p => userOrders.Contains(p.OrderId)).ToList();
        }

        // Email Methods
        public void SendWelcomeEmail(string email)
        {
            EmailQueue.Add($"Welcome: {email}");
            EmailStatus[email] = false;
        }

        public void SendOrderConfirmation(string email, string orderId)
        {
            EmailQueue.Add($"Order Confirmation: {email} - {orderId}");
            EmailStatus[$"{email}-{orderId}"] = false;
        }

        public void ProcessEmailQueue()
        {
            foreach (var email in EmailQueue.ToList())
            {
                // معالجة الإيميل
                Console.WriteLine($"Sending: {email}");
                EmailQueue.Remove(email);
            }
        }

        // Reporting Methods
        public decimal GetTotalRevenue()
        {
            return Payments.Sum(p => p.Amount);
        }

        public int GetTotalUsers()
        {
            return Users.Count;
        }

        public Dictionary<string, int> GetProductSales()
        {
            var sales = new Dictionary<string, int>();
            foreach (var order in Orders)
            {
                foreach (var productName in order.ProductNames)
                {
                    sales[productName] = sales.GetValueOrDefault(productName, 0) + 1;
                }
            }
            return sales;
        }
    }

    // Supporting classes
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class Order
    {
        public string Id { get; set; }
        public string UserEmail { get; set; }
        public List<string> ProductNames { get; set; } = new();
    }

    public class Payment
    {
        public string OrderId { get; set; }
        public decimal Amount { get; set; }
        public string Method { get; set; }
    }
 }
