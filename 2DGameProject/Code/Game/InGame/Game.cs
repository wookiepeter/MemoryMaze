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
        Player player;
        Map map;

        public Game()
        {
            map = new Map("Assets/MapFiles/ExampleMap.txt", 64);
            player = new Player(new Vector2i(1, 1), map);
        }

        public void Update(float deltaTime)
        {
            player.Update(deltaTime, map);
        }

        public void draw(RenderWindow win, View view)
        {
            map.Draw(win, view);
            player.Draw(win, view);
        }
    }
}
