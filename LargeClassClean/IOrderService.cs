using CodeSmellsDemo.LargeClassSmell;

namespace CodeSmellsDemo.LargeClassClean
{
    public interface IOrderService
    {
        string CreateOrder(string userEmail, List<string> productNames);
        Order GetOrder(string orderId);
        decimal GetOrderTotal(string orderId);
    }
}
