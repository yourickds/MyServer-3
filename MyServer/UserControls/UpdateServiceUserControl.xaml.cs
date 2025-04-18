using System.Windows.Controls;
using MyServer.ViewModels;
using MyServer.Models;
using System.Windows;
using MyServer.Stores;
using System.IO;
using Microsoft.Win32;
using MyServer.Actions;

namespace MyServer.UserControls
{
    /// <summary>
    /// Логика взаимодействия для UpdateServiceUserControl.xaml
    /// </summary>
    public partial class UpdateServiceUserControl : UserControl
    {
        private readonly UpdateServiceViewModel _viewModel;

        private Service SelectedService;

        public UpdateServiceUserControl(Service SelectedService)
        {
            InitializeComponent();
            _viewModel = new UpdateServiceViewModel(SelectedService);
            this.SelectedService = SelectedService;
            DataContext = _viewModel;
        }

        private void UpdateService(object sender, System.Windows.RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(_viewModel.Name))
            {
                MessageBox.Show("Field `Name` is required");
                return;
            }

            if (String.IsNullOrEmpty(_viewModel.FilePath))
            {
                MessageBox.Show("Field `FilePath` is required");
                return;
            }

            if (!File.Exists(_viewModel.FilePath))
            {
                MessageBox.Show("File Not Found in `FilePath`");
                return;
            }

            bool nameExists = ServiceStore.Instance.Services
                .Any(s => s.Name == _viewModel.Name && s.Id != SelectedService.Id);

            if (nameExists)
            {
                MessageBox.Show("Name already exists!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Прерываем выполнение
            }

            SelectedService.Name = _viewModel.Name;
            SelectedService.FilePath = _viewModel.FilePath;
            SelectedService.Arguments = _viewModel.Arguments;
            SelectedService.Startup = _viewModel.Startup;

            ServiceStore.Instance.UpdateService(SelectedService);
            MessageBox.Show("Update Service");
        }

        private void OpenDialogFilePath(object sender, RoutedEventArgs e)
        {
            // Создаем экземпляр OpenFileDialog
            OpenFileDialog openFileDialog = new();

            // Показываем диалог и проверяем, был ли выбран файл
            if (openFileDialog.ShowDialog() == true)
            {
                // Если файл был выбран, обновляем текст в TextBox
                _viewModel.FilePath = openFileDialog.FileName;
            }
        }

        private void ToggleService(object sender, RoutedEventArgs e)
        {
            if (GetStatusService.Invoke(SelectedService))
                GetStopService.Invoke(SelectedService);
            else
                GetStartService.Invoke(SelectedService);
        }
    }
}
