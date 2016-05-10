using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML;
using SFML.Graphics;
using SFML.Window;

namespace GameProject2D
{
    class Game
    {
        Player player;
        Map map;

        public Game()
        {
            player = new Player(new Vector2f(0, 0));
            map = new Map(10, 10);
        }

        public void Update(float deltaTime)
        {
            player.update(deltaTime);
        }

        public void draw(RenderWindow win, View view)
        {
            map.draw(win, view);
            player.draw(win, view);
        }
    }
}
