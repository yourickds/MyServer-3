using System.IO;
using MyServer.Models;

namespace MyServer.Actions
{
    public static class GenerateProfileBat
    {
        public static void Invoke(Profile profile)
        {
            if (profile != null && profile.Modules != null)
            {
                if (Directory.Exists("userdata/profiles"))
                {
                    if (File.Exists("userdata/profiles/" + profile.Name + ".bat"))
                        File.Delete("userdata/profiles/" + profile.Name + ".bat");

                    string content = "set PATH=";
                    foreach (var module in profile.Modules)
                    {
                        content += module.Dir.Replace("%myserverdir%\\", AppDomain.CurrentDomain.BaseDirectory) + ";";
                    }
                    content += "%PATH%";

                    StreamWriter writer = new("userdata/profiles/" + profile.Name + ".bat");
                    writer.Write(content);
                    writer.Close();
                }
            }
        }
    }
}
