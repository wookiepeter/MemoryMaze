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
        RectangleShape mapSprite;

        ItemList itemList;
        MapFromTxt mapFromText = new MapFromTxt();

        

        public Map(int mapSizeX, int mapSizeY)
        {
            //// get Size per cell from CellTexture
            //sizePerCell = (int)(AssetManager.GetTexture(AssetManager.TextureName.Wall).Size.X);
            sizePerCell = 64;
            this.mapSizeX = mapSizeX;
            this.mapSizeY = mapSizeY;

            // Probably not possible to draw all Textures(with different image resolutions) in the same sprite
            // without using different sprites for different textures
            //mapSprite = new RectangleShape();
            //mapSprite.Size = new Vector2f(sizePerCell, sizePerCell);
            //mapSprite.Texture = new Texture(AssetManager.GetTexture(AssetManager.TextureName.Wall));
            // mapSprite.TextureRect = new IntRect(0, 0, (int)sizePerCell, (int)sizePerCell);
        }

        /// <summary>
        /// Constructs a Map from a given file.txt
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="_sizePerCell"></param>
        // all variables initialized here need to be initialized in the copyconstructor too
        public Map(String filename, int _sizePerCell)
        {
            cellMap = mapFromText.CreateMap(filename);

            sizePerCell = _sizePerCell;

            mapSizeX = cellMap.GetLength(0);
            //Logger.Instance.Write("mapSizeX: " + mapSizeX, 2);
            mapSizeY = cellMap.GetLength(1);
            //Logger.Instance.Write("mapSizeY: " + mapSizeY, 2);

            mapSprite = new RectangleShape();
            mapSprite.Size = new Vector2f(sizePerCell, sizePerCell);
            mapSprite.Texture = new Texture(AssetManager.GetTexture(AssetManager.TextureName.Wall));
        }

        // Constructor for the Copy function
        Map(int _sizePerCell, Cell[,] _cellMap, int _mapSizeX, int _mapSizeY, RectangleShape _mapSprite)
        {
            
            sizePerCell = _sizePerCell;
            cellMap = new Cell[_mapSizeX, _mapSizeY];
            for(int i = 0; i<_mapSizeY; i++)
            {
                for (int j = 0; j<_mapSizeX; j++)
                {
                    cellMap[j, i] = new Cell(_cellMap[j, i].GetContent());
                }
            }
            mapSizeX = _mapSizeX;
            mapSizeY = _mapSizeY;
            mapSprite = _mapSprite;
        }

        public Map Copy()
        {
            return new Map(sizePerCell, cellMap, mapSizeX, mapSizeY, mapSprite);
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

        public int Update(float deltaTime)
        {

            return 0;
        }
        
        public void Draw(RenderTexture win, View view)
        {
            System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
            stopWatch.Start();    
            for (int i = 0; i < mapSizeY; i++)
            {
                for (int j = 0; j < mapSizeX; j++)
                {
                    mapSprite.Texture = cellMap[j, i].GetTexture(getTextureIndexFromNeighbours(new Vector2i(j, i)));
                    //mapSprite.Scale = new Vector2f(sizePerCell / mapSprite.Texture.Size.X, sizePerCell / mapSprite.Texture.Size.Y);
                    mapSprite.Position = new Vector2(j * sizePerCell, i * sizePerCell);
                    win.Draw(mapSprite);
                }
            }
            stopWatch.Stop();
            Logger.Instance.Write("time needed to draw whole map: " + stopWatch.Elapsed.TotalMilliseconds + " ms", Logger.level.Debug);
        }

        public void DrawGUI(GUI gui, float deltaTime)
        {

        }

        // Used to simplify the choice of Texture significantly...
        /// <summary>
        /// This function computes a value between 0 and 15 based on the 4 direct neighbors of the given Cell
        /// look at Assetmanager -> textures
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns>index of the correct Groundtexture(starting at the first groundtexture)</returns>
        public int getTextureIndexFromNeighbours(Vector2i position)
        {
            int result = 0;
            if (CellIsWalkable(new Vector2i(position.X-1, position.Y)))
                result += 8;
            if (CellIsWalkable(new Vector2i(position.X, position.Y-1)))
                result += 4;
            if (CellIsWalkable(new Vector2i(position.X + 1, position.Y)))
                result += 2;
            if (CellIsWalkable(new Vector2i(position.X, position.Y+1)))
                result += 1;
        
            return result;
        }
        
        public Boolean CellIsWalkable(Vector2i position)
        {
            if (!isInMap(position))
                return false;
            return cellMap[position.X, position.Y].IsWalkable();
        }

        public Boolean CellIsMovable(Vector2i position)
        {
            if(!isInMap(position))
                return false;
            return cellMap[position.X, position.Y].IsMovable();
        }

        public Boolean CellIsGoal(Vector2i position)
        {
            if (!isInMap(position))
                return false;
            return cellMap[position.X, position.Y].IsGoal();
        }

        public Boolean MoveIsPossible(Vector2i position, Vector2i move, List<Vector2i> posList)
        {
            if (CellIsMovable(position + move) && CellIsWalkable(position + move + move) && !Vec2iListContainsVector(posList, position+move+move))
            {
                return true;
            }
            return false;
        }

        public Boolean StrongMoveIsPossible(Vector2i position, Vector2i move, List<Vector2i> posList)
        {
            if (CellIsMovable(position + move) && CellIsMovable(position + move * 2) && CellIsWalkable(position + move * 3) && !Vec2iListContainsVector(posList, position + move * 3))
                return true;
            return false;
        }

        // TODO: rework this function so the position is the position of the block
        // redo all its usages afterwards!!!
        public void MoveBlock(Vector2i position, Vector2i move)
        {
            Vector2i targetBlock = position + move + move;
            Vector2i moveBlock = position + move;
            cellMap[targetBlock.X, targetBlock.Y] = cellMap[moveBlock.X, moveBlock.Y];
            cellMap[moveBlock.X, moveBlock.Y] = new Cell(cellContent.Empty);
        }

        public void RemoveAllItems()
        {
            for(int j = 0; j < mapSizeY; j++)
            {
                for(int i = 0; i < mapSizeX; i++)
                {
                    if (cellMap[i, j].IsItem())
                        cellMap[i, j] = new Cell(cellContent.Empty);
                }
            }
        }

        public int GetSizePerCell()
        {
            return sizePerCell;
        }

        public Boolean isInMap(Vector2i position)
        {
            if(position.X >= mapSizeX || position.X < 0 || position.Y >= mapSizeY || position.Y < 0)
                return false;
            return true;
        }

        public cellContent getContentOfCell(Vector2i position)
        {
            if (!isInMap(position))
                throw new Exception("Invalid position is not on Map -> has no Cellcontent");
            return cellMap[position.X, position.Y].GetContent();
        }

        public Boolean Vector2iAreEqual(Vector2i vec1, Vector2i vec2)
        {
            if (vec1.X == vec2.X && vec1.Y == vec2.Y)
                return true;
            return false;
        }

        private Boolean Vec2iListContainsVector(List<Vector2i> list, Vector2i vec)
        {
            foreach(Vector2i v in list)
            {
                if (Vector2iAreEqual(v, vec))
                    return true;
            }
            return false;
        }
    }
}
