namespace PHPManager.Actions
{
    public class GetMyServerDir
    {
        public static string Invoke()
        {
            string? path = Environment.GetEnvironmentVariable("MyServerDir", EnvironmentVariableTarget.Machine);
            if (String.IsNullOrEmpty(path))
            {
                throw new InvalidOperationException("Не удалось найти путь до MyServer!");
            }

            return path;
        }
    }
}
