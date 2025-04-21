
using System.Diagnostics;
using System.Windows;
using MyServer.Models;
using MyServer.Stores;

namespace MyServer.Actions
{
    public static class SetObservableService
    {
        public static void Invoke(Process process, Service service)
        {
            if (!process.HasExited)
            {
                process.Exited += (sender, args) =>
                {
                    if (process.HasExited
                        && Application.Current != null
                        && !Application.Current.Dispatcher.HasShutdownStarted
                    )
                    {
                        // Обновляем статус в UI
                        Application.Current.Dispatcher.InvokeAsync(() =>
                        {
                            service.Pid = null;
                            ServiceStore.Instance.UpdateService(service);
                            //MessageBox.Show($"Служба {service.Name} была остановлена!");
                        });
                    }
                };
            }
        }
    }
}
