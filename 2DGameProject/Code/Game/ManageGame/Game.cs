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
            {
                Console.WriteLine("Game was continued");
                MainCampaign();
            }
            // this does require the set level(levelToPlay) to be valid(both an existing level and an already unlocked one)
            if (id == 2)
            {
                Console.WriteLine("levelToPlay: " + ProfileConstants.levelToPlay);
                MainCampaign();
                curIndex = ProfileConstants.levelToPlay;
            }
            Console.WriteLine("Here Now " + curIndex);


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
            levelList.Add(new Level("Assets/MapFiles/1_Gelb/Gelb1.txt", 64, new Vector2i(2, 2), 1));
            levelList.Add(new Level("Assets/MapFiles/1_Gelb/Gelb2.txt", 64, new Vector2i(2, 3), 1));
            levelList.Add(new Level("Assets/MapFiles/1_Gelb/Gelb3.txt", 64, new Vector2i(4, 3), 1));
            levelList.Add(new Level("Assets/MapFiles/1_Gelb/Gelb9.txt", 64, new Vector2i(10, 4), 1));
            levelList.Add(new Level("Assets/MapFiles/1_Gelb/Gelb4.txt", 64, new Vector2i(8, 1), 1));
            levelList.Add(new Level("Assets/MapFiles/1_Gelb/Gelb5.txt", 64, new Vector2i(7, 3), 1));
            levelList.Add(new Level("Assets/MapFiles/1_Gelb/Gelb8.txt", 64, new Vector2i(7, 2), 1));
            levelList.Add(new Level("Assets/MapFiles/1_Gelb/Gelb6.txt", 64, new Vector2i(3, 1), 1));
            levelList.Add(new Level("Assets/MapFiles/1_Gelb/Gelb7.txt", 64, new Vector2i(4, 3), 1));
            levelList.Add(new Level("Assets/MapFiles/1_Gelb/Gelb_Bearbeiten.txt", 64, new Vector2i(7, 7), 1));
            levelList.Add(new Level("Assets/MapFiles/2_Rot/Rot1.txt", 64, new Vector2i(8, 3), 1));
            levelList.Add(new Level("Assets/MapFiles/2_Rot/Rot2.txt", 64, new Vector2i(1, 1), 1));
            levelList.Add(new Level("Assets/MapFiles/2_Rot/Rot3.txt", 64, new Vector2i(1, 4), 1));
            levelList.Add(new Level("Assets/MapFiles/2_Rot/Rot4.txt", 64, new Vector2i(7, 3), 1));
            levelList.Add(new Level("Assets/MapFiles/2_Rot/Rot5.txt", 64, new Vector2i(5, 7), 1));
            levelList.Add(new Level("Assets/MapFiles/2_Rot/Rot6.txt", 64, new Vector2i(5, 5), 1));
            levelList.Add(new Level("Assets/MapFiles/2_Rot/TRot1.txt", 64, new Vector2i(4, 3), 1));
            levelList.Add(new Level("Assets/MapFiles/2_Rot/TRot2.txt", 64, new Vector2i(6, 2), 1));
            levelList.Add(new Level("Assets/MapFiles/2_Rot/TRot3.txt", 64, new Vector2i(1, 6), 1));
            levelList.Add(new Level("Assets/MapFiles/2_Rot/TRot4.txt", 64, new Vector2i(4, 5), 1));
            levelList.Add(new Level("Assets/MapFiles/2_Rot/TRot5.txt", 64, new Vector2i(7, 4), 1));
            levelList.Add(new Level("Assets/MapFiles/2_Rot/TRot6.txt", 64, new Vector2i(8, 1), 1));
            levelList.Add(new Level("Assets/MapFiles/3_Grün/Grün1.txt", 64, new Vector2i(4, 3), 1));
            levelList.Add(new Level("Assets/MapFiles/3_Grün/Grün2.txt", 64, new Vector2i(10, 10), 1));
            levelList.Add(new Level("Assets/MapFiles/3_Grün/Grün9.txt", 64, new Vector2i(8, 6), 1));
            levelList.Add(new Level("Assets/MapFiles/3_Grün/Grün91.txt", 64, new Vector2i(4, 11), 1));
            levelList.Add(new Level("Assets/MapFiles/4_Blau/Blau0.txt", 64, new Vector2i(6,6), 1));
            levelList.Add(new Level("Assets/MapFiles/4_Blau/Blau1.txt", 64, new Vector2i(4, 1), 1));
            levelList.Add(new Level("Assets/MapFiles/4_Blau/Blau3.txt", 64, new Vector2i(5, 4), 1));
            levelList.Add(new Level("Assets/MapFiles/4_Blau/Blau5.txt", 64, new Vector2i(4, 5), 1));
            levelList.Add(new Level("Assets/MapFiles/4_Blau/Blau2.txt", 64, new Vector2i(5, 3), 1));
            levelList.Add(new Level("Assets/MapFiles/4_Blau/Blau4.txt", 64, new Vector2i(11, 3), 1));
            levelList.Add(new Level("Assets/MapFiles/4_Blau/Blau9.txt", 64, new Vector2i(6, 4), 1));
            levelList.Add(new Level("Assets/MapFiles/4_Blau/Blau91.txt", 64, new Vector2i(10, 3), 1));
            levelList.Add(new Level("Assets/MapFiles/4_Blau/Blau92.txt", 64, new Vector2i(10, 12), 1));
            levelList.Add(new Level("Assets/MapFiles/4_Blau/Blau93.txt", 64, new Vector2i(3, 5), 1));


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
                    level = levelList[curIndex].Copy();
                }
            }

            if(levelStatus == 2)
            {
                level = levelList[curIndex].Copy();
            }

            return nextGameState;
        }

        public void Draw(RenderTexture win, View view, float deltaTime)
        {
            Vector2f relativeViewDistance = win.GetView().Center - view.Center;
            level.Draw(win, view, relativeViewDistance, deltaTime);
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
