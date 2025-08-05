namespace CodeSmellsDemo.LargeClassClean
{
    public class EmailService : IEmailService
    {
        private readonly List<string> _emailQueue = new();
        private readonly Dictionary<string, bool> _emailStatus = new();

        public void SendWelcomeEmail(string email)
        {
            _emailQueue.Add($"Welcome: {email}");
            _emailStatus[email] = false;
        }

        public void SendOrderConfirmation(string email, string orderId)
        {
            _emailQueue.Add($"Order Confirmation: {email} - {orderId}");
            _emailStatus[$"{email}-{orderId}"] = false;
        }

        public void ProcessEmailQueue()
        {
            foreach (var email in _emailQueue.ToList())
            {
                Console.WriteLine($"Sending: {email}");
                _emailQueue.Remove(email);
            }
        }
    }
}
