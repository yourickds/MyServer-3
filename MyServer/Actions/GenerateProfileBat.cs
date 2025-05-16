using System.IO;
using System.Windows;
using MyServer.Models;
using MyServer.Stores;

namespace MyServer.Actions
{
    public static class GenerateProfileBat
    {
        public static void Invoke(Profile profile)
        {
            if (profile != null && profile.Modules != null)
            {
                try
                {
                    string serverDir = GetMyServerDir.Invoke(); // Может бросить InvalidOperationException
                    string profilesDir = System.IO.Path.Combine(serverDir, "userdata", "profiles");
                    string batFilePath = System.IO.Path.Combine(profilesDir, $"{profile.Name}.bat");

                    if (!Directory.Exists(profilesDir))
                    {
                        Directory.CreateDirectory(profilesDir);
                    }

                    if (File.Exists(batFilePath))
                    {
                        File.Delete(batFilePath);
                    }

                    string content = "set PATH=" + profilesDir + ";";
                    foreach (var module in profile.Modules)
                    {
                        content += module.Dir.Replace("%myserverdir%\\", AppDomain.CurrentDomain.BaseDirectory) + ";";
                    }
                    foreach (var path in PathStore.Instance.Paths)
                    {
                        content += path.Dir.Replace("%myserverdir%\\", AppDomain.CurrentDomain.BaseDirectory) + ";";
                    }
                    content += Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Machine);
                    StreamWriter writer = new(batFilePath);
                    writer.Write(content);
                    writer.Close();
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
}
