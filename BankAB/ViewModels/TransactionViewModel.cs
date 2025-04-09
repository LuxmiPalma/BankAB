namespace BankAB.ViewModels
{
    public class TransactionViewModel
    {
        public int TransactionId { get; set; }
        public int AccountId { get; set; }
        public int CustomerId { get; set; }
        public DateOnly Date { get; set; }
        public decimal Amount { get; set; }

        public string Operation { get; set; }
    }
}
