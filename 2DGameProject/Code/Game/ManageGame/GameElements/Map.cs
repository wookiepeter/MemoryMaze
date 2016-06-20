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
        static Cell[,] cellMap;
        Sprite mapSprite;

        MapFromTxt mapFromText = new MapFromTxt();

        public Map(int mapSizeX, int mapSizeY)
        {
            //// get Size per cell from CellTexture
            //sizePerCell = (int)(AssetManager.GetTexture(AssetManager.TextureName.Wall).Size.X);
            sizePerCell = 64;
            this.mapSizeX = mapSizeX;
            this.mapSizeY = mapSizeY;
            cellMap = RandomCellMap(this.mapSizeX, this.mapSizeY);

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
            cellMap = mapFromText.CreateMap(filename);

            sizePerCell = _sizePerCell;

            mapSizeX = cellMap.GetLength(0);
            mapSizeY = cellMap.GetLength(1);

            mapSprite = new Sprite(new Texture(AssetManager.GetTexture(AssetManager.TextureName.Wall)));
        }

        // TRASH could be deleted now
        private Cell[,] RandomCellMap(int sizeX, int sizeY)
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

        public void Draw(RenderWindow win, View view)
        {
            for (int i = 0; i < mapSizeX; i++)
            {
                for (int j = 0; j < mapSizeY; j++)
                {
                    mapSprite.Texture = cellMap[j, i].GetTexture(new Vector2i(j, i));
                    mapSprite.Scale = new Vector2f(sizePerCell / mapSprite.Texture.Size.X, sizePerCell / mapSprite.Texture.Size.Y);
                    mapSprite.Position = new Vector2(j * sizePerCell, i * sizePerCell);
                    win.Draw(mapSprite);
                }
            }
        }
        
        // Massive but simple(16 different values based on 4 different "booleans")
        public static Texture chooseGroundTexture(Vector2i position)
        {
            if(cellMap[position.X-1, position.Y].IsWalkable())
            {
                if(cellMap[position.X, position.Y-1].IsWalkable())
                {
                    if(cellMap[position.X+1, position.Y].IsWalkable())
                    {
                        if(cellMap[position.X, position.Y+1].IsWalkable())
                        {
                            return AssetManager.GetTexture(AssetManager.TextureName.Ground);
                        }
                        else
                        {
                            return AssetManager.GetTexture(AssetManager.TextureName.Ground);
                        }
                    }
                    else
                    {
                        if (cellMap[position.X, position.Y + 1].IsWalkable())
                        {
                            return AssetManager.GetTexture(AssetManager.TextureName.Ground);
                        }
                        else
                        {
                            return AssetManager.GetTexture(AssetManager.TextureName.Ground2lefttop);
                        }
                    }
                }
                else
                {
                    if (cellMap[position.X + 1, position.Y].IsWalkable())
                    {
                        if (cellMap[position.X, position.Y + 1].IsWalkable())
                        {
                            return AssetManager.GetTexture(AssetManager.TextureName.Ground);
                        }
                        else
                        {
                            return AssetManager.GetTexture(AssetManager.TextureName.Ground);
                        }
                    }
                    else
                    {
                        if (cellMap[position.X, position.Y + 1].IsWalkable())
                        {
                            return AssetManager.GetTexture(AssetManager.TextureName.Ground2leftbottom);
                        }
                        else
                        {
                            return AssetManager.GetTexture(AssetManager.TextureName.Ground2lefttop);
                        }
                    }
                }
            }
            else
            {
                if (cellMap[position.X, position.Y - 1].IsWalkable())
                {
                    if (cellMap[position.X + 1, position.Y].IsWalkable())
                    {
                        if (cellMap[position.X, position.Y + 1].IsWalkable())
                        {
                            return AssetManager.GetTexture(AssetManager.TextureName.Ground);
                        }
                        else
                        {
                            return AssetManager.GetTexture(AssetManager.TextureName.Ground2topright);
                        }
                    }
                    else
                    {
                        if (cellMap[position.X, position.Y + 1].IsWalkable())
                        {
                            return AssetManager.GetTexture(AssetManager.TextureName.Ground2vertical);
                        }
                        else
                        {
                            return AssetManager.GetTexture(AssetManager.TextureName.Ground1top);
                        }
                    }
                }
                else
                {
                    if (cellMap[position.X + 1, position.Y].IsWalkable())
                    {
                        if (cellMap[position.X, position.Y + 1].IsWalkable())
                        {
                            return AssetManager.GetTexture(AssetManager.TextureName.Ground2bottomright);
                        }
                        else
                        {
                            return AssetManager.GetTexture(AssetManager.TextureName.Ground1right);
                        }
                    }
                    else
                    {
                        if (cellMap[position.X, position.Y + 1].IsWalkable())
                        {
                            return AssetManager.GetTexture(AssetManager.TextureName.Ground1bottom);
                        }
                        else
                        {
                            return AssetManager.GetTexture(AssetManager.TextureName.Ground);
                        }
                    }
                }
            }
        }
        
        public Boolean CellIsWalkable(Vector2i position)
        {
            if (position.X >= mapSizeX || position.X < 0 || position.Y >= mapSizeY || position.Y < 0)
            {
                return false;
            }
            return cellMap[position.X, position.Y].IsWalkable();
        }

        public Boolean CellIsMovable(Vector2i position)
        {
            if(position.X >= mapSizeX ||position.X < 0 ||position.Y >= mapSizeY || position.Y <0)
            {
                return false;
            }
            return cellMap[position.X, position.Y].IsMovable();
        }

        public Boolean MoveIsPossible(Vector2i position, Vector2i move)
        {
            if (CellIsMovable(position + move) && CellIsWalkable(position + move + move))
                return true;
            return false;
        }

        public void MoveBlock(Vector2i position, Vector2i move)
        {
            Vector2i targetBlock = position + move + move;
            Vector2i moveBlock = position + move;
            cellMap[targetBlock.X, targetBlock.Y] = cellMap[moveBlock.X, moveBlock.Y];
            cellMap[moveBlock.X, moveBlock.Y] = new Cell(cellContent.Empty);
        }

        public int GetSizePerCell()
        {
            return sizePerCell;
        }
    }
}
