using CodeSmellsDemo.Data;
using CodeSmellsDemo.DuplicatedCodeSmell;
using CodeSmellsDemo.GodClassSmell;
using CodeSmellsDemo.LargeClassClean;
using CodeSmellsDemo.LargeClassSmell;
using CodeSmellsDemo.LongMethodSmell;
using CodeSmellsDemo.LongSwitchClean;
using CodeSmellsDemo.LongSwitchSmell;
using CodeSmellsDemo.MagicNumbersClean;
using CodeSmellsDemo.MagicNumbersSmell;
using CodeSmellsDemo.Models;
using CodeSmellsDemo.NPlusOneQueryClean;
using CodeSmellsDemo.NPlusOneQuerySmell;
using CodeSmellsDemo.RepeatedSaveChanges;
using CodeSmellsDemo.StringConcatenationClean;
using CodeSmellsDemo.StringConcatenationSmell;
using CodeSmellsDemo.TooManyParametersSmell;
using RepeatedSaveChanges.CodeSmells;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Code Smells Performance Comparison ===\n");

        #region Repeated Save Changes
        var context = new AppDbContext();
        Console.WriteLine("--- Start Test Repeated SaveChanges ---");
        RepeatedSaveChangesSmell.Run(context);

        context = new AppDbContext();
        RepeatedSaveChangesClean.Run(context);
        Console.WriteLine("=== End Test Repeated SaveChanges ===\n");
        #endregion

        #region God Class
        Console.WriteLine("--- Start Test God Class ---");
        var stopwatch = Stopwatch.StartNew();
        var god = new GodClassSmell(new AppDbContext());
        god.CreateAndNotifyOrder("Ali", new List<OrderItem>
        {
            new OrderItem { ProductName = "Product 1", Price = 100, Quantity = 2 }
        });
        stopwatch.Stop();
        Console.WriteLine($"[Smell God Class] : {stopwatch.ElapsedMilliseconds} ms");

        stopwatch = Stopwatch.StartNew();
        var cleanOrderService = new GodClassClean(new AppDbContext());
        var notification = new NotificationService();
        int orderId = cleanOrderService.CreateOrder("Ahmed", new List<OrderItem>
        {
            new OrderItem { ProductName = "Product 1", Price = 100, Quantity = 2 }
        });
        notification.SendOrderNotification("Ahmed", orderId);
        stopwatch.Stop();
        Console.WriteLine($"[Clean God Class] : {stopwatch.ElapsedMilliseconds} ms");
        Console.WriteLine("=== End Test God Class ===\n");
        #endregion

        #region Magic Numbers
        Console.WriteLine("--- Start Test Magic Numbers ---");
        stopwatch = Stopwatch.StartNew();
        var magicNumbersSmell = new MagicNumbersSmell();
        for (int i = 0; i < 10000; i++)
        {
            magicNumbersSmell.CalculateDiscount(1500m, "VIP");
            magicNumbersSmell.IsValidAge(25);
        }
        stopwatch.Stop();
        Console.WriteLine($"[Smell Magic Numbers] : {stopwatch.ElapsedMilliseconds} ms");

        stopwatch = Stopwatch.StartNew();
        var magicNumbersClean = new MagicNumbersClean();
        for (int i = 0; i < 10000; i++)
        {
            magicNumbersClean.CalculateDiscount(1500m, "VIP");
            magicNumbersClean.IsValidAge(25);
        }
        stopwatch.Stop();
        Console.WriteLine($"[Clean Magic Numbers] : {stopwatch.ElapsedMilliseconds} ms");
        Console.WriteLine("=== End Test Magic Numbers ===\n");
        #endregion

        #region Long Method
        Console.WriteLine("--- Start Test Long Method ---");
        stopwatch = Stopwatch.StartNew();
        var longMethodSmell = new LongMethodSmell();
        longMethodSmell.ProcessBigOrder();
        stopwatch.Stop();
        Console.WriteLine($"[Smell Long Method] : {stopwatch.ElapsedMilliseconds} ms");

        stopwatch = Stopwatch.StartNew();
        var longMethodClean = new LongMethodClean();
        longMethodClean.ProcessBigOrder();
        stopwatch.Stop();
        Console.WriteLine($"[Clean Long Method] : {stopwatch.ElapsedMilliseconds} ms");
        Console.WriteLine("=== End Test Long Method ===\n");
        #endregion

        #region Too Many Parameters
        Console.WriteLine("--- Start Test Too Many Parameters ---");
        stopwatch = Stopwatch.StartNew();
        var tooManyparams = new TooManyParametersSmell();
        for (int i = 0; i < 1000; i++)
        {
            tooManyparams.RegisterCustomer("Baraa", "+963 932 912 812",
                "MhdBaraa@gmail.com", "Midan", "Damascus", "postal", true, DateTime.Now);
        }
        stopwatch.Stop();
        Console.WriteLine($"[Smell Too Many Parameters] : {stopwatch.ElapsedMilliseconds} ms");

        stopwatch = Stopwatch.StartNew();
        var tooManyparamsClean = new TooManyParametersClean();
        var param = new CustomerRegistrationDto
        {
            Active = true,
            Address = "Midan",
            Birthday = DateTime.Now,
            City = "Damascus",
            Email = "MhdBaraa@gmail.com",
            Name = "Baraa",
            Phone = "+963 932 912 812",
            Postal = "postal"
        };
        for (int i = 0; i < 1000; i++)
        {
            tooManyparamsClean.RegisterCustomer(param);
        }
        stopwatch.Stop();
        Console.WriteLine($"[Clean Too Many Parameters] : {stopwatch.ElapsedMilliseconds} ms");
        Console.WriteLine("=== End Test Too Many Parameters ===\n");
        #endregion

        #region Duplicated Code (Existing)
        Console.WriteLine("--- Start Test Duplicated Code ---");
        stopwatch = Stopwatch.StartNew();
        var duplicatedCodeSmell = new DuplicatedCodeSmell();
        for (int i = 0; i < 10000; i++)
        {
            duplicatedCodeSmell.CreateOrder();
            duplicatedCodeSmell.CancelOrder();
        }
        stopwatch.Stop();
        Console.WriteLine($"[Smell Duplicated Code] : {stopwatch.ElapsedMilliseconds} ms");

        stopwatch = Stopwatch.StartNew();
        var duplicatedCodeClean = new DuplicatedCodeClean();
        for (int i = 0; i < 10000; i++)
        {
            duplicatedCodeClean.CreateOrder();
            duplicatedCodeClean.CancelOrder();
        }
        stopwatch.Stop();
        Console.WriteLine($"[Clean Duplicated Code] : {stopwatch.ElapsedMilliseconds} ms");
        Console.WriteLine("=== End Test Duplicated Code ===\n");
        #endregion

        #region Large Class
        Console.WriteLine("--- Start Test Large Class ---");
        stopwatch = Stopwatch.StartNew();
        var largeClassSmell = new LargeClassSmell();
        for (int i = 0; i < 1000; i++)
        {
            largeClassSmell.RegisterUser($"User{i}", $"user{i}@test.com", "password");
            largeClassSmell.AddProduct($"Product{i}", 50m, 100);
            var orderId1 = largeClassSmell.CreateOrder($"user{i}@test.com", new List<string> { $"Product{i}" });
            largeClassSmell.ProcessPayment(orderId1, "Credit Card", 50m);
            largeClassSmell.SendOrderConfirmation($"user{i}@test.com", orderId1);
        }
        stopwatch.Stop();
        Console.WriteLine($"[Smell Large Class] : {stopwatch.ElapsedMilliseconds} ms");

        stopwatch = Stopwatch.StartNew();
        var ecommerceSystem = new ECommerceSystem();
        for (int i = 0; i < 1000; i++)
        {
            ecommerceSystem.RegisterUser($"User{i}", $"user{i}@test.com", "password");
            // ملاحظة: هنا نحتاج لإضافة منتجات عبر ProductService منفصل
            // لكن للتبسيط سنركز على العمليات المتاحة
            var orderId1 = ecommerceSystem.PlaceOrder($"user{i}@test.com",
                new List<string> { $"Product{i}" }, "Credit Card");
        }
        stopwatch.Stop();
        Console.WriteLine($"[Clean Large Class] : {stopwatch.ElapsedMilliseconds} ms");
        Console.WriteLine("=== End Test Large Class ===\n");
        #endregion

        #region Long Switch Statement
        Console.WriteLine("--- Start Test Long Switch Statement ---");
        stopwatch = Stopwatch.StartNew();
        var longSwitchSmell = new LongSwitchSmell();
        for (int i = 0; i < 5000; i++)
        {
            longSwitchSmell.CalculateShipping("express", 2.5m, "regional");
            longSwitchSmell.GetShippingTime("overnight");
        }
        stopwatch.Stop();
        Console.WriteLine($"[Smell Long Switch] : {stopwatch.ElapsedMilliseconds} ms");

        stopwatch = Stopwatch.StartNew();
        var longSwitchClean = new LongSwitchClean();
        for (int i = 0; i < 5000; i++)
        {
            longSwitchClean.CalculateShipping("express", 2.5m, "regional");
            longSwitchClean.GetShippingTime("overnight");
        }
        stopwatch.Stop();
        Console.WriteLine($"[Clean Long Switch] : {stopwatch.ElapsedMilliseconds} ms");
        Console.WriteLine("=== End Test Long Switch Statement ===\n");
        #endregion

        #region N+1 Query Problem - أخطر رائحة للأداء!
        Console.WriteLine("--- Start Test N+1 Query Problem ---");
        Console.WriteLine("This is the MOST DANGEROUS performance smell!");

        stopwatch = Stopwatch.StartNew();
        var nPlusOneSmell = new NPlusOneQuerySmell(context);
        var smellResults = nPlusOneSmell.GetOrderSummaries();
        stopwatch.Stop();
        Console.WriteLine($"[SMELL N+1 Query] Orders: {smellResults.Count} - Time: {stopwatch.ElapsedMilliseconds} ms");


        stopwatch = Stopwatch.StartNew();
        var nPlusOneClean = new NPlusOneQueryClean(context);
        var cleanResults = nPlusOneClean.GetOrderSummaries();
        stopwatch.Stop();
        Console.WriteLine($"[CLEAN N+1 Query] Orders: {cleanResults.Count} - Time: {stopwatch.ElapsedMilliseconds} ms");

        // اختبار Customer Order Info
        stopwatch = Stopwatch.StartNew();
        nPlusOneSmell = new NPlusOneQuerySmell(context);
        var smellResults1 = nPlusOneSmell.GetCustomerOrderInfo();
        stopwatch.Stop();
        Console.WriteLine($"[SMELL Customer Info] Customers: {smellResults1.Count} - Time: {stopwatch.ElapsedMilliseconds} ms");

        stopwatch = Stopwatch.StartNew();
        nPlusOneClean = new NPlusOneQueryClean(context);
        var cleanResults2 = nPlusOneClean.GetCustomerOrderInfo();
        stopwatch.Stop();
        Console.WriteLine($"[CLEAN Customer Info] Customers: {cleanResults2.Count} - Time: {stopwatch.ElapsedMilliseconds} ms");

        Console.WriteLine("=== End Test N+1 Query Problem ===\n");
        #endregion

        #region String Concatenation in Loops - فرق هائل في الأداء!
        Console.WriteLine("--- Start Test String Concatenation ---");
        Console.WriteLine("String concatenation in loops - MASSIVE performance difference!");

        var orders = GenerateTestOrders(1000); // 1000 orders للاختبار

        stopwatch = Stopwatch.StartNew();
        var stringSmell = new StringConcatenationSmell();
        var smellReport = stringSmell.GenerateOrderReport(orders);
        stopwatch.Stop();
        Console.WriteLine($"[SMELL String Concatenation] Report length: {smellReport.Length} chars - Time: {stopwatch.ElapsedMilliseconds} ms");

        stopwatch = Stopwatch.StartNew();
        var stringClean = new StringConcatenationClean();
        var cleanReport = stringClean.GenerateOrderReport(orders);
        stopwatch.Stop();
        Console.WriteLine($"[CLEAN String Concatenation] Report length: {cleanReport.Length} chars - Time: {stopwatch.ElapsedMilliseconds} ms");

        // اختبار HTML Table generation
        stopwatch = Stopwatch.StartNew();
        var smellHtml = stringSmell.GenerateHtmlTable(orders);
        stopwatch.Stop();
        Console.WriteLine($"[SMELL HTML Generation] HTML length: {smellHtml.Length} chars - Time: {stopwatch.ElapsedMilliseconds} ms");

        stopwatch = Stopwatch.StartNew();
        var cleanHtml = stringClean.GenerateHtmlTable(orders);
        stopwatch.Stop();
        Console.WriteLine($"[CLEAN HTML Generation] HTML length: {cleanHtml.Length} chars - Time: {stopwatch.ElapsedMilliseconds} ms");

        Console.WriteLine("=== End Test String Concatenation ===\n");
    }
        #endregion

        #region private

        // Helper methods to generate test data
    private static List<CodeSmellsDemo.Models.Order> GenerateTestOrders(int count)
    {
        var random = new Random();
        var orders = new List<CodeSmellsDemo.Models.Order>();

        for (int i = 1; i <= count; i++)
        {
            var order = new CodeSmellsDemo.Models.Order
            {
                Id = i,
                CustomerName = $"Customer {i}",
                CreatedDate = DateTime.Now.AddDays(-random.Next(365)),
                Total = random.Next(100, 2000),
                Items = new List<OrderItem>()
            };

            // Add random items to each order
            int itemCount = random.Next(1, 6);
            for (int j = 1; j <= itemCount; j++)
            {
                order.Items.Add(new OrderItem
                {
                    Id = i * 1000 + j,
                    ProductName = $"Product {random.Next(1, 100)}",
                    Price = random.Next(10, 500),
                    Quantity = random.Next(1, 5),
                    OrderId = i
                });
            }

            orders.Add(order);
        }

        return orders;
    }

    #endregion
}