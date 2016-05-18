using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML;
using SFML.Graphics;
using SFML.Window;


namespace GameProject2D
{
    // enum goes global
    enum cellContent
    {
        Empty,
        Item,
        Wall,
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
            if (content == cellContent.Wall)
                return false;
            else
                return true;
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
                default: return AssetManager.GetTexture(AssetManager.TextureName.WhitePixel);
            }
        }
    }
}
