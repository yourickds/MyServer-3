using System.IO;
using MyServer.Models;

namespace MyServer.Actions
{
    public static class DeleteProfileBat
    {
        public static void Invoke(Profile profile)
        {
            if (profile != null && profile.Modules != null)
            {
                if (Directory.Exists("userdata/profiles"))
                {
                    if (File.Exists("userdata/profiles/" + profile.Name + ".bat"))
                        File.Delete("userdata/profiles/" + profile.Name + ".bat");
                }
            }
        }
    }
}
