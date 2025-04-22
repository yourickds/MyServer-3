using System.IO;
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

            // Проверка на допустимые символы
            if (!System.Text.RegularExpressions.Regex.IsMatch(_viewModel.Name, @"^[a-z0-9\-]+$"))
            {
                MessageBox.Show("Name can only contain lowercase letters, numbers and hyphens");
                return;
            }

            if (!String.IsNullOrEmpty(_viewModel.DocumentRoot))
            {
                // Проверка на допустимые символы (только a-z и /)
                if (!System.Text.RegularExpressions.Regex.IsMatch(_viewModel.DocumentRoot, @"^[a-z][a-z\/]*$"))
                {
                    MessageBox.Show("DocumentRoot can only contain lowercase letters and '/', and cannot start with '/'");
                    return;
                }

                if (_viewModel.DocumentRoot.EndsWith("/"))
                {
                    MessageBox.Show("DocumentRoot cannot end with '/'");
                    return;
                }

                if (_viewModel.DocumentRoot.Contains("//"))
                {
                    MessageBox.Show("DocumentRoot cannot contain double slashes '//'");
                    return;
                }
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

            // Проверяем существует ли директория
            if (SelectedDomain.Name != _viewModel.Name && System.IO.Directory.Exists("domains/" + SelectedDomain.Name))
            {
                // Переименовываем директорию
                try
                {
                    // Проверяем, не существует ли уже директория с новым именем
                    if (!Directory.Exists("domains/" + _viewModel.Name))
                    {
                        Directory.Move("domains/" + SelectedDomain.Name, "domains/" + _viewModel.Name);
                    }
                    else
                    {
                        MessageBox.Show("Директория с таким именем уже существует");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при переименовании директории: {ex.Message}");
                    return;
                }
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
