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

        public LevelutionHandler(Lever _lever)
        {
            leverList = new List<Lever>();
            leverList.Add(_lever);
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

        public void Update(List<Vector2i> botPosList, Map map, float deltaTime)
        {
            foreach(Lever lev in leverList)
            {
                lev.Update(botPosList, map, deltaTime);
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
