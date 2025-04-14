using System.Windows;
using System.Windows.Controls;
using MyServer.Actions;

namespace MyServer.UserControls
{
    /// <summary>
    /// Логика взаимодействия для SettingsUserControl.xaml
    /// </summary>
    public partial class SettingsUserControl : UserControl
    {
        public SettingsUserControl()
        {
            InitializeComponent();
        }

        private void GenerateConfigSerives(object sender, RoutedEventArgs e)
        {
            GenerateConfigServices.Invoke();
        }
    }
}
