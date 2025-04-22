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
                // Проверяем есть ли у профиля домены
                if (_viewModel.SelectedProfile.Domains.Count > 0)
                {
                    MessageBox.Show("Нельзя удалить профиль если он привязан к доменам!", "Удаление профиля", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить профиль ?", "Удаление профиля", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    ProfileStore.Instance.DeleteProfile(_viewModel.SelectedProfile.Id);
                }
            }
            else
            {
                MessageBox.Show("Selected Profile");
            }
        }
    }
}
