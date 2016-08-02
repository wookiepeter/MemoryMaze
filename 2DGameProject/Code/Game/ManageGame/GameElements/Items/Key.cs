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
    public class Key : Item
    {
        Sprite sprite = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.Item));   

        // This doesn't check if the item is valid
        public Key(Vector2i _position, Map map)
        {
            Logger.Instance.Write(_position.ToString(), 0);
            position = _position;
            deleted = false;
            sprite.Position = new Vector2f(position.X * map.sizePerCell + map.sizePerCell*0.25f, position.Y * map.sizePerCell + map.sizePerCell*0.25f);
            sprite.Scale = new Vector2f((float)map.sizePerCell*0.5f/(float)sprite.Texture.Size.X, (float)map.sizePerCell * 0.5f / (float)sprite.Texture.Size.Y);
        }

        // CopyConstructor
        public Key(Key _key)
        {
            position = _key.position;
            sprite.Position = _key.sprite.Position;
            sprite.Scale = _key.sprite.Scale;
        }

        override public Item Copy()
        {
            return new Key(this);
        }

        override public void Update(Map map, float deltaTime)
        {
            if (!map.CellIsWalkable(position))
                deleted = true;
        }

        override public void Draw(RenderTexture win, View view)
        {
            win.Draw(sprite);
        }

    }
}
