using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using System.Xml.Serialization;
using System.IO;

namespace MemoryMaze
{
    public class ManageStars
    {
        // TODO check validity of ManageStars?!
        public String PlayerName;

        public Rating[] levelRating;

        public int lastSelectedLevel;

        public int levelSkips;

        public enum Rating
        { 
            Skipped = -1,
            Fail = 0,
            Bronze = 1,
            Silver = 2,
            Gold = 3,
        }

        public ManageStars() { }

        public ManageStars(String _playerName, int levelCount)
        {
            PlayerName = _playerName;
            levelRating = new Rating[levelCount];
            for(int i = 0; i < levelRating.Length; i++)
            {
                levelRating[i] = Rating.Fail;
            }
            lastSelectedLevel = 0;
            levelSkips = 3;
        }

        public void UpdateScoreOfLevel(int _index, Rating _rating)
        {
            if (levelRating[_index] < _rating)
            {
                levelRating[_index] = _rating;
            }
        }

        public Rating GetScoreOfLevel(int index)
        {
            if(index > levelRating.Length || index < 0)
            {
                Logger.Instance.Write("Invalid LevelIndex", 0);
                return Rating.Fail;
            }
            return levelRating[index];
        }

        public int getIndexOfFirstUnsolvedLevel()
        {
            Console.WriteLine("Level.Count: " + levelRating.Length);
            for (int i = 0; i < levelRating.Length; i++)
            {
                if(levelRating[i] == Rating.Fail)
                {
                    return i;
                }
            }
            return 0;
        }

        public int GetLastSelectedLevel()
        {
            return lastSelectedLevel;
        }

        public bool levelIsUnlocked(int level)
        {
            if (level < 0 || level >= levelRating.Length)
                return false;
            if(level == 0)
                return true;
            return levelRating[level - 1] != Rating.Fail;
        }

        public bool EverythingUnlocked()
        {
            return levelRating[levelRating.Length - 1] != Rating.Fail;
        }
        
        public void SetLastSelectedLevel(int _lastSelectedLevel)
        {
            lastSelectedLevel = _lastSelectedLevel;
        }
        /// <summary>
        /// Save ManageStars object with XmlSerializer
        /// </summary>
        /// <param name="player_">player object</param>
        private void saveRatings(ManageStars manageStars, String fileName)
        {
            XmlSerializer ser = new XmlSerializer(typeof(ManageStars));
            FileStream stream = new FileStream(fileName, FileMode.Create);

            ser.Serialize(stream, manageStars);
            stream.Close();
        }

        public void saveManageStars(String playerName)
        {
            saveRatings(this, "Assets/" + playerName);
        }

        public bool SkipLevel(int levelIndex)
        {
            Console.WriteLine(levelSkips);
            if (levelSkips <= 0 || levelRating[levelIndex] != Rating.Fail)
                return false;
            levelSkips--;
            levelRating[levelIndex] = Rating.Skipped;
            return true;
        }

        /// <summary>
        /// Load ManageStars class
        /// </summary>
        /// <returns>loaded player</returns>
        private ManageStars loadRatings(String playerName, int numberOfRatings)
        {
            try
            {
                Console.WriteLine("PlayerName " + playerName);
                XmlSerializer ser = new XmlSerializer(typeof(ManageStars));
                StreamReader reader = new StreamReader("Assets/" + playerName);

                ManageStars manageStars = (ManageStars)ser.Deserialize(reader);
                
                // 
                if(manageStars.levelRating.Length != numberOfRatings)
                {
                    throw new Exception("InvalidNumberOfLevels");
                }

                reader.Close();

                return manageStars;
            }
            catch (Exception e)
            {
                if(e is System.IO.FileNotFoundException || e.Message == "InvalidNumberOfLevels")
                {
                    ManageStars manageStars = new ManageStars("Assets/" + playerName, numberOfRatings);
                    return manageStars;
                }
                else
                {
                    throw e;
                }
            }
        }

        public ManageStars loadManageStars(String playerName, int numberOfRatings)
        {
            Console.WriteLine(playerName);
            return loadRatings(playerName, numberOfRatings);
        }

        // TODO: try to find a better way to do this
        public ManageStars unsafelyLoadManageStars(String playerName)
        {
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(ManageStars));
                StreamReader reader = new StreamReader("Assets/" + playerName);

                ManageStars manageStars = (ManageStars)ser.Deserialize(reader);

                reader.Close();

                return manageStars;
            }
            catch (Exception e)
            {
                if (e is System.IO.FileNotFoundException)
                {
                    ManageStars manageStars = new ManageStars("Assets/" + playerName, 40);
                    return manageStars;
                }
                else
                {
                    throw e;
                }
            }
        }
    }
}
