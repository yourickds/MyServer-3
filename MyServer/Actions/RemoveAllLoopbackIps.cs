using MyServer.Models;
using MyServer.Stores;

namespace MyServer.Actions
{
    public class RemoveAllLoopbackIps
    {
        public static void Invoke()
        {
            foreach (Host host in HostStore.Instance.Hosts)
            {
                // Удаляем из netsh
                RemoveLoopbackIp.Invoke(host.Ip);
            }
        }
    }
}
