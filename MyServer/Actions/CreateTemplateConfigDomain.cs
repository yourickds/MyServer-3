using System.IO;
using MyServer.Models;

namespace MyServer.Actions
{
    public static class CreateTemplateConfigDomain
    {
        public static void Invoke(Domain domain)
        {
            if (System.IO.Directory.Exists("userdata/configs/Apache24/vhosts"))
            {
                if (File.Exists("userdata/configs/Apache24/vhosts/" + domain.Name + ".conf.tpl"))
                    File.Delete("userdata/configs/Apache24/vhosts/" + domain.Name + ".conf.tpl");

                StreamReader reader = new("userdata/configs/Apache24/vhosts/vhost.ex");
                string content = reader.ReadToEnd();
                reader.Close();
                content = content.Replace("%domainName%", domain.Name);
                content = content.Replace("%documentRoot%", domain.DocumentRoot);
                foreach (Module module in domain.Profile.Modules)
                {
                    if (module != null && module.Variable != null && module.Dir != null)
                    {
                        content = content.Replace(module.Variable, module.Dir.Replace("\\", "/"));
                    }
                }
                StreamWriter writer = new("userdata/configs/Apache24/vhosts/" + domain.Name + ".conf.tpl");
                writer.Write(content);
                writer.Close();
            }
            if (!System.IO.Directory.Exists("domains/" + domain.Name))
                System.IO.Directory.CreateDirectory("domains/" + domain.Name);
        }
    }
}
