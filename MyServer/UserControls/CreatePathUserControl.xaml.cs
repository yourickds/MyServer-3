using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using MyServer.Stores;
using MyServer.ViewModels;

namespace MyServer.UserControls
{
    /// <summary>
    /// Логика взаимодействия для CreatePathUserControl.xaml
    /// </summary>
    public partial class CreatePathUserControl : UserControl
    {
        private readonly CreatePathViewModel _viewModel;

        public CreatePathUserControl()
        {
            InitializeComponent();
            _viewModel = new CreatePathViewModel();
            DataContext = _viewModel;
        }

        private void CreatePath(object sender, System.Windows.RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(_viewModel.Name))
            {
                MessageBox.Show("Field `Name` is required");
                return;
            }

            if (String.IsNullOrEmpty(_viewModel.Dir))
            {
                MessageBox.Show("Field `Directory` is required");
                return;
            }

            if (!Directory.Exists(_viewModel.Dir))
            {
                MessageBox.Show("Directory Not Found in `Dir`");
                return;
            }

            Models.Path newPath = new()
            {
                Name = _viewModel.Name,
                Dir = _viewModel.Dir
            };

            // Проверяем перед добавлением
            if (PathStore.Instance.Paths.Any(s => s.Name == newPath.Name))
            {
                MessageBox.Show("Name already exists!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Прерываем выполнение
            }

            PathStore.Instance.AddPath(newPath);
            MessageBox.Show("Added Path");
        }

        private void OpenDialogDirectoryPath(object sender, RoutedEventArgs e)
        {
            OpenFolderDialog openFolderDialog = new();

            if (openFolderDialog.ShowDialog() == true)
            {
                _viewModel.Dir = openFolderDialog.FolderName;
            }
        }
    }
}
