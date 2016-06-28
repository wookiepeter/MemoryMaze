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
        //Contains and manages the actual Levels
        Player player;
        Enemy enemy;
        Map map;

        int mapStatus = 0;

        public Level(String mapfile, int sizePerCell, Vector2i position)
        {
            map = new Map(mapfile, sizePerCell);
            player = new Player(position, map);
            enemy = new Enemy(new Vector2i(2, 5), map, Enemy.EnemyKind.ANTIVIRUS);
        }

        public int update(float deltaTime)
        {
            map.Update(deltaTime);
            player.Update(deltaTime, map);
            if (map.CellIsGoal(player.mapPosition))
                mapStatus = 1;
            return mapStatus;
        }
        public void draw(RenderWindow win, View view)
        {
            map.Draw(win, view);
            player.Draw(win, view);
            enemy.draw(win, view);
        }
    }
}
