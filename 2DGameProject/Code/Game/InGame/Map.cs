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
    public class Map
    {
        public int mapSizeX { get; private set; }
        public int mapSizeY { get; private set; }
        public int sizePerCell { get; private set; }
        Cell[,] cellMap;
        Sprite mapSprite;

        public Map(int mapSizeX, int mapSizeY)
        {
            //// get Size per cell from CellTexture
            //sizePerCell = (int)(AssetManager.GetTexture(AssetManager.TextureName.Wall).Size.X);
            sizePerCell = 100;
            this.mapSizeX = mapSizeX;
            this.mapSizeY = mapSizeY;
            cellMap = randomCellMap(this.mapSizeX, this.mapSizeY);

            // Probably not possible to draw all Textures(with different image resolutions) in the same sprite
            // without using different sprites for different textures
            mapSprite = new Sprite(new Texture(AssetManager.GetTexture(AssetManager.TextureName.Wall)));
            // mapSprite.TextureRect = new IntRect(0, 0, (int)sizePerCell, (int)sizePerCell);
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
            for (int i = 0; i < mapSizeX; i++)
            {
                for (int j = 0; j < mapSizeY; j++)
                {
                    mapSprite.Texture = cellMap[i, j].getTexture();
                    mapSprite.Scale = new Vector2f(sizePerCell / mapSprite.Texture.Size.X, sizePerCell / mapSprite.Texture.Size.Y);
                    mapSprite.Position = new Vector2(i * sizePerCell, j * sizePerCell);
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
