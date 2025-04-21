using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;
using MyServer.ViewModels;

namespace MyServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainWindowViewModel();
            DataContext = _viewModel;
        }

        private void OpenLinkAuthor(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri)
            {
                UseShellExecute = true
            });
            e.Handled = true;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            // Отменяем стандартное закрытие окна
            e.Cancel = true;

            // Скрываем окно вместо закрытия
            this.Hide();

            base.OnClosing(e);
        }
    }
}