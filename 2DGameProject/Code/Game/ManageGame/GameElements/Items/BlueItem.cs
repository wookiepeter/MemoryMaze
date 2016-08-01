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
    class BlueItem : Item
    {
        Sprite sprite = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.BlueItem));

        public BlueItem() { }
        public BlueItem(Vector2i _position, Map map)
        {
            //Logger.Instance.Write(_position.ToString(), 0);
            position = _position;
            deleted = false;
            sprite.Position = new Vector2f(position.X * map.sizePerCell + map.sizePerCell * 0.25f, position.Y * map.sizePerCell + map.sizePerCell * 0.25f);
            sprite.Scale = new Vector2f((float)map.sizePerCell * 0.5f / (float)sprite.Texture.Size.X, (float)map.sizePerCell * 0.5f / (float)sprite.Texture.Size.Y);
        }
        public BlueItem(BlueItem _blueItem)
        {
            position = _blueItem.position;
            sprite.Position = _blueItem.sprite.Position;
            sprite.Scale = _blueItem.sprite.Scale;
        }

        override public Item Copy()
        {
            return new BlueItem(this);
        }
        override public void Update(Map map, float deltaTime)
        {
            if (!map.CellIsWalkable(position))
                deleted = true;
        }

        override public void Draw(RenderWindow win, View view)
        {
            win.Draw(sprite);
        }
    }
}
