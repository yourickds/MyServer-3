using System.Windows;
using System.Windows.Controls;
using MyServer.Models;
using MyServer.Stores;
using MyServer.ViewModels;

namespace MyServer.UserControls
{
    /// <summary>
    /// Логика взаимодействия для DomainUserControl.xaml
    /// </summary>
    public partial class DomainUserControl : UserControl
    {
        private readonly DomainViewModel _viewModel;
        public DomainUserControl()
        {
            InitializeComponent();
            _viewModel = new DomainViewModel();
            DataContext = _viewModel;
        }

        private void CreateDomain(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.SelectedDomain = null;
        }

        private void DeleteDomain(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_viewModel.SelectedDomain != null && _viewModel.SelectedDomain is Domain domain)
            {
                // Подтверждаем удаление
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить домен ?", "Удаление домена.", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // Проверяем существует ли директория
                    if (System.IO.Directory.Exists("domains/" + domain.Name))
                    {
                        // Спрашиваем удалить ли ее
                        MessageBoxResult resultDir = MessageBox.Show("Вы хотите удалить директория сайта ?", "Удаление сайта.", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (resultDir == MessageBoxResult.Yes)
                        {
                            System.IO.Directory.Delete("domains/" + domain.Name, recursive: true);
                        }
                    }
                    DomainStore.Instance.DeleteDomain(_viewModel.SelectedDomain.Id);
                }
            }
            else
            {
                MessageBox.Show("Selected Domain");
            }
        }
    }
}
