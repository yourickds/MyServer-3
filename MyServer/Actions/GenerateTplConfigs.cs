using System.IO;
using System.Windows;

namespace MyServer.Actions
{
    public static class GenerateTplConfigs
    {
        public static void Invoke()
        {
            try
            {
                string serverDir = GetMyServerDir.Invoke(); // Может бросить InvalidOperationException
                string configDir = System.IO.Path.Combine(serverDir, "userdata", "configs");

                if (Directory.Exists(configDir))
                {
                    // Подготовка конфигов
                    string[] findFiles = Directory.GetFiles(configDir, "*.tpl", SearchOption.AllDirectories);
                    foreach (string file in findFiles)
                    {
                        GenerateConfig.Invoke(file);
                    }
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
