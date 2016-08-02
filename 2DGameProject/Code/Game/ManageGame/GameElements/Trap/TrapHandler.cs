using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

namespace MemoryMaze
{
    class TrapHandler
    {
        List<AntivirTrap> antiviTrapList;


        public TrapHandler(Map map) {

            antiviTrapList = new List<AntivirTrap>();
            foreach (AntivirTrap trap in GetTrapsFromMap(map))
            {
                antiviTrapList.Add(trap.Copy());
            }
        }

        TrapHandler(TrapHandler _trapHandler)
        {
            antiviTrapList = new List<AntivirTrap>();
            foreach (AntivirTrap trap in _trapHandler.antiviTrapList)
            {
                antiviTrapList.Add(trap.Copy());
            }
        }

        public TrapHandler Copy()
        {
            return new TrapHandler(this);
        }

        public void Update(Map map, Player player, float deltaTime)
        { 
 
            //antiviTrapList.Update();
            List<Vector2i> botPosList = player.getListOfBotPositions();
            List<Item> removeList = new List<Item>();
            foreach (AntivirTrap trap in antiviTrapList)
            {
                trap.Update(map, deltaTime);
                foreach (Vector2i vec in botPosList)
                {
                    if (!trap.deleted)
                    {
                        if (trap.position.X == vec.X && trap.position.Y == vec.Y)
                        {
                            player.isAlive = false;
                            
                        }
                    }
                }
            }
            antiviTrapList.RemoveAll(a => a.deleted == true);

        }
        public void Draw(RenderWindow win, View view)
        {
            foreach (AntivirTrap trap in antiviTrapList)
                trap.Draw(win, view);
        }

        private List<AntivirTrap> GetTrapsFromMap(Map map)
        {
            List<AntivirTrap> result = new List<AntivirTrap>();
            for (int j = 0; j < map.mapSizeY; j++)
            {
                for (int i = 0; i < map.mapSizeX; i++)
                {
                    Vector2i curPos = new Vector2i(i, j);
                    if (map.getContentOfCell(curPos) == cellContent.TrapTile)
                    {
                        result.Add(new AntivirTrap(new Vector2i(i, j), map));
                    }
                }
            }
            return result;
        }

    }
}
