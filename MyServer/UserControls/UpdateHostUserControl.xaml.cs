using System.IO;
using System.Windows;
using System.Windows.Controls;
using MyServer.Actions;
using MyServer.Models;
using MyServer.Stores;
using MyServer.ViewModels;

namespace MyServer.UserControls
{
    /// <summary>
    /// Логика взаимодействия для UpdateHostUserControl.xaml
    /// </summary>
    public partial class UpdateHostUserControl : UserControl
    {
        private readonly UpdateHostViewModel _viewModel;

        private Host SelectedHost;
        public UpdateHostUserControl(Host selectedHost)
        {
            InitializeComponent();
            _viewModel = new UpdateHostViewModel(selectedHost);
            SelectedHost = selectedHost;
            DataContext = _viewModel;
        }

        private void UpdateHost(object sender, System.Windows.RoutedEventArgs e)
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

            bool nameExists = HostStore.Instance.Hosts
                .Any(s => s.Name == _viewModel.Name && s.Id != SelectedHost.Id);

            if (nameExists)
            {
                MessageBox.Show("Name already exists!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Прерываем выполнение
            }

            RemoveAllLoopbackIps.Invoke();
            SelectedHost.Name = _viewModel.Name;
            SelectedHost.Ip = _viewModel.Ip;
            HostStore.Instance.UpdateHost(SelectedHost);
            MessageBox.Show("Update Host");
        }
    }
}
