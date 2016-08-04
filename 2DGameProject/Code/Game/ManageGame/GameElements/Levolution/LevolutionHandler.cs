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
        public LevelutionHandler()
        {

        }

        private LevelutionHandler(LevelutionHandler _levolutionHandler)
        {

        }

        public LevelutionHandler Copy()
        {
            return new LevelutionHandler(this);
        }

        public void Update(List<Vector2i> botPosList, Map map, float deltaTime)
        {

        }

        public void Draw(RenderTexture win, View view)
        {

        }
    }
}
