using System.Windows;
using System.Windows.Controls;
using MyServer.Models;
using MyServer.Stores;
using MyServer.ViewModels;

namespace MyServer.UserControls
{
    /// <summary>
    /// Логика взаимодействия для ModuleUserControl.xaml
    /// </summary>
    public partial class ModuleUserControl : UserControl
    {
        private readonly ModuleViewModel _viewModel;
        public ModuleUserControl()
        {
            InitializeComponent();
            _viewModel = new ModuleViewModel();
            DataContext = _viewModel;
        }

        private void CreateModule(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.SelectedModule = null;
        }

        private void DeleteModule(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_viewModel.SelectedModule != null && _viewModel.SelectedModule is Module)
            {
                ModuleStore.Instance.DeleteModule(_viewModel.SelectedModule.Id);
            }
            else
            {
                MessageBox.Show("Selected Module");
            }
        }
    }
}
