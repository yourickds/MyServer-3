using System.Windows;
using System.Windows.Controls;
using MyServer.ViewModels;
using MyServer.Models;
using MyServer.Stores;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.IO;

namespace MyServer.UserControls
{
    /// <summary>
    /// Логика взаимодействия для CreateServiceUserControl.xaml
    /// </summary>
    public partial class CreateServiceUserControl : UserControl
    {
        private readonly CreateServiceViewModel _viewModel;

        public CreateServiceUserControl()
        {
            InitializeComponent();
            _viewModel = new CreateServiceViewModel();
            DataContext = _viewModel;
        }

        private void CreateService(object sender, System.Windows.RoutedEventArgs e)
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

            Service newService = new()
            {
                Name = _viewModel.Name,
                FilePath = _viewModel.FilePath,
                Arguments = _viewModel.Arguments,
                Startup = _viewModel.Startup
            };

            // Проверяем перед добавлением
            if (ServiceStore.Instance.Services.Any(s => s.Name == newService.Name))
            {
                MessageBox.Show("Name already exists!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Прерываем выполнение
            }

            ServiceStore.Instance.AddService(newService);
            MessageBox.Show("Added Service");
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
    }
}
