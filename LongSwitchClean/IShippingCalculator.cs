using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmellsDemo.LongSwitchClean
{
    public interface IShippingCalculator
    {
        decimal CalculateShipping(decimal weight, string destination);
        string GetShippingTime();
        bool IsAvailable(string destination);
    }
}
