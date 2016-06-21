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
        //simply contains and manage all levels of the game
        Level level;
        List<Level> levelList = new List<Level>();
        int curIndex = 0;

        int levelStatus;

        GameState nextGameState;

        public Game()
        {
            levelList.Add(new Level("Assets/MapFiles/ExampleMap.txt", 64, new Vector2i(1, 1)));
            levelList.Add(new Level("Assets/MapFiles/ExampleMap.txt", 64, new Vector2i(4, 3)));
            level = levelList[curIndex];
            nextGameState = GameState.InGame;
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
                    level = levelList[curIndex];
                }
            }
            return nextGameState;
        }

        public void draw(RenderWindow win, View view)
        {
            level.draw(win, view);
        }
    }
}
