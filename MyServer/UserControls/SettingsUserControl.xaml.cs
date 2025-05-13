using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using MyServer.Actions;
using MyServer.Stores;
using MyServer.ViewModels;

namespace MyServer.UserControls
{
    /// <summary>
    /// Логика взаимодействия для SettingsUserControl.xaml
    /// </summary>
    public partial class SettingsUserControl : UserControl
    {
        private readonly SettingViewModel _viewModel;

        public SettingsUserControl()
        {
            InitializeComponent();
            _viewModel = new SettingViewModel();
            DataContext = _viewModel;
        }

        private void GenerateConfigSerives(object sender, RoutedEventArgs e)
        {
            RegenerateAllConfigs.Invoke();
            // Перезапускаем службы
            Actions.RestartWorkServices.Invoke();
        }

        private void OpenHostsFile(object sender, RoutedEventArgs e)
        {
            var notepad = new Process();
            notepad.StartInfo.FileName = "notepad.exe";
            notepad.StartInfo.Arguments = Environment.GetFolderPath(Environment.SpecialFolder.Windows) + "/System32/drivers/etc/hosts";
            notepad.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            notepad.StartInfo.Verb = "runas";
            notepad.StartInfo.UseShellExecute = true;
            notepad.Start();
        }

        private void RestartWorkServices(object sender, RoutedEventArgs e)
        {
            Actions.RestartWorkServices.Invoke();
        }

        private void OpenDialogDirectoryPath(object sender, RoutedEventArgs e)
        {
            OpenFolderDialog openFolderDialog = new();

            if (openFolderDialog.ShowDialog() == true)
            {
                _viewModel.NamePath = openFolderDialog.FolderName;
            }
        }

        private void AddPath(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(_viewModel.NamePath))
            {
                MessageBox.Show("Field `NamePath` is required");
                return;
            }

            if (!Directory.Exists(_viewModel.NamePath))
            {
                MessageBox.Show("Directory Not Found");
                return;
            }

            Models.Path newPath = new()
            {
                Name = _viewModel.NamePath,
            };

            // Проверяем перед добавлением
            if (SettingStore.Instance.Paths.Any(p => p.Name == newPath.Name))
            {
                MessageBox.Show("Path already exists!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Прерываем выполнение
            }

            SettingStore.Instance.AddPath(newPath);
            MessageBox.Show("Added Path");
        }

        private void AddHost(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(_viewModel.DomainHost))
            {
                MessageBox.Show("Field `DomainHost` is required");
                return;
            }

            if (String.IsNullOrEmpty(_viewModel.IpHost))
            {
                MessageBox.Show("Field `IpHost` is required");
                return;
            }

            if (!System.Net.IPAddress.TryParse(_viewModel.IpHost, out var address))
            {
                MessageBox.Show("Field `IpHost` is ip address");
                return;
            }

            Models.Host newHost = new()
            {
                Name = _viewModel.DomainHost,
                Ip = _viewModel.IpHost,
            };

            // Проверяем перед добавлением
            if (SettingStore.Instance.Hosts.Any(h => h.Name == newHost.Name))
            {
                MessageBox.Show("Host already exists!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Прерываем выполнение
            }

            SettingStore.Instance.AddHost(newHost);
            // Пересоздаем файл Hosts
            ClearDomainHosts.Invoke();
            SetDomainHosts.Invoke();
            MessageBox.Show("Added Host");
        }

        private void DeletePath(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedPath != null && _viewModel.SelectedPath is Models.Path)
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить Path?", "Удаление Path", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    SettingStore.Instance.DeletePath(_viewModel.SelectedPath.Id);
                }
            }
            else
            {
                MessageBox.Show("Selected Path");
            }
        }

        private void DeleteHost(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedHost != null && _viewModel.SelectedHost is Models.Host)
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить Host?", "Удаление Host", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    RemoveLoopbackIp.Invoke(_viewModel.SelectedHost.Ip);
                    SettingStore.Instance.DeleteHost(_viewModel.SelectedHost.Id);
                    // Пересоздаем файл Hosts
                    ClearDomainHosts.Invoke();
                    SetDomainHosts.Invoke();
                }
            }
            else
            {
                MessageBox.Show("Selected Host");
            }
        }
    }
}
