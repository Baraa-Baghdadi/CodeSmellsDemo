using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmellsDemo.LongSwitchClean
{
    public class OvernightShipping : IShippingCalculator
    {
        public decimal CalculateShipping(decimal weight, string destination)
        {
            return destination switch
            {
                "local" => weight * 10.0m,
                "regional" => weight * 25.0m,
                _ => weight * 50.0m
            };
        }

        public string GetShippingTime() => "Next business day";
        public bool IsAvailable(string destination) => true;
    }
}
