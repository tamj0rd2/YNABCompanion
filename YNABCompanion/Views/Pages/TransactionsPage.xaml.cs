namespace YNABCompanion.Views.Pages
{
    using System.IO;
    using System.Windows;

    using YNABCompanion.Mappers;
    using YNABCompanion.ViewModels;

    /// <summary>
    /// Interaction logic for TransactionsPage.xaml
    /// </summary>
    public partial class TransactionsPage
    {
        private readonly TransactionsPageViewModel viewModel;

        private readonly TransactionMapper transactionMapper;

        public TransactionsPage()
        {
            this.transactionMapper = new TransactionMapper();
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
                this.viewModel.Transactions.Clear();

                var lines = File.ReadAllLines(dialog.FileName);
                var transactions = this.transactionMapper.Map(lines, this.viewModel.BankBalance);

                this.viewModel.Transactions.AddRange(transactions);
                this.TransactionsList.Items.Refresh();
            }
        }

        private void UpdateBalanceClick(object sender, RoutedEventArgs e)
        {
            this.transactionMapper.MapBalances(this.viewModel.Transactions, this.viewModel.BankBalance);
            this.TransactionsList.Items.Refresh();
        }
    }
}
