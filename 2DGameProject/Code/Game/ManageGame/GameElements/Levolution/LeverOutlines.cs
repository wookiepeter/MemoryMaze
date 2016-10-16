using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML;
using SFML.Window;
using SFML.Graphics;

namespace MemoryMaze
{
    class LeverOutlines
    {
        struct Connection
        {
            public Connection(List<RectangleShape> rectList)
            {
                lineList = rectList;
            }

            public void Draw(RenderTexture win, View view, Vector2f relViewDis)
            {
                foreach(RectangleShape rec in lineList)
                {
                    win.Draw(rec);
                }
            }

            List<RectangleShape> lineList;
        }

        Lever lever;
        List<Connection> connections;
        int sizePercell;

        public LeverOutlines(Lever _lever, int _sizePerCell)
        {
            lever = _lever;
            sizePercell = _sizePerCell;
            connections = new List<Connection>();
            foreach(MapManipulation mani in _lever.mapManilList)
            {
                connections.Add(new Connection(GenerateConnection(mani)));
            }            
        }

        public void Draw(RenderTexture win, View view, Vector2f relViewDis)
        {
            foreach(Connection con in connections)
            {
                con.Draw(win, view, relViewDis);
            }
        }

        public RectangleShape GenerateLine(Vector2f start, Vector2f end, int thickness)
        {
            RectangleShape result = new RectangleShape(new Vector2(Vector2.distance(start, end), thickness));
            result.Origin = new Vector2f(result.Size.X * 0.5f, result.Size.Y * 0.5f);
            result.Position = new Vector2f(start.X + end.X * 0.5f, start.Y + end.Y * 0.5f);
            result.Rotation = (start.Y == end.Y) ? 0 : 90;
            result.FillColor = Color.Red;
            return result;
        }

        public List<RectangleShape> GenerateConnection(MapManipulation mapMani)
        {
            List<RectangleShape> result = new List<RectangleShape>();
            List<Vector2f> posList = new List<Vector2f>();
            int horizontalDist = Math.Abs(mapMani.position.X - lever.position.X);
            int verticalDist = Math.Abs(mapMani.position.Y - lever.position.Y);
            int curX = 0;
            int curY = 0;
            int xfactor = (mapMani.position.X - lever.position.X > 0) ? 1 : -1;
            int yfactor = (mapMani.position.Y - lever.position.Y > 0) ? 1 : -1;

            posList.Add(lever.exactPosition);
            bool horizontal = (Rand.IntValue(0, 2) == 0) ? true : false;
            Vector2f target = new Vector2f(mapMani.position.X * sizePercell, mapMani.position.Y * sizePercell);
            while (!posList[posList.Count - 1].Equals(target))
            {
                Console.WriteLine("posList" + posList[posList.Count - 1]);
                Console.WriteLine("target" + target);

                int randXDif = (horizontalDist != 0)?Rand.IntValue(1, horizontalDist): 0;
                int randYDif = (verticalDist != 0)?Rand.IntValue(1, verticalDist) : 0;
                int xDif = (randXDif < (horizontalDist - Math.Abs(curX))) ? randXDif : (horizontalDist - Math.Abs(curX));
                int yDif = (randYDif < (verticalDist - Math.Abs(curY))) ? randYDif : (verticalDist - Math.Abs(curY));
                if (horizontal)
                {
                    curX = curX + (xfactor*xDif);
                    posList.Add(new Vector2f(lever.position.X * sizePercell + curX * sizePercell,lever.position.Y * sizePercell + curY * sizePercell));
                }
                else
                {
                    curY = curY + (yfactor * yDif);
                    posList.Add(new Vector2f(lever.position.X * sizePercell + curX * sizePercell, lever.position.Y * sizePercell + curY * sizePercell));
                }
                horizontal = !horizontal;
            }
            for( int i = 1; i < posList.Count; i++)
            {
                result.Add(GenerateLine(posList[i-1], posList[i], 2));
            }
            return result;
        }
    }
}
