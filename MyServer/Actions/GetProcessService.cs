using System.Diagnostics;

namespace MyServer.Actions
{
    public static class GetProcessService
    {
        public static Process? Invoke(int pid, string filePath)
        {
            try
            {
                Process process = Process.GetProcessById(pid);

                // Проверяем, не завершился ли процесс
                if (!process.HasExited)
                {
                    try
                    {
                        if (process.MainModule != null && process.MainModule.FileName == filePath)
                        {
                            return process;
                        }
                    }
                    catch (System.ComponentModel.Win32Exception)
                    {
                        //MessageBox.Show($"Не удалось получить директорию процесса");
                    }
                }
            }
            catch (ArgumentException)
            {
                //MessageBox.Show($"Процесс не существует.");
            }
            catch (System.ComponentModel.Win32Exception)
            {
                //MessageBox.Show($"Нет доступа к процессу. Требуются права администратора?");
            }
            catch (InvalidOperationException)
            {
                //MessageBox.Show($"Процесс завершился или недоступен.");
            }
            catch (Exception)
            {
                //MessageBox.Show($"Неизвестная ошибка: {ex.Message}");
            }

            return null;
        }
    }
}
