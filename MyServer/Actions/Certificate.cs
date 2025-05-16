using System.Diagnostics;
using System.IO;
using System.Windows;

namespace MyServer.Actions
{
    public class Certificate
    {
        public static void Create(string name)
        {
            try
            {
                string serverDir = GetMyServerDir.Invoke(); // Может бросить InvalidOperationException
                string certsDir = System.IO.Path.Combine(serverDir, "userdata", "certs");
                string mkcertDir = System.IO.Path.Combine(serverDir, "programs", "SSL");
                string certPemFile = System.IO.Path.Combine(certsDir, $"{name}.local.pem");
                string certKeyFile = System.IO.Path.Combine(certsDir, $"{name}.local-key.pem");

                if (!Directory.Exists(certsDir))
                {
                    Directory.CreateDirectory(certsDir);
                }

                if (File.Exists(certPemFile))
                {
                    File.Delete(certPemFile);
                }

                if (File.Exists(certKeyFile))
                {
                    File.Delete(certKeyFile);
                }

                using Process process = new();
                process.StartInfo = new ProcessStartInfo
                {
                    FileName = "mkcert.exe",
                    WorkingDirectory = mkcertDir,
                    Arguments = $"-cert-file={certPemFile} -key-file={certKeyFile} {name}.local",
                    UseShellExecute = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true
                };

                try
                {
                    process.Start();
                    process.WaitForExit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при запуске: {ex.Message}");
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

        public static void Delete(string name)
        {
            try
            {
                string serverDir = GetMyServerDir.Invoke(); // Может бросить InvalidOperationException
                string certsDir = System.IO.Path.Combine(serverDir, "userdata", "certs");
                string certPemFile = System.IO.Path.Combine(certsDir, $"{name}.local.pem");
                string certKeyFile = System.IO.Path.Combine(certsDir, $"{name}.local-key.pem");

                if (File.Exists(certPemFile))
                {
                    File.Delete(certPemFile);
                }

                if (File.Exists(certKeyFile))
                {
                    File.Delete(certKeyFile);
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
