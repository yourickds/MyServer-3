using System.Diagnostics;
using MyServer.Models;
using MyServer.Stores;

namespace MyServer.Actions
{
    public static class GetStopService
    {
        public static void Invoke(Service service)
        {
            if (service.Pid != null)
            {
                Process? process = GetProcessService.Invoke(service.Pid.Value, service.FilePath.Replace("%myserverdir%\\", AppDomain.CurrentDomain.BaseDirectory));
                process?.Kill(true);
                service.Pid = null;
                ServiceStore.Instance.UpdateService(service);
            }
        }
    }
}
