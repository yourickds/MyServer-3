
using System.Diagnostics;

namespace MyServer.Actions
{
    public class RemoveLoopbackIp
    {
        public static void Invoke(string ipAddress)
        {
            var processInfo = new ProcessStartInfo
            {
                FileName = "netsh",
                Arguments = $"interface ipv4 delete address 1 {ipAddress}",
                Verb = "runas",
                UseShellExecute = false,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
            };

            using (var process = Process.Start(processInfo))
            {
                process?.WaitForExit();
            }
        }
    }
}
