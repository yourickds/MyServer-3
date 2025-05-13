using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using ComposerManager.Actions;

string workingDirectory = Environment.CurrentDirectory;
string folderName = Path.GetFileName(workingDirectory);
string MyServerDir;

try
{
    MyServerDir = GetMyServerDir.Invoke();
}
catch (InvalidOperationException e)
{
    Console.WriteLine(e.Message);
    Environment.Exit(0);
    return;
}

using (var db = new ComposerManager.Db())
{
    var domain = db.Domains.AsNoTracking().FirstOrDefault(d => d.Name == folderName);
    if (domain == null)
    {
        Console.WriteLine("Не удалось определить домен!");
        return;
    }

    // Получить профиль домена
    var profile = db.Profiles.Include(p => p.Modules).AsNoTracking().FirstOrDefault(p => p.Id == domain.ProfileId);
    if (profile == null)
    {
        Console.WriteLine("Профиль не найден!");
        return;
    }

    // Определяем версию PHP
    var php = profile.Modules.FirstOrDefault(m => m.Variable == "%PHP%");
    if (php == null)
    {
        Console.WriteLine("Не удалось определить версию PHP");
        return;
    }

    string DirPHP = php.Dir.Replace("%myserverdir%", MyServerDir);

    // Определяем Composer
    var composer = profile.Modules.FirstOrDefault(m => m.Variable == "%COMPOSER%");
    if (composer == null)
    {
        Console.WriteLine("Ну удалось определить версию COMPOSER");
        return;
    }

    string DirCOMPOSER = composer.Dir.Replace("%myserverdir%", MyServerDir);

    using Process process = new();
    process.StartInfo = new ProcessStartInfo
    {
        FileName = Path.Combine(DirPHP, "php.exe"),
        Arguments = Path.Combine(DirCOMPOSER, "composer.phar") + " " + string.Join(" ", args) + " --ansi",
        WorkingDirectory = workingDirectory,
        RedirectStandardOutput = true,
        RedirectStandardError = true,
        UseShellExecute = false,
        CreateNoWindow = true,
        StandardOutputEncoding = System.Text.Encoding.UTF8,
        StandardErrorEncoding = System.Text.Encoding.UTF8
    };

    try
    {
        // Включаем обработку ANSI-цветов
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        process.Start();

        var outputTask = ReadStreamAsync(process.StandardOutput, Console.Out);
        var errorTask = ReadStreamAsync(process.StandardError, Console.Error);

        await process.WaitForExitAsync();
        await Task.WhenAll(outputTask, errorTask);

        // Метод для асинхронного чтения потока и вывода в реальном времени
        async static Task ReadStreamAsync(StreamReader reader, TextWriter writer)
        {
            char[] buffer = new char[4096];
            int count;

            while ((count = await reader.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                await writer.WriteAsync(buffer, 0, count);
                await writer.FlushAsync();
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ошибка при запуске: {ex.Message}");
    }
}