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
    public enum cellContent
    {
        Empty,
        Item,
        RedItem,
        GreenItem,
        BlueItem,
        ScoreItem,
        Wall,
        Movable,
        TrapTile,
        Goal,
        Last
    };

    class Cell
    {
        cellContent content;

        public Cell(cellContent _content)
        {
            content = _content;
        }

        public cellContent GetContent()
        {
            return content;
        }

        public void SetContent(cellContent _content)
        {
            content = _content;
        }

        public Boolean IsWalkable()
        {
            if (content == cellContent.Wall || content == cellContent.Movable)
                return false;
            else
                return true;
        }
        
        public Boolean IsMovable()
        {
            return (content == cellContent.Movable);
        }

        public Boolean IsGoal()
        {
            return (content == cellContent.Goal);
        }

        public Boolean IsItem()
        {
            return (content == cellContent.Item || content == cellContent.RedItem || content == cellContent.BlueItem || (content == cellContent.GreenItem) || (content == cellContent.ScoreItem));

        }
        public Boolean IsTrap()
        {
            return (content == cellContent.TrapTile);
        }


        public Texture GetTexture(int groundIndex)
        {
            switch (this.content)
            {
                case cellContent.Empty: return getGroundTextureFromIndex(groundIndex);
                case cellContent.TrapTile: return getGroundTextureFromIndex(groundIndex);
                case cellContent.Item: return AssetManager.GetTexture(AssetManager.TextureName.Item);
                case cellContent.Wall: return AssetManager.GetTexture(AssetManager.TextureName.Wall);
                case cellContent.Movable: return AssetManager.GetTexture(AssetManager.TextureName.Movable);
                case cellContent.Goal: return getGoalTextureFromIndex(groundIndex);
                default: return AssetManager.GetTexture(AssetManager.TextureName.WhitePixel);
            }
        }

        private Texture getGroundTextureFromIndex(int groundIndex)
        {
            return AssetManager.GetTexture(AssetManager.TextureName.GroundLonely + groundIndex);
        }

        private Texture getGoalTextureFromIndex(int groundIndex)
        {
            return AssetManager.GetTexture(AssetManager.TextureName.GoalLonely + groundIndex);
        }
    }
}
