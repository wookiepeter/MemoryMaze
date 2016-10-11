using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;


namespace MemoryMaze
{
    public class PornLever : Lever
    {
        public PornLever(Vector2i _position, Map map, List<MapManipulation> _mapManiList)
        {
            position = _position;
            mapManilList = new List<MapManipulation>();
            foreach(MapManipulation mapmani in _mapManiList)
            {
                mapManilList.Add(mapmani);
            }
            exactPosition = new Vector2f(position.X * map.GetSizePerCell(), position.Y * map.GetSizePerCell());
            sprite.Scale = new Vector2f((float)map.GetSizePerCell() / (float)sprite.Texture.Size.X, (float)map.GetSizePerCell() / (float)sprite.Texture.Size.Y);
        }

        private PornLever(PornLever _lever)
        {
            position = _lever.position;
            sprite.Position = _lever.sprite.Position;
            sprite.Scale = _lever.sprite.Scale;
            mapManilList = new List<MapManipulation>();
            exactPosition = _lever.exactPosition;
            foreach(MapManipulation mani in _lever.mapManilList)
            {
                mapManilList.Add(mani.Copy());
            }
        }

        override public Lever Copy()
        {
            return new PornLever(this);
        }

        override public void Update(Player player, Map map, float deltaTime)
        {
            if (map.GetContentOfCell(position) == cellContent.Movable)
            {
                if (!active)
                {
                    Execute(map, player);
                    active = !active;
                }
            }
            else
            {
                if (active)
                {
                    Execute(map, player);
                    active = !active;
                }
            }
        }



        override protected void Execute(Map map, Player player)
        {
            foreach(MapManipulation mani in mapManilList)
            {
                if (!player.getListOfBotPositions().Contains(mani.position))
                    mani.execute(map);
            }
        }

        override public void Draw(RenderTexture win, View view, Vector2f relViewDis)
        {
            sprite.Position = exactPosition + relViewDis;
            win.Draw(sprite);
        }



//////////////////////////////////////////////////////////////////////////////////////////

        static bool mirkohatrecht = false;

        /// <summary>
        /// This should NEVER be called
        /// </summary>
        public static void MakeHeile()
        {
            
            if (Rand.IntValue(0, 2) == 0)
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
