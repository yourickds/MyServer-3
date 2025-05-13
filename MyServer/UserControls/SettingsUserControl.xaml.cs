using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using MyServer.Actions;
using MyServer.Properties;

namespace MyServer.UserControls
{
    /// <summary>
    /// Логика взаимодействия для SettingsUserControl.xaml
    /// </summary>
    public partial class SettingsUserControl : UserControl
    {
        public SettingsUserControl()
        {
            InitializeComponent();

            // Срабатывает при каждом появлении / скрытии
            this.IsVisibleChanged += (s, e) =>
            {
                if (this.IsVisible)
                {
                    // Обновляем TextBox только при ПОЯВЛЕНИИ контрола
                    IdPesudoInterfaceTextBox.Text = Settings.Default.IdPseudoInterface.ToString();
                }
            };
        }

        private void GenerateConfigSerives(object sender, RoutedEventArgs e)
        {
            RegenerateAllConfigs.Invoke();
            // Перезапускаем службы
            Actions.RestartWorkServices.Invoke();
        }

        private void OpenHostsFile(object sender, RoutedEventArgs e)
        {
            var notepad = new Process();
            notepad.StartInfo.FileName = "notepad.exe";
            notepad.StartInfo.Arguments = Environment.GetFolderPath(Environment.SpecialFolder.Windows) + "/System32/drivers/etc/hosts";
            notepad.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            notepad.StartInfo.Verb = "runas";
            notepad.StartInfo.UseShellExecute = true;
            notepad.Start();
        }

        private void RestartWorkServices(object sender, RoutedEventArgs e)
        {
            Actions.RestartWorkServices.Invoke();
        }

        private void ChangePseudoInterface(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(IdPesudoInterfaceTextBox.Text, out int interfaceId))
            {
                // Успешное преобразование, используем interfaceId
                Properties.Settings.Default.IdPseudoInterface = interfaceId;
                Properties.Settings.Default.Save();
            }
            else
            {
                // Обработка некорректного ввода
                MessageBox.Show("Разрешено только целочисленное значение", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                IdPesudoInterfaceTextBox.Focus();
            }
        }
    }
}
