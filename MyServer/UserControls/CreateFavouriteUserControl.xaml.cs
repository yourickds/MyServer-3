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
    /// Логика взаимодействия для CreateFavouriteUserControl.xaml
    /// </summary>
    public partial class CreateFavouriteUserControl : UserControl
    {
        private readonly CreateFavouriteViewModel _viewModel;

        public CreateFavouriteUserControl()
        {
            InitializeComponent();
            _viewModel = new CreateFavouriteViewModel();
            DataContext = _viewModel;
        }

        private void CreateFavourite(object sender, System.Windows.RoutedEventArgs e)
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

            // Реалиизовать проверку на наличие файла или корректность url адреса http:// or https://
            //if (!File.Exists(_viewModel.FilePath) && !Uri.CheckSchemeName(_viewModel.FilePath))
            //{
            //    MessageBox.Show("File or Url Not Found in `FilePath`");
            //    return;
            //}

            Favourite newFavourite = new()
            {
                Name = _viewModel.Name,
                FilePath = _viewModel.FilePath,
                Arguments = _viewModel.Arguments,
                InBrowser = _viewModel.InBrowser,
            };

            // Проверяем перед добавлением
            if (FavouriteStore.Instance.Favourites.Any(s => s.Name == newFavourite.Name))
            {
                MessageBox.Show("Name already exists!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Прерываем выполнение
            }

            FavouriteStore.Instance.AddFavourite(newFavourite);
            MessageBox.Show("Added Favourite");
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
