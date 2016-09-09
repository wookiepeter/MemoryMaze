using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

namespace MemoryMaze
{
    class MapFromTxt
    {
        System.IO.StreamReader file;
        Boolean goalWasSet;

        Dictionary<String, cellContent> cellContentDic= new Dictionary<String, cellContent>();

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

        public LevelutionHandler getLevelutionHandler(String filename, Map map)
        {
            file = new System.IO.StreamReader(@filename);
            List<Lever> leverList = new List<Lever>();

            String buffer;

            while(!file.EndOfStream)
            {
                buffer = file.ReadLine();
                if (buffer.Contains("_map:"))
                    break;
            }

            String[] array;

            Vector2i leverPosition;
            List<MapManipulation> maniList;

            cellContentDic.Add("empty", cellContent.Empty);
            cellContentDic.Add("movable", cellContent.Movable);
            cellContentDic.Add("wall", cellContent.Wall);

            while(!file.EndOfStream)
            {
                buffer = file.ReadLine();
                array = buffer.Split(';');
                
                int size = array.Length;
                
                if (size < 1)
                {
                    break;
                }

                if (array[0].Contains("lever"))
                {
                    leverPosition = creatVector2i(array[0].Replace("lever", ""));
                    if (!map.isInMap(leverPosition))
                        Logger.Instance.Write("Lever is not in mapArea [mapfile: " + filename + "]", 0);
                    maniList = new List<MapManipulation>();
                    for (int i = 1; i < array.Length; i++)
                    {
                        maniList.Add(createManipulation(array[i], map));
                    }
                    foreach (MapManipulation mani in maniList)
                    {
                        if (map.isInMap(mani.position))
                        {
                            Logger.Instance.Write("Mapmanipulation is not in mapArea [mapfile: " + filename + "]", 0);
                        }
                    }
                    leverList.Add(new PornLever(leverPosition, map, maniList));
                }

                if (array[0].Contains("bluelev"))
                {
                    leverPosition = creatVector2i(array[0].Replace("bluelev", ""));
                    if (!map.isInMap(leverPosition))
                        Logger.Instance.Write("Lever is not in mapArea [mapfile: " + filename + "]", 0);
                    maniList = new List<MapManipulation>();
                    for (int i = 1; i < array.Length; i++)
                    {
                        maniList.Add(createManipulation(array[i], map));
                    }
                    foreach (MapManipulation mani in maniList)
                    {
                        if (map.isInMap(mani.position))
                        {
                            Logger.Instance.Write("Mapmanipulation is not in mapArea [mapfile: " + filename + "]", 0);
                        }
                    }
                    leverList.Add(new BlueLever(leverPosition, map, maniList));
                }
            }
            return new LevelutionHandler(leverList);
        } 

        public TransportHandler getTransFromMap(String filename, Map map)
        {
            List<Transporter> transPorterList = new List<Transporter>();
            file = new System.IO.StreamReader(@filename);

            String buffer;

            while (!file.EndOfStream)
            {
                buffer = file.ReadLine();
                if (buffer.Contains("_map:"))
                    break;
            }

            String[] array;
            Vector2i entrance;
            Vector2i exit;


            while (!file.EndOfStream)
            {
                buffer = file.ReadLine();
                array = buffer.Split(';');

                int size = array.Length;

                if (size < 1)
                {
                    break;
                }
                
                if(size > 2)
                {
                    Logger.Instance.Write("Invalid Transporter in file[" + filename + "]", Logger.level.Error);
                }

                if (array[0].Contains("transport"))
                {
                    entrance = creatVector2i(array[0].Replace("transport", ""));
                    if (!map.isInMap(entrance))
                        Logger.Instance.Write("Lever is not in mapArea [mapfile: " + filename + "]", 0);
                    
                    if(array[1].Contains("portal"))
                    {
                        exit = creatVector2i(array[1].Replace("portal", ""));
                        transPorterList.Add(new Portal(entrance, exit, map));
                    }
                    else if(array[1].Contains("teleport"))
                    {
                        exit = creatVector2i(array[1].Replace("teleport", ""));
                        transPorterList.Add(new Teleporter(entrance, exit, map));
                    }
                    else
                    {
                        Logger.Instance.Write("Invalid Transporter in file[" + filename + "]", Logger.level.Error);
                    }
                }
            }


            return new TransportHandler(transPorterList);
        }

        public int[] getRatingNumbersFromMap(String filename)
        {
            int[] result = new int[3];

            file = new System.IO.StreamReader(@filename);

            String buffer;

            while (!file.EndOfStream)
            {
                buffer = file.ReadLine();
                if (buffer.Contains("_map:"))
                    break;
            }

            String[] array;
            while (!file.EndOfStream)
            {
                buffer = file.ReadLine();
                if (buffer.Contains("rating"))
                {
                    buffer = buffer.Replace("rating", "");
                    buffer = buffer.Replace("(", "");
                    buffer = buffer.Replace(")", "");
                    array = buffer.Split(',');
                    if (array.Length != 3)
                    {
                        Logger.Instance.Write("Wrong Number of Ratings in file: " + filename, Logger.level.Error);
                        throw new Exception("InvalidNumbersInRating");
                    }
                    for (int i = 0; i <3; i++)
                    {
                        result[i] = int.Parse(array[i]);
                    }
                    return result;
                }
            }
            Logger.Instance.Write("No mapRating in file: " + filename, Logger.level.Error);
            throw new Exception("NoRatingFoundInMapfile");
        }

        // str 
        private MapManipulation createManipulation(String str, Map map)
        {
            Console.WriteLine(str);
            foreach (String name in cellContentDic.Keys)
            {
                Console.WriteLine(name);
                if (str.Contains(name))
                {
                    Vector2i pos = creatVector2i(str.Replace(name, ""));
                    return new MapManipulation(pos, cellContentDic[name], map.GetContentOfCell(pos));
                }
            }
            throw (new Exception("invalid Mapmanipulation"));
        }

        private Vector2i creatVector2i(String str)
        {
            Console.WriteLine(str);
            str = str.Replace("(", "");
            str = str.Replace(")", "");
            String[] innerBuffer = str.Split(',');
            Console.WriteLine(innerBuffer[0] + " " + innerBuffer[1]);
            return new Vector2i(int.Parse(innerBuffer[0]), int.Parse(innerBuffer[1]));
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
                case 'R': return cellContent.RedItem;
                case 'B': return cellContent.BlueItem;
                case 'G': return cellContent.GreenItem;
                case 'S': return cellContent.ScoreItem;
                case 't': return cellContent.TrapTile;
                case 'g': if (!goalWasSet) { goalWasSet = true; return cellContent.Goal; } else return cellContent.Empty;
                default: return cellContent.Wall;
            }
        }
    }
}
