using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML;
using SFML.Graphics;
using SFML.Window;

namespace MemoryMaze
{
    class Game
    {
        //simply contains and manages all levels of the game
        Level level;
        List<Level> levelList = new List<Level>();
        int curIndex = 0;

        int levelStatus;
        public int scoreItemCounter;
        GameState nextGameState;

        Text levelNumber = new Text("someText", new Font("Assets/Fonts/calibri.ttf"));

        ManageProfiles manageProfiles;
        ManageStars manageStars;

        public Game(int id)
        {
            Console.WriteLine("Sind in der GameFunktion mit der: " +id);
            if(id == 0)
                Tutorial();

            if (id == 1)
                MainCampaign();

            // this does require the set level(levelToPlay) to be valid(both an existing level and an already unlocked one)
            if (id == 2)
            {
                MainCampaign();
                curIndex = ProfileConstants.levelToPlay;
            }


            level = levelList[curIndex].Copy();

            levelNumber.CharacterSize = 20;
            levelNumber.Color = Color.Red;
        }
        void Tutorial()
        {
            levelList.Add(new Level("Assets/MapFiles/map00.txt", 64, new Vector2i(8,1), 1));
            nextGameState = GameState.Intro;
        }
        void MainCampaign()
        {
            levelList.Add(new Level("Assets/MapFiles/map01.txt", 64, new Vector2i(2, 2), 1));
            levelList.Add(new Level("Assets/MapFiles/map02.txt", 64, new Vector2i(2, 3), 1));
            levelList.Add(new Level("Assets/MapFiles/map03.txt", 64, new Vector2i(4, 3), 1));
            levelList.Add(new Level("Assets/MapFiles/map04.txt", 64, new Vector2i(8,1), 1));
            levelList.Add(new Level("Assets/MapFiles/map05.txt", 64, new Vector2i(7, 5), 1));
            levelList.Add(new Level("Assets/MapFiles/map06.txt", 64, new Vector2i(3, 1), 1));
            levelList.Add(new Level("Assets/MapFiles/map07.txt", 64, new Vector2i(4, 3), 1));
            levelList.Add(new Level("Assets/MapFiles/map08.txt", 64, new Vector2i(7, 2), 1));
            levelList.Add(new Level("Assets/MapFiles/map09.txt", 64, new Vector2i(10, 4), 1));
            levelList.Add(new Level("Assets/MapFiles/map10.txt", 64, new Vector2i(8, 3), 1));
            levelList.Add(new Level("Assets/MapFiles/map11.txt", 64, new Vector2i(1, 1), 1));
            levelList.Add(new Level("Assets/MapFiles/map12.txt", 64, new Vector2i(1, 4), 1));
            levelList.Add(new Level("Assets/MapFiles/map13.txt", 64, new Vector2i(7, 3), 1));
            levelList.Add(new Level("Assets/MapFiles/map14.txt", 64, new Vector2i(5, 7), 1));
            levelList.Add(new Level("Assets/MapFiles/map15.txt", 64, new Vector2i(4,3), 1));
            levelList.Add(new Level("Assets/MapFiles/map16.txt", 64, new Vector2i(6,2), 1));
            levelList.Add(new Level("Assets/MapFiles/map17.txt", 64, new Vector2i(1,6), 1));
            levelList.Add(new Level("Assets/MapFiles/map18.txt", 64, new Vector2i(4,5), 1));
            levelList.Add(new Level("Assets/MapFiles/map19.txt", 64, new Vector2i(7,4), 1));
            levelList.Add(new Level("Assets/MapFiles/map20.txt", 64, new Vector2i(8,1), 1));
            levelList.Add(new Level("Assets/MapFiles/Chris06.txt", 64, new Vector2i(7,7), 1));
            levelList.Add(new Level("Assets/MapFiles/Chris01.txt", 64, new Vector2i(8,6), 1));
            levelList.Add(new Level("Assets/MapFiles/chris02.txt", 64, new Vector2i(4,10), 1));
            levelList.Add(new Level("Assets/MapFiles/Chris03.txt", 64, new Vector2i(6,4), 1));
            levelList.Add(new Level("Assets/MapFiles/Chis07.txt", 64, new Vector2i(5,5), 1));
            levelList.Add(new Level("Assets/MapFiles/Chris04.txt", 64, new Vector2i(10,3), 1));
            levelList.Add(new Level("Assets/MapFiles/Chris05.txt", 64, new Vector2i(10,12), 1));
            levelList.Add(new Level("Assets/MapFiles/Chris08.txt", 64, new Vector2i(2, 5), 1));
            levelList.Add(new Level("Assets/MapFiles/Grün1.txt", 64, new Vector2i(4, 3), 1));
            levelList.Add(new Level("Assets/MapFiles/4_Blau/Blau1.txt", 64, new Vector2i(4, 1), 1));
            levelList.Add(new Level("Assets/MapFiles/4_Blau/Blau2.txt", 64, new Vector2i(5, 3), 1));
            levelList.Add(new Level("Assets/MapFiles/4_Blau/Blau3.txt", 64, new Vector2i(5, 4), 1));
            levelList.Add(new Level("Assets/MapFiles/4_Blau/Blau4.txt", 64, new Vector2i(11, 3), 1));
            levelList.Add(new Level("Assets/MapFiles/4_Blau/Blau5.txt", 64, new Vector2i(4, 5), 1));
            nextGameState = GameState.InGame;

            // manageProfiles has to load all active profiles before it can be accessed
            manageProfiles = new ManageProfiles();
            manageProfiles = manageProfiles.loadManageProfiles();
            manageStars = new ManageStars();
            Console.WriteLine("playerName: " + manageProfiles.getActiveProfileName());
            manageStars = manageStars.loadManageStars(manageProfiles.getActiveProfileName(), levelList.Count);
            curIndex = manageStars.getIndexOfFirstUnsolvedLevel();
            Console.WriteLine(curIndex);
        }

        public GameState Update(float deltaTime)
        {
            levelStatus = level.update(deltaTime);

            if (levelStatus == 1)
            {
                int curScore = level.getScoreCounter();
                if (nextGameState == GameState.InGame)
                {
                    manageStars.UpdateScoreOfLevel(curIndex, levelList[curIndex].getRating());
                }
                curIndex++;
                if (curIndex >= levelList.Count)
                {
                    SaveGame();
                    nextGameState = GameState.MainMenu;
                }
                else
                {
                    levelList[curIndex].setScoreCounter(curScore);
                    Logger.Instance.Write("THIS SHOULD HAPPEN ONCE", 2);
                    level = levelList[curIndex].Copy();
                }
            }

            if(levelStatus == 2)
            {
                level = levelList[curIndex].Copy();
            }

            return nextGameState;
        }

        public void Draw(RenderTexture win, View view)
        {
            Vector2f relativeViewDistance = win.GetView().Center - view.Center;
            level.Draw(win, view, relativeViewDistance);
        }

        public void DrawGUI(GUI gui, float deltaTime)
        {
            levelNumber.DisplayedString = "Level " + curIndex + " of " + (levelList.Count-1);
            levelNumber.Position = new Vector2f(gui.view.Size.X-100, gui.view.Size.Y-50);
            gui.Draw(levelNumber);
            level.DrawGUI(gui, deltaTime);
        }

        public void SaveGame()
        {
            if(nextGameState == GameState.InGame)
            {
                manageStars.saveManageStars(manageProfiles.getActiveProfileName());
            }
        }
    }
}
