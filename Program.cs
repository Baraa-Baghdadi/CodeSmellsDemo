using CodeSmellsDemo.Data;
using CodeSmellsDemo.DuplicatedCodeSmell;
using CodeSmellsDemo.GodClassSmell;
using CodeSmellsDemo.LazyLoading;
using CodeSmellsDemo.LongMethodSmell;
using CodeSmellsDemo.Models;
using CodeSmellsDemo.RepeatedSaveChanges;
using CodeSmellsDemo.TooManyParametersSmell;
using RepeatedSaveChanges.CodeSmells;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        #region Repated Save Change

        var context = new AppDbContext();

        Console.WriteLine("--- Start Test Repeated SaveChanges ---");
        RepeatedSaveChangesSmell.Run(context);

        // إعادة تهيئة قاعدة البيانات
        context = new AppDbContext();

        RepeatedSaveChangesClean.Run(context);

        Console.WriteLine("===  End Test Repeated SaveChanges ===");

        #endregion

        #region God Class

        // GodClass Smell
        Console.WriteLine("--- Start Test God Class ---");
        var stopwatch = Stopwatch.StartNew();
        var god = new GodClassSmell(new AppDbContext());
        god.CreateAndNotifyOrder("Ali", new List<OrderItem>
        {
            new OrderItem { ProductName = "Product 1", Price = 100, Quantity = 2 }
        });
        stopwatch.Stop();
        Console.WriteLine($"[Smell God Class] : {stopwatch.ElapsedMilliseconds} ms");

        // GodClass Clean
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

        Console.WriteLine("===  End Test God Class ===");

        #endregion

        #region Long Method
        Console.WriteLine("--- Start Test long method ---");
        stopwatch = Stopwatch.StartNew();
        var longMethodSmell = new LongMethodSmell();
        longMethodSmell.ProcessBigOrder();
        stopwatch.Stop();
        Console.WriteLine($"[Smell long method] : {stopwatch.ElapsedMilliseconds} ms");
        stopwatch = Stopwatch.StartNew();
        var longMethodClean = new LongMethodClean();
        longMethodClean.ProcessBigOrder();
        stopwatch.Stop();
        Console.WriteLine($"[Clean long method] : {stopwatch.ElapsedMilliseconds} ms");
        Console.WriteLine("===  End Test long method ===");

        #endregion

        #region Too Many Parameters
        Console.WriteLine("--- Start Test Too Many Parameters ---");
        stopwatch = Stopwatch.StartNew();
        var tooManyparams = new TooManyParametersSmell();
        tooManyparams.RegisterCustomer("Baraa","+963 932 912 812","MhdBaraa@gmail.com","Midan","Damascus","postal",true,DateTime.Now);
        stopwatch.Stop();
        Console.WriteLine($"[Smell Too Many Parameters] : {stopwatch.ElapsedMilliseconds} ms");
        
        stopwatch = Stopwatch.StartNew();
        var tooManyparamsClean = new TooManyParametersClean();
        var param = new CustomerRegistrationDto
        {
            Active = true,
            Address = "",
            Birthday = DateTime.Now,
            City = "",
            Email = "",
            Name = "Name",
            Phone = "",
            Postal = ""
        };
        tooManyparamsClean.RegisterCustomer(param);
        stopwatch.Stop();
        Console.WriteLine($"[Clean Too Many Parameters] : {stopwatch.ElapsedMilliseconds} ms");

        Console.WriteLine("===  End Test Too Many Parameters ===");
        #endregion

        #region Load Data
        Console.WriteLine("--- Start Test Load Data ---");
        context = new AppDbContext();
        stopwatch = Stopwatch.StartNew();
        var lazyLoading = new LazyLoading(context);
        lazyLoading.ApplyLazyLoading();
        stopwatch.Stop();
        Console.WriteLine($"[Smell Load Data (Lazy loading)] : {stopwatch.ElapsedMilliseconds} ms");

        context = new AppDbContext();
        stopwatch = Stopwatch.StartNew();
        var eagerLoading = new EagerLoading(context);
        eagerLoading.ApplyEagerLoading();
        stopwatch.Stop();
        Console.WriteLine($"[Smell Load Data (Eager loading)] : {stopwatch.ElapsedMilliseconds} ms");

        Console.WriteLine("===  End Test Load Data ===");
        #endregion

        #region Duplicated Code
        Console.WriteLine("--- Start Test Duplicated Code ---");
        stopwatch = Stopwatch.StartNew();
        var duplicatedCodeSmell = new DuplicatedCodeSmell();
        for (int i = 0; i < 100; i++)
        {
            duplicatedCodeSmell.CreateOrder();
            duplicatedCodeSmell.CancelOrder();
        }

        stopwatch.Stop();
        Console.WriteLine($"[Smell Duplicated Code] : {stopwatch.ElapsedMilliseconds} ms");

        stopwatch = Stopwatch.StartNew();
        var duplicatedCodeClean = new DuplicatedCodeClean();
        for (int i = 0; i < 100; i++)
        {
            duplicatedCodeClean.CreateOrder();
            duplicatedCodeClean.CancelOrder();
        }

        stopwatch.Stop();
        Console.WriteLine($"[Clean Duplicated Code] : {stopwatch.ElapsedMilliseconds} ms");

        Console.WriteLine("===  End Test Duplicated Code ===");
        #endregion

    }
}