using System.IO;
using MyServer.Models;

namespace MyServer.Actions
{
    public static class DeleteConfigsDomain
    {
        public static void Invoke(Domain domain)
        {
            if (File.Exists("userdata/configs/Apache24/vhosts/" + domain.Name + ".conf"))
                File.Delete("userdata/configs/Apache24/vhosts/" + domain.Name + ".conf");
            if (File.Exists("userdata/configs/Apache24/vhosts/" + domain.Name + ".conf.tpl"))
                File.Delete("userdata/configs/Apache24/vhosts/" + domain.Name + ".conf.tpl");
        }
    }
}
