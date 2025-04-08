using System.Windows;
using System.Windows.Controls;
using MyServer.Stores;
using MyServer.ViewModels;
using MyServer.Models;

namespace MyServer.UserControls
{
    /// <summary>
    /// Логика взаимодействия для ServiceUserControl.xaml
    /// </summary>
    public partial class ServiceUserControl : UserControl
    {
        private readonly ServiceViewModel _viewModel;

        public ServiceUserControl()
        {
            InitializeComponent();
            _viewModel = new ServiceViewModel();
            DataContext = _viewModel;
        }

        private void CreateService(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.SelectedService = null;
        }

        private void DeleteService(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_viewModel.SelectedService != null && _viewModel.SelectedService is Service)
            {
                ServiceStore.Instance.DeleteService(_viewModel.SelectedService.Id);
            }
            else
            {
                MessageBox.Show("Selected Service");
            }
        }
    }
}
