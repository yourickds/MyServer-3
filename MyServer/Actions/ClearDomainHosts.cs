using System.IO;
using System.Text.RegularExpressions;
using System.Windows;

namespace MyServer.Actions
{
    public static class ClearDomainHosts
    {
        public static void Invoke()
        {
            try
            {
                string content = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.Windows) + "/System32/drivers/etc/hosts");
                string pattern = @"(\r?\n)?# MyServer[\s\S]*?# End MyServer(\r?\n)?";
                content = Regex.Replace(content, pattern, string.Empty);
                File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.Windows) + "/System32/drivers/etc/hosts", content);
            }
            catch
            {
                MessageBox.Show("Невозможно внести изменения в файл HOSTS, возможна блокировка со стороны антивируса.", "MyServer");
            }
        }
    }
}
