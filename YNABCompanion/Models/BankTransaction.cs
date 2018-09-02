namespace YNABCompanion.Models
{
    using System;

    using YNABCompanion.Enums;

    public class BankTransaction
    {
        public DateTime Date { get; set; }

        public string Reference { get; set; }

        public TransactionType TransactionType { get; set; }

        public decimal Value { get; set; }
    }
}
