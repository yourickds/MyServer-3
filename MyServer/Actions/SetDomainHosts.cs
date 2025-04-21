using System.IO;
using System.Windows;
using MyServer.Models;
using MyServer.Stores;

namespace MyServer.Actions
{
    public static class SetDomainHosts
    {
        public static void Invoke()
        {
            try
            {
                using StreamWriter sw = File.AppendText(Environment.GetFolderPath(Environment.SpecialFolder.Windows) + "/System32/drivers/etc/hosts");
                sw.WriteLine("\n# MyServer");
                foreach (Domain domain in DomainStore.Instance.Domains)
                {
                    sw.WriteLine("127.0.0.1\t" + domain.Name + ".local");
                }
                sw.WriteLine("# End MyServer");
            }
            catch
            {
                MessageBox.Show("Невозможно внести изменения в файл HOSTS, возможна блокировка со стороны антивируса.", "MyServer");
            }
        }
    }
}
