using System.Windows;
using System.Windows.Controls;
using MyServer.Models;
using MyServer.Stores;
using MyServer.ViewModels;

namespace MyServer.UserControls
{
    /// <summary>
    /// Логика взаимодействия для CreateDomainUserControl.xaml
    /// </summary>
    public partial class CreateDomainUserControl : UserControl
    {
        private readonly CreateDomainViewModel _viewModel;
        public CreateDomainUserControl()
        {
            InitializeComponent();
            _viewModel = new CreateDomainViewModel();
            DataContext = _viewModel;
        }

        private void CreateDomain(object sender, System.Windows.RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(_viewModel.Name))
            {
                MessageBox.Show("Field `Name` is required");
                return;
            }

            Domain newDomain = new()
            {
                Name = _viewModel.Name,
                DocumentRoot = _viewModel.DocumentRoot,
            };

            // Проверяем перед добавлением
            if (DomainStore.Instance.Domains.Any(s => s.Name == newDomain.Name))
            {
                MessageBox.Show("Name already exists!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Прерываем выполнение
            }

            DomainStore.Instance.AddDomain(newDomain);
            MessageBox.Show("Added Domain");
        }
    }
}
