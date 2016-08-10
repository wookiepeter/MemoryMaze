﻿using System;
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
    class ManageStars
    {
        // TODO check validity of ManageStars?!
        public String PlayerName;

        public Rating[] levelRating;

        public enum Rating
        { 
            Fail = 0,
            Bronze = 1,
            Silver = 2,
            Gold = 3,
        }

        public ManageStars(String _playerName, List<Level> _levelList)
        {
            PlayerName = _playerName;
            levelRating = new Rating[_levelList.Count];
            for(int i = 0; i < levelRating.Length; i++)
            {
                levelRating[i] = Rating.Fail;
            }
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

        /// <summary>
        /// Save ManageStars object with XmlSerializer
        /// </summary>
        /// <param name="player_">player object</param>
        private void savePlayerData(ManageStars manageStars, String fileName)
        {
            XmlSerializer ser = new XmlSerializer(typeof(ManageStars));
            FileStream stream = new FileStream(fileName, FileMode.Create);

            ser.Serialize(stream, manageStars);
            stream.Close();


        }
        /// <summary>
        /// Load ManageStars class
        /// </summary>
        /// <returns>loaded player</returns>
        private ManageStars loadPlayerData(String fileName)
        {
            XmlSerializer ser = new XmlSerializer(typeof(ManageStars));
            StreamReader reader = new StreamReader(fileName);

            ManageStars manageStars = (ManageStars)ser.Deserialize(reader);

            reader.Close();

            return manageStars;
        }
    }
}
