using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using MyServer.Actions;
using MyServer.Models;
using MyServer.Stores;

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

        private async void RestartWorkServices(object sender, RoutedEventArgs e)
        {
            // Получаем все службы
            foreach (Service service in ServiceStore.Instance.Services)
            {
                if (service.Pid != null && GetStatusService.Invoke(service))
                {
                    GetStopService.Invoke(service);
                    await Task.Delay(300); // Даём время на завершение процесса
                    GetStartService.Invoke(service);
                }
            }
        }
    }
}
