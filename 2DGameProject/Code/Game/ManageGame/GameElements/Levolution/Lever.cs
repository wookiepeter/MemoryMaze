using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;


namespace MemoryMaze
{
    public class Lever
    {
        Vector2i position;
        Sprite sprite;

        public Lever()
        {

        }

        private Lever(Lever _lever)
        {

        }

        public Lever Copy()
        {
            return new Lever(this);
        }

        public void Update(List<Vector2i> botPosList, Map map, float deltaTime)
        {

        }

        public void Draw(RenderTexture win, View view)
        {

        }

//////////////////////////////////////////////////////////////////////////////////////////

        static bool mirkohatrecht = false;

        /// <summary>
        /// This should NEVER be called
        /// </summary>
        public static void MakeHeile()
        {
            Random rand = new Random();
            if (rand.Next(0, 1) == 0)
                mirkohatrecht = true;

            if (mirkohatrecht)
            {
                MakeHeile();
            }
            else
            {
                MakePutt();
            }
        }
        /// <summary>
        /// This Too...
        /// </summary>
        public static void MakePutt()
        {
            for (int i = 0; i < 100; i++)
                i--;
        }
    }
}
