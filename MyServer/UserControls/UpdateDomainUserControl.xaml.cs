using System.Windows;
using System.Windows.Controls;
using MyServer.Models;
using MyServer.Stores;
using MyServer.ViewModels;

namespace MyServer.UserControls
{
    /// <summary>
    /// Логика взаимодействия для UpdateDomainUserControl.xaml
    /// </summary>
    public partial class UpdateDomainUserControl : UserControl
    {
        private readonly UpdateDomainViewModel _viewModel;

        private Domain SelectedDomain;

        public UpdateDomainUserControl(Domain selectedDomain)
        {
            InitializeComponent();
            _viewModel = new UpdateDomainViewModel(selectedDomain);
            SelectedDomain = selectedDomain;
            DataContext = _viewModel;
        }

        private void UpdateDomain(object sender, System.Windows.RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(_viewModel.Name))
            {
                MessageBox.Show("Field `Name` is required");
                return;
            }

            if (_viewModel.SelectedProfile is null or not Profile)
            {
                MessageBox.Show("Field `Profile` is required");
                return;
            }

            bool nameExists = DomainStore.Instance.Domains
                .Any(s => s.Name == _viewModel.Name && s.Id != SelectedDomain.Id);

            if (nameExists)
            {
                MessageBox.Show("Name already exists!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Прерываем выполнение
            }

            SelectedDomain.Name = _viewModel.Name;
            SelectedDomain.DocumentRoot = _viewModel.DocumentRoot;
            SelectedDomain.ProfileId = _viewModel.SelectedProfile.Id;
            SelectedDomain.Profile = _viewModel.SelectedProfile;

            DomainStore.Instance.UpdateDomain(SelectedDomain);
            MessageBox.Show("Update Domain");
        }
    }
}
