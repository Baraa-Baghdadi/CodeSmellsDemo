using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmellsDemo.LongSwitchClean
{
    public class SameDayShipping : IShippingCalculator
    {
        public decimal CalculateShipping(decimal weight, string destination)
        {
            if (destination != "local")
                throw new InvalidOperationException("Same day shipping only available locally");
            return weight * 15.0m;
        }

        public string GetShippingTime() => "Same day";
        public bool IsAvailable(string destination) => destination == "local";
    }
}
