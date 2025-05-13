using System.Windows;
using System.Windows.Controls;
using MyServer.Models;
using MyServer.Stores;
using MyServer.ViewModels;

namespace MyServer.UserControls
{
    /// <summary>
    /// Логика взаимодействия для HostUserControl.xaml
    /// </summary>
    public partial class HostUserControl : UserControl
    {
        private readonly HostViewModel _viewModel;
        public HostUserControl()
        {
            InitializeComponent();
            _viewModel = new HostViewModel();
            DataContext = _viewModel;
        }

        private void CreateHost(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.SelectedHost = null;
        }

        private void DeleteHost(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_viewModel.SelectedHost != null && _viewModel.SelectedHost is Host)
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить host ?", "Удаление host", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    HostStore.Instance.DeleteHost(_viewModel.SelectedHost.Id);
                }
            }
            else
            {
                MessageBox.Show("Selected Host");
            }
        }
    }
}
