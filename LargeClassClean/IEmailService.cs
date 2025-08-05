using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmellsDemo.LargeClassClean
{
    public interface IEmailService
    {
        void SendWelcomeEmail(string email);
        void SendOrderConfirmation(string email, string orderId);
        void ProcessEmailQueue();
    }
}
