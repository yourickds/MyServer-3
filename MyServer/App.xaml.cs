using System.Windows;
using MyServer.Actions;
using MyServer.Models;
using MyServer.Stores;

namespace MyServer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //Оичщаем на всякий файл Hosts
            ClearDomainHosts.Invoke();
            // Заполняем файл hosts
            SetDomainHosts.Invoke();

            //Получаем все службы и проверяем какие уже запущены и цепляем слушателя, а какие надо запустить
            foreach (Service service in ServiceStore.Instance.Services)
            {
                // Проверяем указан ли Pid и запущена ли данная служба
                if (service.Pid != null && GetStatusService.Invoke(service))
                {
                    //Process? process = GetProcessService.Invoke(service.Pid.Value, service.FilePath.Replace("%myserverdir%\\", AppDomain.CurrentDomain.BaseDirectory));
                    //// Если процесс существует то вешаем на него слушателя
                    //if (process != null && !process.HasExited) 
                    //{
                    //    MessageBox.Show("What at Fuck");
                    //    SetObservableService.Invoke(process, service);
                    //}
                    // Не получается повесить на уже существующий процесс слушателя (
                    GetStopService.Invoke(service);
                    GetStartService.Invoke(service);
                }
                // Если не запущена служба, запускаем службу если есть галочка
                else if (service.Startup)
                {
                    GetStartService.Invoke(service);
                }
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            // Останавливаем все запущенные службы
            foreach (Service service in ServiceStore.Instance.Services)
            {
                GetStopService.Invoke(service);
            }
            // Очищаем файл Hosts
            ClearDomainHosts.Invoke();
            base.OnExit(e);
        }
    }

}
