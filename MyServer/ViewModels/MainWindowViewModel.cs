using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using MyServer.Actions;
using MyServer.Models;
using MyServer.Stores;

namespace MyServer.ViewModels
{
    public class MainWindowViewModel: INotifyPropertyChanged
    {
        private ContextMenu _ctxMenu = new();

        public MainWindowViewModel()
        {
            _ctxMenu = CreateContextMenu();
            DomainStore.Instance.PropertyChanged += OnDomainStoreChanged;
        }

        public ContextMenu CtxMenu
        {
            get => _ctxMenu;
            private set
            {
                _ctxMenu = value;
                OnPropertyChanged();
            }
        }

        private ContextMenu CreateContextMenu()
        {
            ContextMenu menu = new();

            menu.Items.Add(CreateDomains());

            menu.Items.Add(new Separator());

            MenuItem setting = new() { Header = "Settings" };
            setting.Click += ShowMainWindow;
            menu.Items.Add(setting);

            MenuItem exit = new() { Header = "Exit" };
            exit.Click += AppExit;
            menu.Items.Add(exit);

            return menu;
        }

        private MenuItem CreateDomains()
        {
            MenuItem domains = new() { Header = "Domains" };

            foreach (Domain domain in DomainStore.Instance.Domains)
            {
                MenuItem item = new() { Header = domain.Name, Tag = domain };
                item.Click += OpenDomain;
                domains.Items.Add(item);
            }

            return domains;
        }

        private void OpenDomain(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem && menuItem.Tag is Domain domain)
            {
                if (!String.IsNullOrEmpty(domain.Name))
                {
                    var process = new Process();
                    process.StartInfo.FileName = "http://" + domain.Name + ".local";
                    process.StartInfo.UseShellExecute = true;
                    process.Start();
                }
            }

        }

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

        private void OnDomainStoreChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DomainStore.Domains))
            {
                CtxMenu = CreateContextMenu();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
