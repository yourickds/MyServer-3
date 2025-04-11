using System.Windows.Controls;
using MyServer.ViewModels;
using MyServer.Models;
using System.Windows;
using MyServer.Stores;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;

namespace MyServer.UserControls
{
    /// <summary>
    /// Логика взаимодействия для UpdateServiceUserControl.xaml
    /// </summary>
    public partial class UpdateServiceUserControl : UserControl
    {
        private readonly UpdateServiceViewModel _viewModel;

        private Service SelectedService;

        public UpdateServiceUserControl(Service SelectedService)
        {
            InitializeComponent();
            _viewModel = new UpdateServiceViewModel(SelectedService);
            this.SelectedService = SelectedService;
            DataContext = _viewModel;
        }

        private void UpdateService(object sender, System.Windows.RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(_viewModel.Name))
            {
                MessageBox.Show("Field `Name` is required");
                return;
            }

            if (String.IsNullOrEmpty(_viewModel.FilePath))
            {
                MessageBox.Show("Field `FilePath` is required");
                return;
            }

            if (!File.Exists(_viewModel.FilePath))
            {
                MessageBox.Show("File Not Found in `FilePath`");
                return;
            }

            bool nameExists = ServiceStore.Instance.Services
                .Any(s => s.Name == _viewModel.Name && s.Id != SelectedService.Id);

            if (nameExists)
            {
                MessageBox.Show("Name already exists!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Прерываем выполнение
            }

            SelectedService.Name = _viewModel.Name;
            SelectedService.FilePath = _viewModel.FilePath;
            SelectedService.Arguments = _viewModel.Arguments;
            SelectedService.Startup = _viewModel.Startup;

            ServiceStore.Instance.UpdateService(SelectedService);
            MessageBox.Show("Update Service");
        }

        private void OpenDialogFilePath(object sender, RoutedEventArgs e)
        {
            // Создаем экземпляр OpenFileDialog
            OpenFileDialog openFileDialog = new();

            // Показываем диалог и проверяем, был ли выбран файл
            if (openFileDialog.ShowDialog() == true)
            {
                // Если файл был выбран, обновляем текст в TextBox
                _viewModel.FilePath = openFileDialog.FileName;
            }
        }

        private void ToggleService(object sender, RoutedEventArgs e)
        {
            if (SelectedService is not null and Service)
            {
                if (SelectedService.Status && SelectedService.Pid is not null)
                {
                    if (StopService(SelectedService.Pid.Value))
                    {
                        SelectedService.Pid = null;
                        SelectedService.Status = false;
                        ServiceStore.Instance.UpdateService(SelectedService);
                    }
                }
                else
                {
                    int? pid = StartService();
                    if (pid is not null)
                    {
                        SelectedService.Pid = pid;
                        SelectedService.Status = true;
                        ServiceStore.Instance.UpdateService(SelectedService);
                    }
                }
            }
            else
            {
                MessageBox.Show("Selected Service");
            }
        }

        private int? StartService()
        {
            Process process = new();
            process.StartInfo.FileName = SelectedService.FilePath.Replace("%myserverdir%", AppDomain.CurrentDomain.BaseDirectory);
            process.StartInfo.UseShellExecute = true;
            if (!String.IsNullOrEmpty(SelectedService.Arguments))
            {
                process.StartInfo.Arguments = SelectedService.Arguments.Replace("%myserverdir%", AppDomain.CurrentDomain.BaseDirectory);
            }
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            if (process.Start())
            {
                MessageBox.Show("Service Started. Pid: " + process.Id);
                return process.Id;
            }
            MessageBox.Show("Не удалось запустить процесс");
            return null;
        }

        private bool StopService(int pid)
        {
            try
            {
                Process process = Process.GetProcessById(pid);

                // Проверяем, не завершился ли процесс
                if (process.HasExited)
                {
                    MessageBox.Show($"Процесс уже завершён.");
                    return true;
                }

                try
                {
                    if (process.MainModule != null && process.MainModule.FileName == SelectedService.FilePath)
                    {
                        process.Kill(true);
                    }
                    MessageBox.Show("Service is Stopped");
                    return true;
                }
                catch (System.ComponentModel.Win32Exception)
                {
                    MessageBox.Show($"Не удалось получить директорию процесса");
                }
            }
            catch (ArgumentException)
            {
                MessageBox.Show($"Процесс не существует.");
                return true;
            }
            catch (System.ComponentModel.Win32Exception)
            {
                MessageBox.Show($"Нет доступа к процессу. Требуются права администратора?");
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show($"Процесс завершился или недоступен.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Неизвестная ошибка: {ex.Message}");
            }

            return false;
        }
    }
}
