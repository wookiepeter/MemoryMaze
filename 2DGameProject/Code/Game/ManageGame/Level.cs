using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

namespace MemoryMaze
{
    class Level
    {
        //Contains and manages the acutal Levels
        Player player;
        Map map;
        public Level()
        {
            map = new Map("Assets/MapFiles/ExampleMap.txt", 64);
            player = new Player(new Vector2i(1, 1), map);
        }
        public void update(float deltaTime)
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
