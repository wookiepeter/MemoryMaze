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
            exactPosition = new Vector2f(position.X * map.sizePerCell, position.Y * map.sizePerCell);
            sprite.Scale = new Vector2f((float)map.sizePerCell/(float)sprite.Texture.Size.X, (float)map.sizePerCell / (float)sprite.Texture.Size.Y);
            
        }

        // CopyConstructor
        public Key(Key _key)
        {
            position = _key.position;
            exactPosition = _key.exactPosition;
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

        override public void Draw(RenderTexture win, View view, Vector2f relViewDis)
        {
            sprite.Position = exactPosition + relViewDis;
            win.Draw(sprite);
        }

    }
}
