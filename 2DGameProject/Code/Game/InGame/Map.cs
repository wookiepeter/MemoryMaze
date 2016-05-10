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
    class Map
    {
        int sizeX;
        int sizeY;
        Cell[,] cellMap;

        public Map(int mapSizeX, int mapSizeY)
        {
            sizeX = mapSizeX;
            sizeY = mapSizeY;
            cellMap = randomCellMap(sizeX, sizeY);
        }

        private Cell[,] randomCellMap(int sizeX, int sizeY)
        {
            Cell[,] newCellArray = new Cell[sizeX, sizeY];
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    newCellArray[i, j] = new Cell((cellContent)Rand.IntValue(0, 2));
                }
            }
            return newCellArray;
        }

        public void draw(RenderWindow win, View view)
        {

        }
    }
}
