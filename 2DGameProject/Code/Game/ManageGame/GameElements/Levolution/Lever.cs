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

        List<MapManipulation> mapManilList;

        bool active = false;

        public Lever(Vector2i _position, Map map, List<MapManipulation> _mapManiList)
        {
            position = _position;
            mapManilList = new List<MapManipulation>();
            foreach(MapManipulation mapmani in _mapManiList)
            {
                mapManilList.Add(mapmani);
            }
            sprite.Position = new Vector2f(position.X * map.GetSizePerCell() + (float)map.GetSizePerCell() * 0.25f, 
                position.Y * map.GetSizePerCell() + (float)map.GetSizePerCell() * 0.25f);
            sprite.Scale = new Vector2f((float)map.GetSizePerCell() * 0.5f / (float)sprite.Texture.Size.X,
                (float)map.GetSizePerCell() * 0.5f / (float)sprite.Texture.Size.Y);
        }

        private Lever(Lever _lever)
        {
            position = _lever.position;
            sprite.Position = _lever.sprite.Position;
            sprite.Scale = _lever.sprite.Scale;
            mapManilList = new List<MapManipulation>();
            foreach(MapManipulation mani in _lever.mapManilList)
            {
                mapManilList.Add(mani.Copy());
            }
        }

        public Lever Copy()
        {
            return new Lever(this);
        }

        public void Update(Player player, Map map, float deltaTime)
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
                if(active)
                {
                    Execute(map, player);
                    active = !active;
                }
            }
                
        }

        private Boolean VirusOnLever(List<Vector2i> botPosList, Map map)
        {
            if (botPosList.Contains(position))
                return true;
            return false;
        }

        private void Execute(Map map, Player player)
        {
            foreach(MapManipulation mani in mapManilList)
            {
                if (!player.getListOfBotPositions().Contains(mani.position))
                    mani.execute(map);
            }
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
