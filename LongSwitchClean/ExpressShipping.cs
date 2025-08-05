using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmellsDemo.LongSwitchClean
{
    public class ExpressShipping : IShippingCalculator
    {
        public decimal CalculateShipping(decimal weight, string destination)
        {
            return destination switch
            {
                "local" => weight * 5.0m,
                "regional" => weight * 10.0m,
                _ => weight * 20.0m
            };
        }

        public string GetShippingTime() => "2-3 business days";
        public bool IsAvailable(string destination) => true;
    }
}
