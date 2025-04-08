using MyServer.Models;
using MyServer.Stores;
using MyServer.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace MyServer.UserControls
{
    /// <summary>
    /// Логика взаимодействия для ProfileUserControl.xaml
    /// </summary>
    public partial class ProfileUserControl : UserControl
    {
        private readonly ProfileViewModel _viewModel;

        public ProfileUserControl()
        {
            InitializeComponent();
            _viewModel = new ProfileViewModel();
            DataContext = _viewModel;
        }

        private void CreateProfile(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.SelectedProfile = null;
        }

        private void DeleteProfile(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_viewModel.SelectedProfile != null && _viewModel.SelectedProfile is Profile)
            {
                ProfileStore.Instance.DeleteProfile(_viewModel.SelectedProfile.Id);
            }
            else
            {
                MessageBox.Show("Selected Profile");
            }
        }
    }
}
