using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MemoryMaze
{
    public enum profiles
    {
        one = 1,
        two = 2,
        three = 3,
    }
    public static class ProfileConstants
    {
        public static profiles activeProfile;
        // TODO: move this somewhere else or find a better workaround
        public static int levelToPlay;
    }

    public class ManageProfiles
    {
        public String profileOne;
        public String profileTwo;
        public String profileThree;

        public ManageProfiles()
        {

        }

        public ManageProfiles(String profileName, profiles profile)
        {
            if (File.Exists(getProfileName(profile)))
            {
                File.Delete("Assets/" + getProfileName(profile));
            }
            setProfile(profileName, profile);
        }

        public void setProfile(String newName, profiles profile)
        {
            ProfileConstants.activeProfile = profile;
            if (File.Exists("Assets/" + getProfileName(profile)))
            {
                File.Delete("Assets/" + getProfileName(profile));
            }
            switch(profile)
            {
                case profiles.one: profileOne = newName;
                    break;
                case profiles.two: profileTwo = newName;
                    break;
                case profiles.three: profileThree = newName;
                    break;
            }
            saveManageProfiles();
        }
        
        public String getProfileName(profiles profile)
        {
            switch (profile)
            {
                case profiles.one: return profileOne;
                case profiles.two: return profileTwo;
                default: return profileThree;
            }
        }

        public String getActiveProfileName()
        {
            return getProfileName(ProfileConstants.activeProfile);
        }

        /// <summary>
        /// Save ManageProfiles object with XmlSerializer
        /// </summary>
        /// <param name="player_">player object</param>
        private void saveProfiles(ManageProfiles manageProfiles, String fileName)
        {
            XmlSerializer ser = new XmlSerializer(typeof(ManageProfiles));
            FileStream stream = new FileStream(fileName, FileMode.Create);

            ser.Serialize(stream, manageProfiles);
            stream.Close();
        }

        public void saveManageProfiles()
        {
            saveProfiles(this, "Assets/ProfileData");
        }

        /// <summary>
        /// Load ManageProfiles class
        /// </summary>
        /// <returns>loaded player</returns>
        private ManageProfiles loadProfiles(String fileName)
        {
            try
            {
                if(!File.Exists(fileName))
                {
                    throw new System.IO.FileNotFoundException();
                }
                XmlSerializer ser = new XmlSerializer(typeof(ManageProfiles));
                StreamReader reader = new StreamReader(fileName);
                Console.WriteLine();
                ManageProfiles manageProfiles = (ManageProfiles)ser.Deserialize(reader);

                reader.Close();

                return manageProfiles;
            }
            catch(System.IO.FileNotFoundException)
            {
                ManageProfiles result = new ManageProfiles();
                result.setProfile("-", profiles.one);
                result.setProfile("-", profiles.two);
                result.setProfile("-", profiles.three);
                result.saveManageProfiles();
                return result;
            }
        }

        public ManageProfiles loadManageProfiles()
        {
            return loadProfiles("Assets/ProfileData");
        }
    }
}
