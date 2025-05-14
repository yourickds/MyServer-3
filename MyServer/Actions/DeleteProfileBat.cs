using System.IO;
using System.Windows;
using MyServer.Models;

namespace MyServer.Actions
{
    public static class DeleteProfileBat
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

                    if (File.Exists(batFilePath))
                    {
                        File.Delete(batFilePath);
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
}
