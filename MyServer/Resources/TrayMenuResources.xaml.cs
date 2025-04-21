using System.Windows;
using MyServer.Actions;
using MyServer.Models;
using MyServer.Stores;

namespace MyServer.Resources
{
    public partial class TrayMenuResources: ResourceDictionary
    {
        private void ShowMainWindow(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow == null) return;

            // Показываем окно
            Application.Current.MainWindow.Show();

            // Восстанавливаем состояние окна
            Application.Current.MainWindow.WindowState = WindowState.Normal;

            // Активируем окно (переводим фокус)
            Application.Current.MainWindow.Activate();

            // Если окно было свернуто - разворачиваем
            if (Application.Current.MainWindow.WindowState == WindowState.Minimized)
            {
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            }

            // Переводим окно на передний план
            Application.Current.MainWindow.Topmost = true;
            Application.Current.MainWindow.Topmost = false;
        }

        private void AppExit(object sender, RoutedEventArgs e)
        {
            // 1. Закрываем все окна приложения
            foreach (Window window in Application.Current.Windows)
            {
                window.Close();
            }

            // 2. Останавливаем все запущенные службы
            foreach (Service service in ServiceStore.Instance.Services)
            {
                GetStopService.Invoke(service);
            }

            // 3. Очищаем файл Hosts
            ClearDomainHosts.Invoke();

            // 4. Завершаем работу приложения
            Application.Current.Shutdown();
        }
    }
}
