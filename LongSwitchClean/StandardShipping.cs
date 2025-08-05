namespace CodeSmellsDemo.LongSwitchClean
{
    public class StandardShipping : IShippingCalculator
    {
        public decimal CalculateShipping(decimal weight, string destination)
        {
            return destination switch
            {
                "local" => weight * 2.0m,
                "regional" => weight * 4.0m,
                _ => weight * 8.0m
            };
        }

        public string GetShippingTime() => "5-7 business days";
        public bool IsAvailable(string destination) => true;
    }
}
