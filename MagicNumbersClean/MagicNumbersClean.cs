namespace CodeSmellsDemo.MagicNumbersClean
{
    public class MagicNumbersClean
    {
        // Constants توضح المعنى
        private const decimal VIP_HIGH_DISCOUNT = 0.15m;
        private const decimal VIP_LOW_DISCOUNT = 0.10m;
        private const decimal PREMIUM_HIGH_DISCOUNT = 0.08m;
        private const decimal PREMIUM_LOW_DISCOUNT = 0.05m;
        private const decimal REGULAR_DISCOUNT = 0.02m;

        private const decimal VIP_THRESHOLD = 1000m;
        private const decimal PREMIUM_THRESHOLD = 500m;

        private const int MIN_AGE = 18;
        private const int MAX_AGE = 65;

        public decimal CalculateDiscount(decimal amount, string customerType)
        {
            if (customerType == "VIP")
            {
                if (amount > VIP_THRESHOLD) return amount * VIP_HIGH_DISCOUNT;
                return amount * VIP_LOW_DISCOUNT;
            }

            if (customerType == "Premium")
            {
                if (amount > PREMIUM_THRESHOLD) return amount * PREMIUM_HIGH_DISCOUNT;
                return amount * PREMIUM_LOW_DISCOUNT;
            }

            return amount * REGULAR_DISCOUNT;
        }

        public bool IsValidAge(int age)
        {
            return age >= MIN_AGE && age <= MAX_AGE;
        }
    }
}
