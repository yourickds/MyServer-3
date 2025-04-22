using System.IO;

namespace MyServer.Actions
{
    public static class GenerateConfig
    {
        public static void Invoke(string file)
        {
            if (File.Exists(file))
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
