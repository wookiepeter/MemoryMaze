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
        

        public Game()
        {
            level = new Level();
        }

        public void Update(float deltaTime)
        {
            level.update(deltaTime);
        }

        public void draw(RenderWindow win, View view)
        {
            
            level.draw(win, view);
        }
    }
}
