using System.Diagnostics;
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
    /// Логика взаимодействия для UpdateFavouriteUserControl.xaml
    /// </summary>
    public partial class UpdateFavouriteUserControl : UserControl
    {
        private readonly UpdateFavouriteViewModel _viewModel;

        private Favourite SelectedFavourite;

        public UpdateFavouriteUserControl(Favourite selectedFavourite)
        {
            InitializeComponent();
            _viewModel = new UpdateFavouriteViewModel(selectedFavourite);
            this.SelectedFavourite = selectedFavourite;
            DataContext = _viewModel;
        }

        private void UpdateFavourite(object sender, System.Windows.RoutedEventArgs e)
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

            /*if (!File.Exists(_viewModel.FilePath))
            {
                MessageBox.Show("File Not Found in `FilePath`");
                return;
            }*/

            bool nameExists = FavouriteStore.Instance.Favourites
                .Any(s => s.Name == _viewModel.Name && s.Id != SelectedFavourite.Id);

            if (nameExists)
            {
                MessageBox.Show("Name already exists!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Прерываем выполнение
            }

            SelectedFavourite.Name = _viewModel.Name;
            SelectedFavourite.FilePath = _viewModel.FilePath;
            SelectedFavourite.Arguments = _viewModel.Arguments;
            SelectedFavourite.InBrowser = _viewModel.InBrowser;

            FavouriteStore.Instance.UpdateFavourite(SelectedFavourite);
            MessageBox.Show("Update Favourite");
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
