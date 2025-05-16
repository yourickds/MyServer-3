using System.Diagnostics;
using System.IO;
using System.Windows;

namespace MyServer.Actions
{
    public class Init
    {
        public static void Invoke()
        {
            string MyServerDir;
            try
            {
                MyServerDir = GetMyServerDir.Invoke();
            }
            catch (InvalidOperationException exception)
            {
                MessageBox.Show(exception.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0);
                return;
            }

            if (!File.Exists(System.IO.Path.Combine(MyServerDir, "Init.bat")))
            {
                MessageBox.Show("Не найден файл для инициализация 'Init.bat'");
                Environment.Exit(0);
                return;
            }
            using Process process = new();
            process.StartInfo = new ProcessStartInfo
            {
                FileName = "Init.bat",
                WorkingDirectory = MyServerDir,
                UseShellExecute = true,
                WindowStyle = ProcessWindowStyle.Normal,
                CreateNoWindow = false
            };

            try
            {
                process.Start();
                process.WaitForExit();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при запуске: {ex.Message}");
                Environment.Exit(0);
                return;
            }
        }
    }
}
