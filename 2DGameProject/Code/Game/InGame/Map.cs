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
    public class Map
    {
        public int mapSizeX { get; private set; }
        public int mapSizeY { get; private set; }
        public int sizePerCell { get; private set; }
        Cell[,] cellMap;

        public Map(int mapSizeX, int mapSizeY)
        {
            sizePerCell = 64;
            this.mapSizeX = mapSizeX;
            this.mapSizeY = mapSizeY;
            cellMap = randomCellMap(this.mapSizeX, this.mapSizeY);
        }

        private Cell[,] randomCellMap(int sizeX, int sizeY)
        {
            Cell[,] newCellArray = new Cell[sizeX, sizeY];
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    newCellArray[i, j] = new Cell((cellContent)Rand.IntValue(0, (int)cellContent.Last));
                }
            }
            return newCellArray;
        }

        public void draw(RenderWindow win, View view)
        {
            Sprite mapSprite = new Sprite(new Texture(AssetManager.GetTexture(AssetManager.TextureName.Wall)));
            mapSprite.Scale = new Vector2f(sizePerCell/ mapSprite.Texture.Size.X, sizePerCell/mapSprite.Texture.Size.Y);
            for (int i = 0; i < mapSizeX; i++)
            {
                for (int j = 0; j < mapSizeY; j++)
                {
                    mapSprite.Position = new Vector2(i * sizePerCell, j * sizePerCell);
                    mapSprite.Texture = cellMap[i, j].getTexture();
                    win.Draw(mapSprite);
                }
            }
        }

        public Boolean cellIsWalkable(Vector2i position)
        {
            if (position.X >= mapSizeX || position.X < 0 || position.Y >= mapSizeY || position.Y < 0)
            {
                return false;
            }
            return cellMap[position.X, position.Y].isWalkable();
        }

        public int getSizePerCell()
        {
            return sizePerCell;
        }
    }
}
