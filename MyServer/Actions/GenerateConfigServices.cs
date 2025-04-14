using System.IO;
using System.Windows;

namespace MyServer.Actions
{
    public static class GenerateConfigServices
    {
        public static void Invoke()
        {
            if (Directory.Exists("userdata/configs"))
            {
                // Подготовка конфигов
                string[] findFiles = Directory.GetFiles("userdata/configs", "*.tpl", SearchOption.AllDirectories);
                foreach (string file in findFiles)
                {
                    // Удаляем старый конфиг, если есть
                    if (File.Exists(file.Replace(".tpl", "")))
                        File.Delete(file.Replace(".tpl", ""));
                    // Генерируем новый конфиг
                    StreamReader reader = new(file);
                    string content = reader.ReadToEnd();
                    reader.Close();
                    string pathLinux = AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "/");
                    content = content.Replace("%myserverdir%", pathLinux);
                    StreamWriter writer = new(file.Replace(".tpl", ""));
                    writer.Write(content);
                    writer.Close();
                }
            }
        }
    }
}
