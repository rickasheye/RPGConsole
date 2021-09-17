using Newtonsoft.Json;
using System;
using System.IO;

namespace RPGConsole.Profile
{
    public class ProfileSaving
    {
        public static void SaveProfile(Profile profile, string path)
        {
            string jsonConvert = JsonConvert.SerializeObject(profile);
            if (!File.Exists(path)) { File.Create(path); }
            File.WriteAllText(path, jsonConvert);
        }

        public static Profile ReadProfileJSON(string path)
        {
            if (File.ReadAllText(path) != string.Empty)
            {
                return JsonConvert.DeserializeObject<Profile>(File.ReadAllText(path));
            }
            else
            {
                Console.WriteLine("seems like that profile was empty!");
                return null;
            }
        }
    }
}
