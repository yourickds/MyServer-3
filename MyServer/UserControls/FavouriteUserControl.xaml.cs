using System.Windows;
using System.Windows.Controls;
using MyServer.Models;
using MyServer.Stores;
using MyServer.ViewModels;

namespace MyServer.UserControls
{
    /// <summary>
    /// Логика взаимодействия для FavouriteUserControl.xaml
    /// </summary>
    public partial class FavouriteUserControl : UserControl
    {
        private readonly FavouriteViewModel _viewModel;

        public FavouriteUserControl()
        {
            InitializeComponent();
            _viewModel = new FavouriteViewModel();
            DataContext = _viewModel;
        }

        private void CreateFavourite(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.SelectedFavourite = null;
        }

        private void DeleteFavourite(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_viewModel.SelectedFavourite != null && _viewModel.SelectedFavourite is Favourite)
            {
                FavouriteStore.Instance.DeleteFavourite(_viewModel.SelectedFavourite.Id);
            }
            else
            {
                MessageBox.Show("Selected Favourite");
            }
        }
    }
}
