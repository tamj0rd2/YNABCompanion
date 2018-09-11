namespace YNABCompanion.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    public class TransactionsPageViewModel : BaseViewModel
    {
        public TransactionsPageViewModel()
        {
            this.Transactions = new List<TransactionViewModel>();
        }

        public decimal BankBalance { get; set; }

        public IEnumerable<TransactionViewModel> Transactions { get; private set; }

        public bool IsExportButtonEnabled => this.Transactions.Any();

        public void SetTransactions(IEnumerable<TransactionViewModel> transactions)
        {
            this.Transactions = transactions;
            this.OnPropertyChanged(nameof(this.Transactions));
            this.OnPropertyChanged(nameof(this.IsExportButtonEnabled));
        }
    }
}
