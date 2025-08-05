using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmellsDemo.LongSwitchClean
{
    public class LongSwitchClean
    {
        private readonly Dictionary<string, IShippingCalculator> _shippingCalculators;

        public LongSwitchClean()
        {
            _shippingCalculators = new Dictionary<string, IShippingCalculator>
            {
                { "standard", new StandardShipping() },
                { "express", new ExpressShipping() },
                { "overnight", new OvernightShipping() },
                { "same_day", new SameDayShipping() }
            };
        }

        public decimal CalculateShipping(string shippingMethod, decimal weight, string destination)
        {
            if (!_shippingCalculators.TryGetValue(shippingMethod.ToLower(), out var calculator))
                throw new ArgumentException("Unknown shipping method");

            if (!calculator.IsAvailable(destination))
                return -1;

            return calculator.CalculateShipping(weight, destination);
        }

        public string GetShippingTime(string shippingMethod)
        {
            if (!_shippingCalculators.TryGetValue(shippingMethod.ToLower(), out var calculator))
                return "Unknown";

            return calculator.GetShippingTime();
        }
    }
}
