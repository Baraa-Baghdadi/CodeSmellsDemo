using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmellsDemo.TooManyParametersSmell
{
    public class TooManyParametersClean
    {
        public void RegisterCustomer(CustomerRegistrationDto dto)
        {
            //Console.WriteLine($"{dto.Name} تم تسجيله");
        }
    }

    public class CustomerRegistrationDto
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Postal { get; set; }
        public bool Active { get; set; }
        public DateTime Birthday { get; set; }
    }
}
