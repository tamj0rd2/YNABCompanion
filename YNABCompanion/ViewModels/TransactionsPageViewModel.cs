namespace YNABCompanion.ViewModels
{
    using System.Collections.Generic;

    public class TransactionsPageViewModel
    {
        public TransactionsPageViewModel()
        {
            this.Transactions = new List<TransactionViewModel>();
        }

        public decimal BankBalance { get; set; }

        public List<TransactionViewModel> Transactions { get; set; }
    }
}
