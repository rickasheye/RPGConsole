using Newtonsoft.Json;
using System;
using System.IO;

namespace RPGConsole
{
    public class SystemSettings
    {

        public string defaultUSER;

        public bool cmdMode;

        public bool FPSToggle;

        public string DiscordToken;

        public ulong DiscordPrivateServerName;

        public static SystemSettings LoadSystemSettings(SystemSettings settings, string path)
        {
            if (File.Exists(path))
            {
                return JsonConvert.DeserializeObject<SystemSettings>(File.ReadAllText(path));
            }
            else
            {
                Console.WriteLine("System Settings file doesnt exist creating one called system.json");
                File.WriteAllText(path, JsonConvert.SerializeObject(new SystemSettings()));
                return LoadSystemSettings(settings, path);
            }
        }

        public void SaveSettings(string path)
        {
            System.IO.File.WriteAllText(path, JsonConvert.SerializeObject(this));
        }
    }
}
