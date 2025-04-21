using System.Windows;
using System.Windows.Controls;
using MyServer.Models;
using MyServer.Stores;

namespace MyServer.Actions
{
    public static class SetTrayMenu
    {
        public static ContextMenu Invoke()
        {
            ContextMenu menu = new();
            menu.Items.Clear();

            MenuItem setting = new() { Header = "Settings" };
            setting.Click += ShowMainWindow;

            menu.Items.Add(setting);

            MenuItem exit = new() { Header = "Exit" };
            exit.Click += AppExit;

            menu.Items.Add(exit);

            return menu;
        }

        private static void ShowMainWindow(object sender, RoutedEventArgs e)
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

        private static void AppExit(object sender, RoutedEventArgs e)
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
