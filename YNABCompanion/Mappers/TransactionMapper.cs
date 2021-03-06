﻿namespace YNABCompanion.Mappers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using YNABCompanion.Enums;
    using YNABCompanion.Models;
    using YNABCompanion.Properties;
    using YNABCompanion.ViewModels;

    public class TransactionMapper : ITransactionMapper
    {
        private static readonly Regex TransactionRegex = new Regex("^(?:[^,]+,){4}[^,]+$");

        private static string[] OutboundPaymentStatuses = new[] { "Card Purchase" };

        private static string[] InboundPaymentStatuses = new[] { "Inward Payment", "Card Purchase Refund" };

        private static string[] CashWithdrawalStatuses = new[] { "ATM Cash Withdrawal" };

        public IEnumerable<TransactionViewModel> Map(IEnumerable<string> lines, decimal balance)
        {
            var transactionLines = lines.Skip(1).Reverse().Where(t => TransactionRegex.IsMatch(t)).ToList();
            var bankTransactions = transactionLines.Select(Map);
            var viewModels = new List<TransactionViewModel>();

            foreach (var dateGroup in bankTransactions.GroupBy(bt => bt.Date))
            {
                foreach (var refGroup in dateGroup.GroupBy(dg => dg.Reference))
                {
                    if (refGroup.All(t => t.TransactionType == TransactionType.OutboundPayment))
                    {
                        viewModels.Add(new TransactionViewModel(refGroup.ToList()));
                    }
                    else
                    {
                        var models = refGroup.Select(t => new TransactionViewModel(t));
                        viewModels.AddRange(models);
                    }
                }
            }

            return viewModels.OrderBy(vm => vm.Date).ThenByDescending(vm => vm.Value);
        }

        public YNABTransaction Map(TransactionViewModel transaction)
        {
            return new YNABTransaction
            {
                Amount = transaction.Value,
                Date = transaction.Date.ToString("dd/MM/yyyy"),
                Memo = transaction.Memo
            };
        }

        public void MapBalances(IEnumerable<TransactionViewModel> transactions, decimal balance)
        {
            foreach (var transaction in transactions)
            {
                transaction.SetBalance(balance + transaction.Value);
                balance = transaction.Balance;
            }
        }

        private static BankTransaction Map(string transaction)
        {
            var fields = transaction.Split(',');

            return new BankTransaction
            {
                Date = DateTime.Parse(fields[0]),
                Reference = fields[1],
                TransactionType = GetTransactionType(fields[2].Trim()),
                Value = GetValue(fields[3], fields[4]),
            };
        }

        private static TransactionType GetTransactionType(string transactionType)
        {
            if (OutboundPaymentStatuses.Contains(transactionType))
            {
                return TransactionType.OutboundPayment;
            }

            if (InboundPaymentStatuses.Contains(transactionType))
            {
                return TransactionType.InboundPayment;
            }

            if (CashWithdrawalStatuses.Contains(transactionType))
            {
                return TransactionType.CashWithdrawal;
            }

            throw new ArgumentOutOfRangeException(
                        nameof(transactionType),
                        Resources.Error_BankTransactionMapper_GetTransactionType_ParsingFailure);
        }

        private static decimal GetValue(string inbound, string outbound)
        {
            var moneyIn = decimal.Parse(inbound);
            var moneyOut = decimal.Parse(outbound);

            return moneyIn - moneyOut;
        }
    }
}
