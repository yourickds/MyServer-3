using System.Windows;
using System.Windows.Controls;
using MyServer.Stores;
using MyServer.ViewModels;
using MyServer.Models;
using MyServer.Actions;

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
                //Подтверждение на удаление
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить службу ?", "Удаление службы", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // Проверяем состояние службы
                    if (GetStatusService.Invoke(_viewModel.SelectedService))
                    {
                        // Останавливаем службу
                        GetStopService.Invoke(_viewModel.SelectedService);
                    }
                    ServiceStore.Instance.DeleteService(_viewModel.SelectedService.Id);
                }
            }
            else
            {
                MessageBox.Show("Selected Service");
            }
        }
    }
}
