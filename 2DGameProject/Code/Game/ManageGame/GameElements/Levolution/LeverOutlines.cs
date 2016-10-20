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
            public Connection(List<RectangleShape> rectList, Vector2f _target)
            {
                lineList = rectList;
                target = _target;
            }

            public void Draw(RenderTexture win, View view, Vector2f relViewDis, Color fillColor)
            {
                Vector2f help = new Vector2f();
                foreach(RectangleShape rec in lineList)
                {
                    rec.FillColor = fillColor;
                    help = rec.Position;
                    rec.Position += relViewDis;
                    win.Draw(rec);
                    rec.Position = help;
                }
            }

            public Vector2f target;
            List<RectangleShape> lineList;
        }

        Lever lever;
        List<Connection> connections;
        int sizePercell;
        bool prevLeverState;
        float fadeTime;
        float animFadeTime;
        Color defaultColor;
        Color fillColor;
        Color animColor;
        AnimatedSprite anim;

        float particleAnimationSecondsPerFrame = 0.06F;

        public LeverOutlines(Lever _lever, int _sizePerCell)
        {
            lever = _lever;
            sizePercell = _sizePerCell;
            connections = new List<Connection>();
            prevLeverState = lever.active;
            fadeTime = 0;
            defaultColor = new Color(180, 221, 252, 0);
            anim = new AnimatedSprite(AssetManager.GetTexture(AssetManager.TextureName.ParticlesAnimated), particleAnimationSecondsPerFrame, 13);
            anim.Origin = (Vector2)anim.spriteSize * 0.5F;
            foreach (MapManipulation mani in _lever.mapManilList)
            {
                connections.Add(new Connection(GenerateConnection(mani), new Vector2f(mani.position.X * sizePercell + sizePercell * 0.5f, mani.position.Y * sizePercell + sizePercell * 0.5f)));
            }   
            
                     
        }

        public void Update(float deltaTime)
        {
            anim.UpdateFrame(deltaTime);
            anim.Color = animColor;
            Console.WriteLine("fillColor " + anim.Color);
            if (lever.active != prevLeverState)
            {
                prevLeverState = lever.active;
                fillColor = defaultColor;
                fillColor.A = 255;
                fadeTime = 0.5f;
                animFadeTime = 2.0f;
                animColor = anim.Color;
                animColor = new Color(255, 255, 255, 0);
            }
            if (animFadeTime > 0)
            {
                animColor.A = (byte)(255 * animFadeTime / 0.5f);
                animFadeTime -= deltaTime;
            }
            else
            {
                animFadeTime = 0;
            }
            if (fadeTime > 0)
            {
                fillColor.A = (byte)(255 * fadeTime / 0.5f);
                fadeTime -= deltaTime;
            }
            else
            {
                fadeTime = 0;
            }
        }

        public void Draw(RenderTexture win, View view, Vector2f relViewDis)
        {
            foreach(Connection con in connections)
            {
                con.Draw(win, view, relViewDis, fillColor);
                anim.Position = con.target + relViewDis;
                win.Draw(anim);
                anim.Rotation += 90F;
                win.Draw(anim);
                anim.Rotation += 90F;
                win.Draw(anim);
                anim.Rotation += 90F;
                win.Draw(anim);
                anim.Rotation += 90F;

            }
        }

        public RectangleShape GenerateLine(Vector2f start, Vector2f end, int thickness)
        {
            RectangleShape result = new RectangleShape(new Vector2(Vector2.distance(start, end), thickness));
            result.Origin = new Vector2f(result.Size.X * 0.5f, result.Size.Y * 0.5f);
            result.Position = new Vector2f((start.X + end.X) * 0.5f, (start.Y + end.Y) * 0.5f);
            result.Rotation = (start.Y == end.Y) ? 0 : 90;
            result.FillColor = defaultColor;
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

            posList.Add(lever.exactPosition + new Vector2f(sizePercell * 0.5f, sizePercell * 0.5f));
            bool horizontal = (horizontalDist > verticalDist) ? true : false;
            Vector2f target = new Vector2f(mapMani.position.X * sizePercell + sizePercell * 0.5f, mapMani.position.Y * sizePercell + sizePercell * 0.5f);
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
                    posList.Add(new Vector2f(lever.position.X * sizePercell + curX * sizePercell + sizePercell * 0.5f,lever.position.Y * sizePercell + curY * sizePercell + sizePercell * 0.5f));
                }
                else
                {
                    curY = curY + (yfactor * yDif);
                    posList.Add(new Vector2f(lever.position.X * sizePercell + curX * sizePercell + sizePercell * 0.5f, lever.position.Y * sizePercell + curY * sizePercell + sizePercell * 0.5f));
                }
                horizontal = !horizontal;
            }
            for( int i = 1; i < posList.Count; i++)
            {
                result.Add(GenerateLine(posList[i-1], posList[i], 4));
            }
            return result;
        }
    }
}
