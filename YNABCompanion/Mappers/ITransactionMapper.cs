namespace YNABCompanion.Mappers
{
    using System.Collections.Generic;

    using YNABCompanion.ViewModels;

    public interface ITransactionMapper
    {
        IEnumerable<TransactionViewModel> Map(IEnumerable<string> transactionLines, decimal startingBalance);

        void MapBalances(IEnumerable<TransactionViewModel> transactions, decimal balance);
    }
}
