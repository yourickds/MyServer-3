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
        private const string MutexName = "MyServerMutexUniqueFix";
        private static Mutex? _mutex;
        protected override void OnStartup(StartupEventArgs e)
        {
            bool createdNew;
            _mutex = new Mutex(true, MutexName, out createdNew);

            if (!createdNew)
            {
                // Приложение уже запущено
                MessageBox.Show("Приложение уже запущено", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                Environment.Exit(0); // Немедленное завершение без вызова обработчиков
                return;
            }

            RegenerateAllConfigs.Invoke();

            base.OnStartup(e);

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

            _mutex?.ReleaseMutex();
            _mutex?.Dispose();
            base.OnExit(e);
        }
    }

}
