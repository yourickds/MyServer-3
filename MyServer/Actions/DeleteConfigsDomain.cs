using System.IO;
using System.Windows;
using MyServer.Models;

namespace MyServer.Actions
{
    public static class DeleteConfigsDomain
    {
        public static void Invoke(Domain domain)
        {
            try
            {
                string serverDir = GetMyServerDir.Invoke(); // Может бросить InvalidOperationException
                string vhostsDir = System.IO.Path.Combine(serverDir, "userdata", "configs", "Apache24", "vhosts");
                string confFilePath = System.IO.Path.Combine(vhostsDir, $"{domain.Name}.conf");
                string confTplFilePath = System.IO.Path.Combine(vhostsDir, $"{domain.Name}.conf.tpl");

                if (File.Exists(confFilePath))
                {
                    File.Delete(confFilePath);
                }

                if (File.Exists(confTplFilePath))
                {
                    File.Delete(confTplFilePath);
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0);
            }
            catch (IOException ex) // Ловим ошибки файловых операций
            {
                MessageBox.Show($"Не удалось удалить файл: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0);
            }
            catch (Exception ex) // На всякий случай ловим остальное
            {
                MessageBox.Show($"Неизвестная ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0);
            }
        }
    }
}
