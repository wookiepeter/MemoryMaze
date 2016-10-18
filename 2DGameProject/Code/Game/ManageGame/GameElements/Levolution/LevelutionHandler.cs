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
        List<LeverOutlines> outlines;
        int sizePerCell;

        public LevelutionHandler(List<Lever> _leverList, int _sizePerCell)
        {
            leverList = new List<Lever>();
            sizePerCell = _sizePerCell;
            foreach(Lever lever in _leverList)
            {
                leverList.Add(lever.Copy());
            }
            outlines = new List<LeverOutlines>();
            foreach(Lever lever in leverList)
            {
                outlines.Add(new LeverOutlines(lever, _sizePerCell));
            }
        }

        private LevelutionHandler(LevelutionHandler _levelutionHandler, int _sizePerCell)
        {
            leverList = new List<Lever>();
            sizePerCell = _sizePerCell;
            foreach(Lever lev in _levelutionHandler.leverList)
            {
                leverList.Add(lev.Copy());
            }
            outlines = new List<LeverOutlines>();
            foreach(Lever lever in leverList)
            {
                outlines.Add(new LeverOutlines(lever, _sizePerCell));
            }
        }

        public LevelutionHandler Copy()
        {
            return new LevelutionHandler(this, sizePerCell);
        }

        public void Update(Player player, Map map, float deltaTime)
        {
            foreach(Lever lev in leverList)
            {
                lev.Update(player, map, deltaTime);
            }
            foreach(LeverOutlines outline in outlines)
            {
                outline.Update(deltaTime);
            }
        }

        public void Draw(RenderTexture win, View view, Vector2f relViewDis)
        {
            foreach (Lever lev in leverList)
            {
                lev.Draw(win, view, relViewDis);
            }
            
        }

        public void DrawOutlines(RenderTexture win, View view, Vector2f relViewDis)
        {
            foreach(LeverOutlines outline in outlines)
            {
                outline.Draw(win, view, relViewDis);
            }
        }
    }
}
