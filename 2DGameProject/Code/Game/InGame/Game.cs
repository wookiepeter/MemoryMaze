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
            map = new Map(20, 20);
            player = new Player(new Vector2i(0, 0), map);
        }

        public void Update(float deltaTime)
        {
            player.update(deltaTime, map);
        }

        public void draw(RenderWindow win, View view)
        {
            map.draw(win, view);
            player.draw(win, view);
        }
    }
}
