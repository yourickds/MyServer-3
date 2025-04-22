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
                // Проверяем есть ли у модуля профиля
                if (_viewModel.SelectedModule.Profiles.Count > 0)
                {
                    MessageBox.Show("Нельзя удалить модуль который используется в профилях!", "Удаление модуля", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить модуль ?", "Удаление модуля", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    ModuleStore.Instance.DeleteModule(_viewModel.SelectedModule.Id);
                }
            }
            else
            {
                MessageBox.Show("Selected Module");
            }
        }
    }
}
