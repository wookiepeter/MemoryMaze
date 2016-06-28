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

        GameState nextGameState;

        Text levelNumber = new Text("someText", new Font("Assets/Fonts/calibri.ttf"));

        public Game()
        {
            levelList.Add(new Level("Assets/MapFiles/ExampleMap.txt", 64, new Vector2i(1, 1)));
            levelList.Add(new Level("Assets/MapFiles/ExampleMap.txt", 32, new Vector2i(4, 3)));
            level = levelList[curIndex];
            nextGameState = GameState.InGame;

            levelNumber.CharacterSize = 20;
            levelNumber.Color = Color.Red;
        }

        public GameState Update(float deltaTime)
        {
            levelStatus = level.update(deltaTime);

            if (levelStatus == 1)
            {
                curIndex++;
                if (curIndex >= levelList.Count)
                {
                    nextGameState = GameState.MainMenu;
                }
                else
                {
                    Logger.Instance.Write("THIS SHOULD HAPPEN ONCE", 2);
                    level = null;
                    level = levelList[curIndex];
                }
            }
            return nextGameState;
        }

        public void draw(RenderWindow win, View view)
        {
            level.draw(win, view);
        }

        public void DrawGUI(GUI gui, float deltaTime)
        {
            levelNumber.DisplayedString = "Level " + curIndex + " of " + (levelList.Count-1);
            levelNumber.Position = new Vector2f(gui.view.Size.X-100, gui.view.Size.Y-50);
            gui.Draw(levelNumber);
        }
    }
}
