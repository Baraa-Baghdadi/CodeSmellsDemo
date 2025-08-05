namespace CodeSmellsDemo.LongSwitchSmell
{
    public class LongSwitchSmell
    {
        public decimal CalculateShipping(string shippingMethod, decimal weight, string destination)
        {
            switch (shippingMethod.ToLower())
            {
                case "standard":
                    if (destination == "local")
                        return weight * 2.0m;
                    else if (destination == "regional")
                        return weight * 4.0m;
                    else
                        return weight * 8.0m;

                case "express":
                    if (destination == "local")
                        return weight * 5.0m;
                    else if (destination == "regional")
                        return weight * 10.0m;
                    else
                        return weight * 20.0m;

                case "overnight":
                    if (destination == "local")
                        return weight * 10.0m;
                    else if (destination == "regional")
                        return weight * 25.0m;
                    else
                        return weight * 50.0m;

                case "same_day":
                    if (destination == "local")
                        return weight * 15.0m;
                    else
                        return -1; // غير متاح للمناطق البعيدة

                default:
                    throw new ArgumentException("Unknown shipping method");
            }
        }

        public string GetShippingTime(string shippingMethod)
        {
            switch (shippingMethod.ToLower())
            {
                case "standard":
                    return "5-7 business days";
                case "express":
                    return "2-3 business days";
                case "overnight":
                    return "Next business day";
                case "same_day":
                    return "Same day";
                default:
                    return "Unknown";
            }
        }
    }
}
