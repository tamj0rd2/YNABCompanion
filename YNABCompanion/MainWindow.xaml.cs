namespace YNABCompanion
{
    using YNABCompanion.Views.Pages;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            this.InitializeComponent();
            this.MainFrame.Navigate(new TransactionsPage());
        }
    }
}