using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmellsDemo.TooManyParametersSmell
{
    public class TooManyParametersSmell
    {
        public void RegisterCustomer(string name, string phone, string email, string address, string city, string postal, bool active, DateTime birthday)
        {
            //Console.WriteLine($"{name} تم تسجيله");
        }
    }
}
