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
        Sprite sprite = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.Lever));
        MapManipulation mapManil;

        bool active = false;

        public Lever(Vector2i _position, Map map, MapManipulation _mapManil)
        {
            position = _position;
            mapManil = _mapManil;
            sprite.Position = new Vector2f(position.X * map.GetSizePerCell() + (float)map.GetSizePerCell() * 0.25f, 
                position.Y * map.GetSizePerCell() + (float)map.GetSizePerCell() * 0.25f);
            sprite.Scale = new Vector2f((float)map.GetSizePerCell() * 0.5f / (float)sprite.Texture.Size.X,
                (float)map.GetSizePerCell() * 0.5f / (float)sprite.Texture.Size.Y);
        }

        private Lever(Lever _lever)
        {
            position = _lever.position;
            mapManil = _lever.mapManil;
            sprite.Position = _lever.sprite.Position;
            sprite.Scale = _lever.sprite.Scale;
        }

        public Lever Copy()
        {
            return new Lever(this);
        }

        public void Update(List<Vector2i> botPosList, Map map, float deltaTime)
        {
            foreach(Vector2i vec in botPosList)
            {
                if(map.Vector2iAreEqual(vec, position))
                {
                    if(!active)
                    {
                        active = true;
                        Execute(map);
                    }
                    return;
                }
            }
            if (active)
                Execute(map);
            active = false;
        }

        private void Execute(Map map)
        {
            mapManil.execute(map);
        }

        public void Draw(RenderTexture win, View view)
        {
            win.Draw(sprite);
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
