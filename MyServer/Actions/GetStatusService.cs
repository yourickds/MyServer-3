using System.Diagnostics;
using MyServer.Models;

namespace MyServer.Actions
{
    public static class GetStatusService
    {
        public static bool Invoke(Service service)
        {
            // Проверяем статус службы через процесс если pid установлен
            //if (!service.Status) return false;

            if (service.Pid != null && 
                GetProcessService.Invoke(service.Pid.Value, service.FilePath) is not null and Process)
            {
                    return true;
            }

            return false;
        }
    }
}
