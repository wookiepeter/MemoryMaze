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
            levelList.Add(new Level("Assets/MapFiles/4_Blau/Blau13.txt", 64, new Vector2i(8,10), 1));
            nextGameState = GameState.Intro;
        }
        void MainCampaign()
        {
            levelList.Add(new Level("Assets/MapFiles/1_Gelb/Gelb01.txt", 64, new Vector2i(2, 2), 1));
            levelList.Add(new Level("Assets/MapFiles/1_Gelb/Gelb02-EinfuehrungOrdner.txt", 64, new Vector2i(2, 3), 1));
            levelList.Add(new Level("Assets/MapFiles/1_Gelb/Gelb03.txt", 64, new Vector2i(4, 3), 1));
            levelList.Add(new Level("Assets/MapFiles/1_Gelb/Gelb04.txt", 64, new Vector2i(8, 1), 1));
            levelList.Add(new Level("Assets/MapFiles/1_Gelb/Gelb05-EinfuehrungSchalter.txt", 64, new Vector2i(6,4), 1));
            levelList.Add(new Level("Assets/MapFiles/1_Gelb/Gelb06.txt", 64, new Vector2i(7, 3), 1));
            levelList.Add(new Level("Assets/MapFiles/1_Gelb/Gelb07.txt", 64, new Vector2i(10, 4), 1));
            levelList.Add(new Level("Assets/MapFiles/1_Gelb/Gelb08.txt", 64, new Vector2i(7, 2), 1));
            levelList.Add(new Level("Assets/MapFiles/1_Gelb/Gelb09.txt", 64, new Vector2i(3, 1), 1));
            levelList.Add(new Level("Assets/MapFiles/1_Gelb/Gelb10.txt", 64, new Vector2i(4, 3), 1));
            levelList.Add(new Level("Assets/MapFiles/1_Gelb/Gelb11.txt", 64, new Vector2i(7, 7), 1));

            levelList.Add(new Level("Assets/MapFiles/2_Rot/Rot01-EinfuehrungRoterBot.txt", 64, new Vector2i(8, 3), 1));
            levelList.Add(new Level("Assets/MapFiles/2_Rot/Rot02.txt", 64, new Vector2i(1, 1), 1));
            levelList.Add(new Level("Assets/MapFiles/2_Rot/Rot03.txt", 64, new Vector2i(1, 4), 1));
            levelList.Add(new Level("Assets/MapFiles/2_Rot/Rot04.txt", 64, new Vector2i(7, 2), 1));
            levelList.Add(new Level("Assets/MapFiles/2_Rot/Rot05.txt", 64, new Vector2i(5, 7), 1));
            levelList.Add(new Level("Assets/MapFiles/2_Rot/Rot06.txt", 64, new Vector2i(5, 5), 1));

            levelList.Add(new Level("Assets/MapFiles/2_Rot/TRot01-EinfuehrungTeleporter.txt", 64, new Vector2i(4, 3), 1));
            levelList.Add(new Level("Assets/MapFiles/2_Rot/TRot02.txt", 64, new Vector2i(6, 2), 1));
            levelList.Add(new Level("Assets/MapFiles/2_Rot/TRot03.txt", 64, new Vector2i(1, 6), 1));
            levelList.Add(new Level("Assets/MapFiles/2_Rot/TRot04.txt", 64, new Vector2i(1, 5), 1));
            levelList.Add(new Level("Assets/MapFiles/2_Rot/TRot05.txt", 64, new Vector2i(7, 4), 1));
            levelList.Add(new Level("Assets/MapFiles/2_Rot/TRot06.txt", 64, new Vector2i(8, 1), 1));

            levelList.Add(new Level("Assets/MapFiles/3_Gruen/Gruen01.txt", 64, new Vector2i(8, 3), 1));
            levelList.Add(new Level("Assets/MapFiles/3_Gruen/Gruen02.txt", 64, new Vector2i(4, 3), 1));
            levelList.Add(new Level("Assets/MapFiles/3_Gruen/Gruen03-NEW.txt", 64, new Vector2i(4, 3), 1));
            levelList.Add(new Level("Assets/MapFiles/3_Gruen/Gruen04-NEW.txt", 64, new Vector2i(4, 3), 1));
            levelList.Add(new Level("Assets/MapFiles/3_Gruen/Gruen05.txt", 64, new Vector2i(9, 9), 1));
            levelList.Add(new Level("Assets/MapFiles/3_Gruen/Gruen06.txt", 64, new Vector2i(8, 6), 1));
            levelList.Add(new Level("Assets/MapFiles/3_Gruen/Gruen07.txt", 64, new Vector2i(4, 10), 1));
            levelList.Add(new Level("Assets/MapFiles/3_Gruen/Gruen99Laby.txt", 64, new Vector2i(3,3), 1));
            levelList.Add(new Level("Assets/MapFiles/3_Gruen/Gruen08-NEW.txt", 64, new Vector2i(4, 3), 1));

            levelList.Add(new Level("Assets/MapFiles/4_Blau/Blau01.txt", 64, new Vector2i(5, 4), 1));
            levelList.Add(new Level("Assets/MapFiles/4_Blau/Blau02.txt", 64, new Vector2i(2, 5), 1));
            levelList.Add(new Level("Assets/MapFiles/4_Blau/Blau03-KatiBearbeiten.txt", 64, new Vector2i(4, 1), 1));
            levelList.Add(new Level("Assets/MapFiles/4_Blau/Blau04-KatiBearbeiten.txt", 64, new Vector2i(6,6), 1));
            levelList.Add(new Level("Assets/MapFiles/4_Blau/Blau05.txt", 64, new Vector2i(5, 4), 1));
            levelList.Add(new Level("Assets/MapFiles/4_Blau/Blau06.txt", 64, new Vector2i(4, 5), 1));
            levelList.Add(new Level("Assets/MapFiles/4_Blau/Blau07.txt", 64, new Vector2i(5, 3), 1));
            levelList.Add(new Level("Assets/MapFiles/4_Blau/Blau08.txt", 64, new Vector2i(11, 3), 1));
            levelList.Add(new Level("Assets/MapFiles/4_Blau/Blau09.txt", 64, new Vector2i(6, 4), 1));
            levelList.Add(new Level("Assets/MapFiles/4_Blau/Blau10.txt", 64, new Vector2i(2, 5), 1));
            levelList.Add(new Level("Assets/MapFiles/4_Blau/Blau11.txt", 64, new Vector2i(10, 3), 1));
            levelList.Add(new Level("Assets/MapFiles/4_Blau/Blau12.txt", 64, new Vector2i(10, 12), 1));
            


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
                    manageStars.UpdateScoreOfLevel(curIndex, level.getRating());
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
