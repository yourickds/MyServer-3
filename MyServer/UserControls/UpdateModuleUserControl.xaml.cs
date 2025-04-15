using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using MyServer.Models;
using MyServer.Stores;
using MyServer.ViewModels;

namespace MyServer.UserControls
{
    /// <summary>
    /// Логика взаимодействия для UpdateModuleUserControl.xaml
    /// </summary>
    public partial class UpdateModuleUserControl : UserControl
    {
        private readonly UpdateModuleViewModel _viewModel;

        private Module SelectedModule;

        public UpdateModuleUserControl(Module selectedModule)
        {
            InitializeComponent();
            _viewModel = new UpdateModuleViewModel(selectedModule);
            SelectedModule = selectedModule;
            DataContext = _viewModel;
        }

        private void UpdateModule(object sender, System.Windows.RoutedEventArgs e)
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

            bool nameExists = ModuleStore.Instance.Modules
                .Any(s => s.Name == _viewModel.Name && s.Id != SelectedModule.Id);

            if (nameExists)
            {
                MessageBox.Show("Name already exists!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Прерываем выполнение
            }

            SelectedModule.Name = _viewModel.Name;
            SelectedModule.Dir = _viewModel.Dir;
            SelectedModule.Variable = _viewModel.Variable;

            ModuleStore.Instance.UpdateModule(SelectedModule);
            MessageBox.Show("Update Module");
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
