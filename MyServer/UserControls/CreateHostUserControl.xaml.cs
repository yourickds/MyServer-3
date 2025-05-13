using System.Windows;
using System.Windows.Controls;
using MyServer.Actions;
using MyServer.Models;
using MyServer.Stores;
using MyServer.ViewModels;

namespace MyServer.UserControls
{
    /// <summary>
    /// Логика взаимодействия для CreateHostUserControl.xaml
    /// </summary>
    public partial class CreateHostUserControl : UserControl
    {
        private readonly CreateHostViewModel _viewModel;
        public CreateHostUserControl()
        {
            InitializeComponent();
            _viewModel = new CreateHostViewModel();
            DataContext = _viewModel;
        }

        private void CreateHost(object sender, System.Windows.RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(_viewModel.Name))
            {
                MessageBox.Show("Field `Name` is required");
                return;
            }

            if (String.IsNullOrEmpty(_viewModel.Ip))
            {
                MessageBox.Show("Field `Ip` is required");
                return;
            }

            if (!System.Net.IPAddress.TryParse(_viewModel.Ip, out var address))
            {
                MessageBox.Show("Field `IpHost` is ip address");
                return;
            }

            Host newHost = new()
            {
                Name = _viewModel.Name,
                Ip = _viewModel.Ip
            };

            // Проверяем перед добавлением
            if (HostStore.Instance.Hosts.Any(s => s.Name == newHost.Name))
            {
                MessageBox.Show("Name already exists!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Прерываем выполнение
            }

            RemoveAllLoopbackIps.Invoke();
            HostStore.Instance.AddHost(newHost);
            MessageBox.Show("Added Host");
        }
    }
}
