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

        public Game(int id)
        {
            Console.WriteLine("Sind in der GameFunktion mit der: " +id);
            if(id == 0)
                Tutorial();

            if (id == 1)
                MainCampaign();


            level = levelList[curIndex].Copy();

            levelNumber.CharacterSize = 20;
            levelNumber.Color = Color.Red;
        }
        void Tutorial()
        {
            levelList.Add(new Level("Assets/MapFiles/map00.txt", 64, new Vector2i(5,7), 1));
            nextGameState = GameState.Intro;
        }
        void MainCampaign()
        {
            levelList.Add(new Level("Assets/MapFiles/map01.txt", 64, new Vector2i(2, 2), 1));
            levelList.Add(new Level("Assets/MapFiles/map02.txt", 64, new Vector2i(2, 3), 1));
            levelList.Add(new Level("Assets/MapFiles/map03.txt", 64, new Vector2i(5, 3), 1));
            levelList.Add(new Level("Assets/MapFiles/map04.txt", 64, new Vector2i(8,1), 1));
            levelList.Add(new Level("Assets/MapFiles/map05.txt", 64, new Vector2i(7, 5), 1));
            levelList.Add(new Level("Assets/MapFiles/map06.txt", 64, new Vector2i(3, 1), 1));
            levelList.Add(new Level("Assets/MapFiles/map07.txt", 64, new Vector2i(4, 3), 1));
            levelList.Add(new Level("Assets/MapFiles/map08.txt", 64, new Vector2i(7, 2), 1));
            levelList.Add(new Level("Assets/MapFiles/map09.txt", 64, new Vector2i(10, 4), 1));
            nextGameState = GameState.InGame;
        }

        public GameState Update(float deltaTime)
        {
            levelStatus = level.update(deltaTime);

            if (levelStatus == 1)
            {
                int curScore = level.getScoreCounter();
                curIndex++;
                if (curIndex >= levelList.Count)
                {
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
    }
}
