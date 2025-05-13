using System.Windows;
using System.Windows.Controls;
using MyServer.Models;
using MyServer.Stores;
using MyServer.ViewModels;

namespace MyServer.UserControls
{
    /// <summary>
    /// Логика взаимодействия для PathUserControl.xaml
    /// </summary>
    public partial class PathUserControl : UserControl
    {
        private readonly PathViewModel _viewModel;
        public PathUserControl()
        {
            InitializeComponent();
            _viewModel = new PathViewModel();
            DataContext = _viewModel;
        }

        private void CreatePath(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.SelectedPath = null;
        }

        private void DeletePath(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_viewModel.SelectedPath != null && _viewModel.SelectedPath is Path)
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить path ?", "Удаление path", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    PathStore.Instance.DeletePath(_viewModel.SelectedPath.Id);
                }
            }
            else
            {
                MessageBox.Show("Selected Path");
            }
        }
    }
}
