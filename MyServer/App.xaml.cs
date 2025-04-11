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

            //Получаем все службы и проверяем какие надо запустить
            foreach (Service service in ServiceStore.Instance.Services.Where(s => s.Startup))
            {
                if (!GetStatusService.Invoke(service))
                {
                    GetStartService.Invoke(service);
                }
            }
        }
    }

}
