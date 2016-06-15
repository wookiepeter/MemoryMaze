using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryMaze
{
    class MapFromTxt
    {
        System.IO.StreamReader file;

        /// <summary>
        /// Does create a Map from a txt file. First line and Second Line should contain
        /// Integerrepresentation of the Number of Lines(starting from line 3) and the Number of chars in 1 line(in that order);
        /// </summary>
        /// <param name="filename">Name of the txt file that contains the Map. Should be in Assets/MapFiles</param>
        /// <returns>cellMap</returns>
        public Cell[,] createMap(String filename)
        {
            file = new System.IO.StreamReader(@filename);
            String firstLine = file.ReadLine();
            String secondLine = file.ReadLine();
            int numberOfLines = int.Parse(firstLine);
            int numberOfChars = int.Parse(secondLine);

            Cell[,] cellMap = new Cell[numberOfChars, numberOfLines];
            char[,] charMap = new char[numberOfChars, numberOfLines];
            char[] curLine;
            String lineBuffer;

            for(int i = 0; i< numberOfLines; i++)
            {
                if(file.EndOfStream)
                {
                    Logger.Instance.write("Wrong numberOfLines[" + numberOfLines + "] given in MapFile: " + filename, 0);
                }
                lineBuffer = file.ReadLine();
                if (lineBuffer.Length < numberOfChars)
                {
                    Logger.Instance.write("Wrong numberOfChars per Line given in MapFile: " + filename, 1);
                    lineBuffer += new String('w' , numberOfChars - lineBuffer.Length);
                }
                curLine = lineBuffer.ToCharArray();
                for(int j = 0; j<numberOfChars; j++)
                {
                    cellMap[j, i] = new Cell(getCellContentFromChar(curLine[j]));
                }
            }

            
            return cellMap;
        }

        // simply returns a collContent associated with the given char;
        // default is Wall;
        private cellContent getCellContentFromChar(char c)
        {
            switch(c)
            {
                case 'm': return cellContent.Movable;
                case 'e': return cellContent.Empty;
                case 'i': return cellContent.Item;
                default: return cellContent.Wall;
            }
        }
    }
}
