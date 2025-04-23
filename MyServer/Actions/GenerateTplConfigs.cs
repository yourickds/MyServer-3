using System.IO;

namespace MyServer.Actions
{
    public static class GenerateTplConfigs
    {
        public static void Invoke()
        {
            if (Directory.Exists("userdata/configs"))
            {
                // Подготовка конфигов
                string[] findFiles = Directory.GetFiles("userdata/configs", "*.tpl", SearchOption.AllDirectories);
                foreach (string file in findFiles)
                {
                    GenerateConfig.Invoke(file);
                }
            }
        }
    }
}
