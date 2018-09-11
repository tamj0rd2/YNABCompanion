namespace YNABCompanion.Mappers
{
    using System.Collections.Generic;
    using YNABCompanion.Models;
    using YNABCompanion.ViewModels;

    public interface ITransactionMapper
    {
        IEnumerable<TransactionViewModel> Map(IEnumerable<string> transactionLines, decimal startingBalance);

        void MapBalances(IEnumerable<TransactionViewModel> transactions, decimal balance);

        YNABTransaction Map(TransactionViewModel transaction);
    }
}
