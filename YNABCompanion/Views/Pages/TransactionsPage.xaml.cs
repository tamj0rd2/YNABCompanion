namespace YNABCompanion.Views.Pages
{
    using CsvHelper;
    using System;
    using System.IO;
    using System.Linq;
    using System.Windows;

    using YNABCompanion.Mappers;
    using YNABCompanion.Services;
    using YNABCompanion.ViewModels;

    /// <summary>
    /// Interaction logic for TransactionsPage.xaml
    /// </summary>
    public partial class TransactionsPage
    {
        private readonly TransactionsPageViewModel viewModel;

        private readonly ITransactionMapper transactionMapper;

        private readonly ITransactionIO transactionIOService;

        public TransactionsPage()
        {
            this.transactionMapper = new TransactionMapper();
            this.transactionIOService = new TransactionIO();
            this.InitializeComponent();

            this.viewModel = new TransactionsPageViewModel();
            this.DataContext = this.viewModel;
        }

        private void LoadTransactionsClick(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = "*.csv",
                Filter = "Supported extensions (*.csv)|*.csv"
            };

            if (dialog.ShowDialog() == true)
            {
                var lines = this.transactionIOService.ReadLines(dialog.FileName);
                var transactions = this.transactionMapper.Map(lines, this.viewModel.BankBalance);
                this.viewModel.SetTransactions(transactions);
            }
        }

        private void ExportTransactionsClick(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog
            {
                DefaultExt = "*.csv",
                Filter = "Supported extensions (*.csv)|*.csv",
                FileName = string.Format("{0} transaction export", DateTime.UtcNow.ToString("dd-MM-yyyy"))
            };

            if (dialog.ShowDialog() == true)
            {
                var ynabTransactions = this.viewModel.Transactions.Select(t => this.transactionMapper.Map(t));
                this.transactionIOService.WriteLines(dialog.FileName, ynabTransactions);
            }
        }

        private void UpdateBalanceClick(object sender, RoutedEventArgs e)
        {
            this.transactionMapper.MapBalances(this.viewModel.Transactions, this.viewModel.BankBalance);
        }
    }
}
