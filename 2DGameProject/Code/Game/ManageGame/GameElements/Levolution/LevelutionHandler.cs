using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

namespace MemoryMaze
{
    public class LevelutionHandler
    {
        List<Lever> leverList;

        public LevelutionHandler(List<Lever> _leverList)
        {
            leverList = new List<Lever>();
            foreach(Lever lever in _leverList)
            {
                leverList.Add(lever.Copy());
            }
        }

        private LevelutionHandler(LevelutionHandler _levelutionHandler)
        {
            leverList = new List<Lever>();
            foreach(Lever lev in _levelutionHandler.leverList)
            {
                leverList.Add(lev.Copy());
            }
        }

        public LevelutionHandler Copy()
        {
            return new LevelutionHandler(this);
        }

        public void Update(Player player, Map map, float deltaTime)
        {
            foreach(Lever lev in leverList)
            {
                lev.Update(player, map, deltaTime);
            }
        }

        public void Draw(RenderTexture win, View view)
        {
            foreach(Lever lev in leverList)
            {
                lev.Draw(win, view);
            }
        }
    }
}
