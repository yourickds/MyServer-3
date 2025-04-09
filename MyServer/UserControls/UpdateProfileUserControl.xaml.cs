using MyServer.Models;
using MyServer.Stores;
using MyServer.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace MyServer.UserControls
{
    /// <summary>
    /// Логика взаимодействия для UpdateProfileUserControl.xaml
    /// </summary>
    public partial class UpdateProfileUserControl : UserControl
    {
        private readonly UpdateProfileViewModel _viewModel;

        private Profile SelectedProfile;

        public UpdateProfileUserControl(Profile selectedProfile)
        {
            InitializeComponent();
            _viewModel = new UpdateProfileViewModel(selectedProfile);
            SelectedProfile = selectedProfile;
            DataContext = _viewModel;
        }

        private void UpdateProfile(object sender, System.Windows.RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(_viewModel.Name))
            {
                MessageBox.Show("Field `Name` is required");
                return;
            }

            bool nameExists = ProfileStore.Instance.Profiles
                .Any(s => s.Name == _viewModel.Name && s.Id != SelectedProfile.Id);

            if (nameExists)
            {
                MessageBox.Show("Name already exists!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Прерываем выполнение
            }

            SelectedProfile.Name = _viewModel.Name;

            ProfileStore.Instance.UpdateProfile(SelectedProfile);
            MessageBox.Show("Update Profile");
        }

        private void AppendModule(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_viewModel.SelectedModule is not null and Module)
            {
                if (!_viewModel.AppendModules.Any(m => m.Id == _viewModel.SelectedModule.Id))
                {
                    _viewModel.AppendModules.Add(_viewModel.SelectedModule);
                }
                else
                {
                    MessageBox.Show("Module already exists!");
                }
            }
            else
            {
                MessageBox.Show("Selected Module");
            }
        }

        private void DeleteModule(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedAppendModule is not null and Module)
            {
                _viewModel.AppendModules.Remove(_viewModel.SelectedAppendModule);
            }
            else
            {
                MessageBox.Show("Selected Module for Delete");
            }
        }
    }
}
