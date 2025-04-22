using System.Diagnostics;
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
            RegenerateAllConfigs.Invoke();
            // Перезапускаем службы
            Actions.RestartWorkServices.Invoke();
        }

        private void OpenHostsFile(object sender, RoutedEventArgs e)
        {
            var notepad = new Process();
            notepad.StartInfo.FileName = "notepad.exe";
            notepad.StartInfo.Arguments = Environment.GetFolderPath(Environment.SpecialFolder.Windows) + "/System32/drivers/etc/hosts";
            notepad.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            notepad.StartInfo.Verb = "runas";
            notepad.StartInfo.UseShellExecute = true;
            notepad.Start();
        }

        private void RestartWorkServices(object sender, RoutedEventArgs e)
        {
            Actions.RestartWorkServices.Invoke();
        }
    }
}
