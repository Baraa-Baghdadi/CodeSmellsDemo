using CodeSmellsDemo.LargeClassSmell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmellsDemo.LargeClassClean
{
    public interface IPaymentService
    {
        bool ProcessPayment(string orderId, string paymentMethod, decimal amount);
        List<Payment> GetUserPayments(string userEmail);
        decimal GetTotalRevenue();
    }
}
