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
        Enemy enemy;
        Map map;
        public Level()
        {
            map = new Map("Assets/MapFiles/ExampleMap.txt", 64);
            player = new Player(new Vector2i(1, 1), map);
            enemy = new Enemy(new Vector2i(2, 5), map, Enemy.EnemyKind.ANTIVIRUS);
        }
        public void update(float deltaTime)
        {
            player.Update(deltaTime, map);
            
        }
        public void draw(RenderWindow win, View view)
        {
            map.Draw(win, view);
            player.Draw(win, view);
            enemy.draw(win, view);
        }
    }
}
