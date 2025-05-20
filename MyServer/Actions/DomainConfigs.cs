using System.IO;
using System.Windows;
using MyServer.Models;

namespace MyServer.Actions
{
    public class DomainConfigs
    {
        public static void Create(Domain domain)
        {
            try
            {
                string serverDir = GetMyServerDir.Invoke(); // Может бросить InvalidOperationException
                string configDir = System.IO.Path.Combine(serverDir, "userdata", "configs", "domains", domain.Name);
                string phpVerFile = System.IO.Path.Combine(configDir, ".phpver");
                string composerVerFile = System.IO.Path.Combine(configDir, ".composerver");

                if (!Directory.Exists(configDir))
                {
                    Directory.CreateDirectory(configDir);
                }

                if (File.Exists(phpVerFile))
                {
                    File.Delete(phpVerFile);
                }

                if (File.Exists(composerVerFile))
                {
                    File.Delete(composerVerFile);
                }

                //Получаем профиль домена
                Profile profile = domain.Profile;
                //Получаем модули
                Module? modulePhp = profile.Modules.FirstOrDefault(m => m.Variable == "%PHP%");
                Module? moduleComposer = profile.Modules.FirstOrDefault(m => m.Variable == "%COMPOSER%");

                if (modulePhp is not null)
                {
                    StreamWriter writer = new(phpVerFile);
                    writer.Write(modulePhp.Name);
                    writer.Close();
                }

                if (moduleComposer is not null)
                {
                    StreamWriter writer = new(composerVerFile);
                    writer.Write(moduleComposer.Name);
                    writer.Close();
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

        public static void Delete(Domain domain)
        {
            try
            {
                string serverDir = GetMyServerDir.Invoke(); // Может бросить InvalidOperationException
                string configDir = System.IO.Path.Combine(serverDir, "userdata", "configs", "domains", domain.Name);
                string phpVerFile = System.IO.Path.Combine(configDir, ".phpver");
                string composerVerFile = System.IO.Path.Combine(configDir, ".composerver");

                if (Directory.Exists(configDir))
                {
                    Directory.Delete(configDir, true);
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
