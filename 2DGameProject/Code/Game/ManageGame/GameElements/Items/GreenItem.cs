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
    class GreenItem : Item
    {
        Sprite sprite = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.GreenItem));

        public GreenItem() { }
        public GreenItem(Vector2i _position, Map map)
        {
            //Logger.Instance.Write(_position.ToString(), 0);
            position = _position;
            deleted = false;
            exactPosition = new Vector2f(position.X * map.sizePerCell + map.sizePerCell * 0.25f, position.Y * map.sizePerCell + map.sizePerCell * 0.25f);
            sprite.Scale = new Vector2f((float)map.sizePerCell * 0.5f / (float)sprite.Texture.Size.X, (float)map.sizePerCell * 0.5f / (float)sprite.Texture.Size.Y);
        }
        public GreenItem(GreenItem _greenItem)
        {
            position = _greenItem.position;
            sprite.Position = _greenItem.sprite.Position;
            sprite.Scale = _greenItem.sprite.Scale;
            exactPosition = _greenItem.exactPosition;
        }

        override public Item Copy()
        {
            return new GreenItem(this);
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
