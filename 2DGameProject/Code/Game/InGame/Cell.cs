using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML;
using SFML.Graphics;
using SFML.Window;


namespace MemoryMaze
{
    // enum goes global
    enum cellContent
    {
        Empty,
        Item,
        Wall,
        Movable,
        Last
    };

    class Cell
    {
        cellContent content;

        public Cell(cellContent _content)
        {
            content = _content;
        }

        public cellContent getContent()
        {
            return content;
        }

        public void setContent(cellContent _content)
        {
            content = _content;
        }

        public Boolean isWalkable()
        {
            if (content == cellContent.Wall || content == cellContent.Movable)
                return false;
            else
                return true;
        }
        
        public Boolean isMovable()
        {
            return (content == cellContent.Movable);
        }

        public Color getColor()
        {
            int contentIndex = (int) this.content;
            switch(contentIndex)
            {
                case 0: return Color.Red;
                case 1: return Color.Green;
                case 2: return Color.Black;
                default: return Color.White;
            }
        }

        public Texture getTexture()
        {
            switch (this.content)
            {
                case cellContent.Empty: return AssetManager.GetTexture(AssetManager.TextureName.Ground);
                case cellContent.Item: return AssetManager.GetTexture(AssetManager.TextureName.Item);
                case cellContent.Wall: return AssetManager.GetTexture(AssetManager.TextureName.Wall);
                case cellContent.Movable: return AssetManager.GetTexture(AssetManager.TextureName.Movable);
                default: return AssetManager.GetTexture(AssetManager.TextureName.WhitePixel);
            }
        }
    }
}
