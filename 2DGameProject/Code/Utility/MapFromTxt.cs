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
        Boolean goalWasSet;

        /// <summary>
        /// Does create a Map from a txt file. First line and Second Line should contain
        /// Integerrepresentation of the Number of Lines(starting from line 3) and the Number of chars in 1 line(in that order);
        /// </summary>
        /// <param name="filename">Name of the txt file that contains the Map. Should be in Assets/MapFiles</param>
        /// <returns>cellMap</returns>
        public Cell[,] CreateMap(String filename)
        {
            file = new System.IO.StreamReader(@filename);
            String firstLine = file.ReadLine();
            String secondLine = file.ReadLine();
            int numberOfLines = int.Parse(firstLine);
            int numberOfChars = int.Parse(secondLine);

            goalWasSet = false;

            Cell[,] cellMap = new Cell[numberOfChars, numberOfLines];
            char[] curLine;
            String lineBuffer;

            for(int i = 0; i< numberOfLines; i++)
            {
                if(file.EndOfStream)
                {
                    Logger.Instance.Write("Wrong numberOfLines[" + numberOfLines + "] given in MapFile: " + filename, 0);
                }
                lineBuffer = file.ReadLine();
                if (lineBuffer.Length < numberOfChars)
                {
                    Logger.Instance.Write("Wrong numberOfChars per Line given in MapFile: " + filename, 1);
                    lineBuffer += new String('w' , numberOfChars - lineBuffer.Length);
                }
                curLine = lineBuffer.ToCharArray();
                for(int j = 0; j<numberOfChars; j++)
                {
                    cellMap[j, i] = new Cell(GetCellContentFromChar(curLine[j]));
                }
            }

            return cellMap;
        }

        // simply returns a collContent associated with the given char;
        // default is Wall;
        private cellContent GetCellContentFromChar(char c)
        {
            switch(c)
            {
                case 'm': return cellContent.Movable;
                case 'e': return cellContent.Empty;
                case 'i': return cellContent.Item;
                case 'g': if (!goalWasSet) { goalWasSet = true; return cellContent.Goal; } else return cellContent.Empty;
                default: return cellContent.Wall;
            }
        }
    }
}
