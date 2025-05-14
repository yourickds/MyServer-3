using System.IO;
using System.Windows;
using MyServer.Models;

namespace MyServer.Actions
{
    public static class CreateTemplateConfigDomain
    {
        public static void Invoke(Domain domain)
        {
            try
            {
                string serverDir = GetMyServerDir.Invoke(); // Может бросить InvalidOperationException
                string vhostsDir = System.IO.Path.Combine(serverDir, "userdata", "configs", "Apache24", "vhosts");
                string confTplFilePath = System.IO.Path.Combine(vhostsDir, $"{domain.Name}.conf.tpl");
                string exampleFilePath = System.IO.Path.Combine(vhostsDir, "vhost.ex");
                string domainDir = System.IO.Path.Combine(serverDir, "domains", domain.Name);

                if (!Directory.Exists(vhostsDir))
                {
                    Directory.CreateDirectory(vhostsDir);
                }

                if (File.Exists(confTplFilePath))
                {
                    File.Delete(confTplFilePath);
                }

                StreamReader reader = new(exampleFilePath);
                string content = reader.ReadToEnd();
                reader.Close();
                content = content.Replace("%domainName%", domain.Name);
                content = content.Replace("%documentRoot%", domain.DocumentRoot);
                foreach (Module module in domain.Profile.Modules)
                {
                    if (module != null && module.Variable != null && module.Dir != null)
                    {
                        content = content.Replace(module.Variable, module.Dir.Replace("\\", "/"));
                    }
                }
                StreamWriter writer = new(confTplFilePath);
                writer.Write(content);
                writer.Close();

                if (!Directory.Exists(domainDir))
                {
                    Directory.CreateDirectory(domainDir);
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
