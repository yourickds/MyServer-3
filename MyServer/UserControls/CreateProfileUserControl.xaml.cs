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
                Modules = _viewModel.AppendModules
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
