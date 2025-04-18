
using System.Diagnostics;
using System.Windows;
using MyServer.Models;
using MyServer.Stores;

namespace MyServer.Actions
{
    public static class GetStartService
    {
        public static async void Invoke(Service service)
        {
            Process process = new();
            process.StartInfo.FileName = service.FilePath.Replace("%myserverdir%", AppDomain.CurrentDomain.BaseDirectory);
            process.StartInfo.UseShellExecute = true;
            if (!String.IsNullOrEmpty(service.Arguments))
            {
                process.StartInfo.Arguments = service.Arguments.Replace("%myserverdir%", AppDomain.CurrentDomain.BaseDirectory);
            }
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.EnableRaisingEvents = true;
            if (process.Start())
            {
                await Task.Delay(100); // Даём время на инициализацию
                process.Refresh();     // Обновляем данные процесса

                if (!process.HasExited)
                {
                    service.Pid = process.Id;
                    ServiceStore.Instance.UpdateService(service);

                    SetObservableService.Invoke(process, service);

                    MessageBox.Show("Service Started. Pid: " + process.Id);
                }
                else
                {
                    MessageBox.Show("Не удалось запустить процесс");
                }
            }
            else
            {
                MessageBox.Show("Не удалось запустить процесс");
            }
        }
    }
}
