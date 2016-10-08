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
            exactPosition = new Vector2f(position.X * map.sizePerCell, position.Y * map.sizePerCell);
            sprite.Scale = new Vector2f((float)map.sizePerCell / (float)sprite.Texture.Size.X, (float)map.sizePerCell / (float)sprite.Texture.Size.Y);
        }
        public BlueItem(BlueItem _blueItem)
        {
            position = _blueItem.position;
            sprite.Position = _blueItem.sprite.Position;
            sprite.Scale = _blueItem.sprite.Scale;
            exactPosition = _blueItem.exactPosition;
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

        override public void Draw(RenderTexture win, View view, Vector2f relViewDis)
        {
            sprite.Position = exactPosition + relViewDis;
            win.Draw(sprite);
        }
    }
}
