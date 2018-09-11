namespace YNABCompanion.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using YNABCompanion.Enums;
    using YNABCompanion.Models;

    public class TransactionViewModel : BaseViewModel
    {
        public TransactionViewModel(BankTransaction transaction) : this(new List<BankTransaction> { transaction })
        {
        }

        public TransactionViewModel(List<BankTransaction> transactions)
        {
            this.Transactions = transactions;

            this.Date = this.Transactions[0].Date;
            this.Reference = this.Transactions[0].Reference;
            this.Type = this.Transactions[0].TransactionType;
            this.Value = transactions.Sum(t => t.Value);
        }

        public DateTime Date { get; }

        public string Reference { get;  }

        public TransactionType Type { get; }

        public decimal Value { get; }

        public string DisplayedValue => this.Value.ToString(CultureInfo.InvariantCulture).TrimStart('-');

        public string ValueColour => this.Value >= 0 ? "Green" : "Red";

        public decimal Balance { get; private set; }

        public string Memo { get; set; }

        public List<BankTransaction> Transactions { get; }

        public void SetBalance(decimal balance)
        {
            this.Balance = balance;
            this.OnPropertyChanged(nameof(this.Balance));
        }
    }
}