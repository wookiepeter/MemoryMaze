using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
/*
    Verwaltet die Falle
*/
namespace MemoryMaze
{
    class TrapHandler
    {
        List<AntivirTrap> antiviTrapList;                                                   //Liste mit allen Fallen auf der Map
        public TrapHandler(Map map) {

            antiviTrapList = new List<AntivirTrap>();
            foreach (AntivirTrap trap in GetTrapsFromMap(map))                              //GetTrapsFromMap result List
            {
                antiviTrapList.Add(trap.Copy());                                            //Fügt jede Falle von der Map in die Liste
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
            List<Vector2i> botPosList = player.getListOfBotPositions();                              //Liste mit "allen" Positionen von Bot und Player
            //List<Item> removeList = new List<Item>();                                              //-> zu faul lösche nichts ^.^
            foreach (AntivirTrap trap in antiviTrapList)
            {                                                                                        //Alle aktuellen Fallen
                trap.Update(map, deltaTime);
                foreach (Vector2i vec in botPosList)                                                 //Alle Spieler(bots)
                {
                    if (trap.isAlive)                                                                //Lebt die Falle?
                    {
                        if (trap.position.X == vec.X && trap.position.Y == vec.Y)                    //Befindet sich ein Spieler(bot) auf der Falle?
                        {
                            if (player.controllid == 1)                                              //Ist es der RedBot?
                            {
                                player.botList.Find(b => b.id == 1).isAlive = false;                 //Loesche ihn!
                            }
                            else if (player.controllid == 2)                                         //Ist es der BlueBot?
                            {
                                player.botList.Find(b => b.id == 2).isAlive = false;                 //Loesche ihn!
                            }
                            else if (player.controllid == 0)                                         //Ist es der Player?
                            {
                                player.isAlive = false;                                              //Loesche ihn!

                            }
                        }
                        if (trap.position.X == vec.X && trap.position.Y == vec.Y && player.greenbot && player.controllid == 3)
                        {//Ist es der GrüneBot???
                            trap.isAlive = false;                                                    //Falle deaktiviert! ^.^
                        }
                    }
                        
                }
            }
           // antiviTrapList.RemoveAll(a => a.deleted == true);

        }
        public void Draw(RenderTexture win, View view)
        {
            foreach (AntivirTrap trap in antiviTrapList)
                trap.Draw(win, view);
        }

        private List<AntivirTrap> GetTrapsFromMap(Map map)                                  //Geht die Map durch und fügt bei jeder Collision die Falle in die Liste
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
            return result;                                                                   //Gibt fertige Liste zurück!
        }

    }
}
