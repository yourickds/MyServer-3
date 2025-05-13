using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using MyServer.Stores;
using MyServer.ViewModels;

namespace MyServer.UserControls
{
    /// <summary>
    /// Логика взаимодействия для UpdatePathUserControl.xaml
    /// </summary>
    public partial class UpdatePathUserControl : UserControl
    {
        private readonly UpdatePathViewModel _viewModel;

        private Models.Path SelectedPath;
        public UpdatePathUserControl(Models.Path selectedPath)
        {
            InitializeComponent();
            _viewModel = new UpdatePathViewModel(selectedPath);
            SelectedPath = selectedPath;
            DataContext = _viewModel;
        }

        private void UpdatePath(object sender, System.Windows.RoutedEventArgs e)
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
                MessageBox.Show("Directory Not Found in `Directory`");
                return;
            }

            bool nameExists = PathStore.Instance.Paths
                .Any(s => s.Name == _viewModel.Name && s.Id != SelectedPath.Id);

            if (nameExists)
            {
                MessageBox.Show("Name already exists!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Прерываем выполнение
            }

            SelectedPath.Name = _viewModel.Name;
            SelectedPath.Dir = _viewModel.Dir;

            PathStore.Instance.UpdatePath(SelectedPath);
            MessageBox.Show("Update Path");
        }

        private void OpenDialogDirectoryPath(object sender, RoutedEventArgs e)
        {
            // Создаем экземпляр OpenFileDialog
            OpenFolderDialog OpenFolderDialog = new();

            // Показываем диалог и проверяем, был ли выбран файл
            if (OpenFolderDialog.ShowDialog() == true)
            {
                // Если файл был выбран, обновляем текст в TextBox
                _viewModel.Dir = OpenFolderDialog.FolderName;
            }
        }
    }
}
