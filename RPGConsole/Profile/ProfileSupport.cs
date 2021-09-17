using System;
using System.Collections.Generic;
using System.IO;

namespace RPGConsole.Profile
{
    public class ProfileSupport
    {
        public List<Profile> profiles = new List<Profile>();
        public int maxIDS;

        public void ExitProfile()
        {
            //save and quit the program
            foreach (Profile p in profiles)
            {
                if (p != null)
                {
                    string dirPath = Path.Combine(Environment.CurrentDirectory, "profiles/");
                    if (!Directory.Exists(dirPath))
                    {
                        Directory.CreateDirectory(dirPath);
                    }
                    Console.WriteLine(dirPath);
                    ProfileSaving.SaveProfile(p, System.IO.Path.Combine(dirPath, p.name + ".json"));
                }
                else
                {
                    Console.WriteLine("apparently there was a profile that came up null?");
                }
            }
            Console.WriteLine("saved " + profiles.Count + " profiles!");
        }

        public void InstallProfiles()
        {
            string dirPath = Path.Combine(Environment.CurrentDirectory, "profiles/");
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            foreach (string m in Directory.GetFiles(dirPath))
            {
                if (File.Exists(m))
                {
                    //read this file!
                    CreateProfile(ProfileSaving.ReadProfileJSON(m));
                }
                else
                {
                    File.Create(m);
                    InstallProfiles();
                }
            }
            Console.WriteLine("Read " + profiles.Count + " profiles!");
        }

        public Profile GetProfile(string name)
        {
            foreach (Profile prof in profiles)
            {
                if (prof.name == name)
                {
                    return prof;
                }
            }
            return null;
        }

        public void CreateProfile(Profile profile)
        {
            if (profileExists(profile))
            {
                //then operate
                Console.WriteLine("This profile does already exist!");
            }
            else
            {
                profiles.Add(profile);
                Console.WriteLine("Created profile!");
            }
        }

        public void RemoveProfile(Profile profile)
        {
            if (!profileExists(profile))
            {
                Console.WriteLine("Profile doesnt exist!");
            }
            else
            {
                profiles.Remove(profile);
                Console.WriteLine("Removed profile!");
            }
        }

        public bool profileExists(Profile profile)
        {
            foreach (Profile profilm in profiles)
            {
                if (profile.name == profilm.name)
                {
                    return true;
                    break;
                }
            }
            return false;
        }
    }
}
