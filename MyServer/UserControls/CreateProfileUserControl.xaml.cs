using MyServer.Models;
using MyServer.Stores;
using MyServer.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace MyServer.UserControls
{
    /// <summary>
    /// Логика взаимодействия для CreateProfileUserControl.xaml
    /// </summary>
    public partial class CreateProfileUserControl : UserControl
    {
        private readonly CreateProfileViewModel _viewModel;

        public CreateProfileUserControl()
        {
            InitializeComponent();
            _viewModel = new CreateProfileViewModel();
            DataContext = _viewModel;
        }

        private void CreateProfile(object sender, System.Windows.RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(_viewModel.Name))
            {
                MessageBox.Show("Field `Name` is required");
                return;
            }

            Profile newProfile = new()
            {
                Name = _viewModel.Name,
            };

            // Проверяем перед добавлением
            if (ProfileStore.Instance.Profiles.Any(s => s.Name == newProfile.Name))
            {
                MessageBox.Show("Name already exists!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Прерываем выполнение
            }

            ProfileStore.Instance.AddProfile(newProfile);
            MessageBox.Show("Added Profile");
        }
    }
}
