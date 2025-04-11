
using System.Diagnostics;
using System.Windows;
using MyServer.Models;
using MyServer.Stores;

namespace MyServer.Actions
{
    public static class GetStartService
    {
        public static void Invoke(Service service)
        {
            Process process = new();
            process.StartInfo.FileName = service.FilePath.Replace("%myserverdir%", AppDomain.CurrentDomain.BaseDirectory);
            process.StartInfo.UseShellExecute = true;
            if (!String.IsNullOrEmpty(service.Arguments))
            {
                process.StartInfo.Arguments = service.Arguments.Replace("%myserverdir%", AppDomain.CurrentDomain.BaseDirectory);
            }
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            if (process.Start())
            {
                service.Status = true;
                service.Pid = process.Id;
                ServiceStore.Instance.UpdateService(service);
                MessageBox.Show("Service Started. Pid: " + process.Id);
            }
            else
            {
                MessageBox.Show("Не удалось запустить процесс");
            }
        }
    }
}
