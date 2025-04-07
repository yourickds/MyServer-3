using System.Windows.Controls;
using MyServer.ViewModels;

namespace MyServer.UserControls
{
    /// <summary>
    /// Логика взаимодействия для ServiceUserControl.xaml
    /// </summary>
    public partial class ServiceUserControl : UserControl
    {
        private readonly ServiceViewModel _viewModel;
        public ServiceUserControl()
        {
            InitializeComponent();
            _viewModel = new ServiceViewModel();
            DataContext = _viewModel;
        }
    }
}
