namespace MyServer.Actions
{
    public class ValidateIpAddress
    {
        public static bool Invoke(string ip)
        {
            if (!System.Net.IPAddress.TryParse(ip, out var address))
                return false;

            // Проверяем, что IP начинается с 127.
            byte[] bytes = address.GetAddressBytes();
            return bytes[0] == 127;
        }
    }
}
