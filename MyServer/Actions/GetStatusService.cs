using System.Diagnostics;
using MyServer.Models;
using MyServer.Stores;

namespace MyServer.Actions
{
    public static class GetStatusService
    {
        public static bool Invoke(Service service)
        {
            // Проверяем статус службы через процесс если pid установлен
            //if (!service.Status) return false;

            if (service.Pid != null)
            {
                if (GetProcessService.Invoke(service.Pid.Value, service.FilePath.Replace("%myserverdir%\\", AppDomain.CurrentDomain.BaseDirectory)) is not null and Process)
                {
                    return true;
                }
                else
                {
                    service.Pid = null;
                    ServiceStore.Instance.UpdateService(service);
                }
            }

            return false;
        }
    }
}
