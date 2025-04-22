using MyServer.Models;
using MyServer.Stores;

namespace MyServer.Actions
{
    public static class RestartWorkServices
    {
        public async static void Invoke()
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
