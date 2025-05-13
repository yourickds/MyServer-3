
using System.Diagnostics;

namespace MyServer.Actions
{
    public class AddLoopbackIp
    {
        public static void Invoke(string ipAddress)
        {
            var processInfo = new ProcessStartInfo
            {
                FileName = "netsh",
                Arguments = $"interface ipv4 add address 1 {ipAddress}",
                Verb = "runas",
                UseShellExecute = false,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
            };

            using (Process? process = Process.Start(processInfo))
            {
                process?.WaitForExit();
            }
        }
    }
}
