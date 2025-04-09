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
            if (_viewModel.SelectedDomain != null && _viewModel.SelectedDomain is Domain)
            {
                DomainStore.Instance.DeleteDomain(_viewModel.SelectedDomain.Id);
            }
            else
            {
                MessageBox.Show("Selected Domain");
            }
        }
    }
}
