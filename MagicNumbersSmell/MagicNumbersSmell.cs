namespace CodeSmellsDemo.MagicNumbersSmell
{
    public class MagicNumbersSmell
    {
        public decimal CalculateDiscount(decimal amount, string customerType)
        {
            if (customerType == "VIP")
            {
                if (amount > 1000) return amount * 0.15m; // ما معنى 0.15؟
                return amount * 0.10m; // ما معنى 0.10؟
            }

            if (customerType == "Premium")
            {
                if (amount > 500) return amount * 0.08m; // ما معنى 0.08؟
                return amount * 0.05m; // ما معنى 0.05؟
            }

            return amount * 0.02m; // ما معنى 0.02؟
        }

        public bool IsValidAge(int age)
        {
            return age >= 18 && age <= 65; // أرقام سحرية
        }
    }
}
