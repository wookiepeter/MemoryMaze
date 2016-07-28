using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using SFML.Audio;

namespace MemoryMaze
{
    public class Key
    {
        Vector2i position;
        Sprite sprite = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.Item));
        Boolean exists = false;
        Boolean taken = false;

        public Key(Vector2i _position, Map map)
        {
            if (!map.isInMap(position))
            {
                throw new Exception("Item Position not on map");
            }
            else
            {
                if (!map.CellIsWalkable(position))
                    Logger.Instance.Write("Item is not on a walkable cell", 1);
                position = _position;
                sprite.Position = new Vector2f(position.X * map.sizePerCell + map.sizePerCell*0.2f, position.Y * map.sizePerCell + map.sizePerCell*0.2f);
                sprite.Scale = new Vector2f((float)sprite.TextureRect.Height / ((float)map.sizePerCell * 0.6f), (float)sprite.TextureRect.Height / ((float)map.sizePerCell * 0.6f));
            }

        }

        // CopyConstructor
        public Key(Key _key)
        {
            position = _key.position;
        }

        void Update(Map map, float deltaTime)
        {
            if (!map.CellIsWalkable(position))
                exists = false;
        }

        void Draw(RenderWindow win, View view)
        {
            win.Draw(sprite);
        } 

    }
}
