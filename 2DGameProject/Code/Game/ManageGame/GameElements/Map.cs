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

        MapFromTxt mapFromText = new MapFromTxt();

        public Map(int mapSizeX, int mapSizeY)
        {
            //// get Size per cell from CellTexture
            //sizePerCell = (int)(AssetManager.GetTexture(AssetManager.TextureName.Wall).Size.X);
            sizePerCell = 64;
            this.mapSizeX = mapSizeX;
            this.mapSizeY = mapSizeY;
            cellMap = randomCellMap(this.mapSizeX, this.mapSizeY);

            // Probably not possible to draw all Textures(with different image resolutions) in the same sprite
            // without using different sprites for different textures
            mapSprite = new Sprite(new Texture(AssetManager.GetTexture(AssetManager.TextureName.Wall)));
            // mapSprite.TextureRect = new IntRect(0, 0, (int)sizePerCell, (int)sizePerCell);
        }

        /// <summary>
        /// Constructs a Map from a given file.txt
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="_sizePerCell"></param>
        public Map(String filename, int _sizePerCell)
        {
            cellMap = mapFromText.createMap(filename);

            sizePerCell = _sizePerCell;

            mapSizeX = cellMap.GetLength(0);
            mapSizeY = cellMap.GetLength(1);

            mapSprite = new Sprite(new Texture(AssetManager.GetTexture(AssetManager.TextureName.Wall)));
        }

        // TRASH could be deleted now
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

        public Boolean cellIsMovable(Vector2i position)
        {
            if(position.X >= mapSizeX ||position.X < 0 ||position.Y >= mapSizeY || position.Y <0)
            {
                return false;
            }
            return cellMap[position.X, position.Y].isMovable();
        }

        public Boolean moveIsPossible(Vector2i position, Vector2i move)
        {
            if (cellIsMovable(position + move) && cellIsWalkable(position + move + move))
                return true;
            return false;
        }

        public void moveBlock(Vector2i position, Vector2i move)
        {
            Vector2i targetBlock = position + move + move;
            Vector2i moveBlock = position + move;
            cellMap[targetBlock.X, targetBlock.Y] = cellMap[moveBlock.X, moveBlock.Y];
            cellMap[moveBlock.X, moveBlock.Y] = new Cell(cellContent.Empty);
        }

        public int getSizePerCell()
        {
            return sizePerCell;
        }
    }
}
