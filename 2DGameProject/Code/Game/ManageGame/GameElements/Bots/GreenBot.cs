using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;


namespace MemoryMaze
{
    class GreenBot : Bot
    {
        Vector2f size { get { return sprite.Size; } set { sprite.Size = value; } }
        public GreenBot(Vector2i position, Map map)
        {
            //ToDO Texturen/Spritre festlegen
            id = 3;
            this.counter = 10;
            this.isAlive = true;
            this.sprite = new RectangleShape(new Vector2f(1F, 1F));
            this.sprite.Size = new Vector2f(map.GetSizePerCell() * 0.8F, map.GetSizePerCell() * 0.8F);
            this.sprite.Texture = AssetManager.GetTexture(AssetManager.TextureName.GreenBot);
            this.mapPosition = position;
            UpdateSpritePosition(map);
          

        }
        public override void Update(float deltaTime, Map map, int controllid, List<Vector2i> botPosList)
        {
            Vector2i move = GetMove();
            if (controllid == id)
            {
                if (map.CellIsWalkable(mapPosition + move))
                {
                    if (move.X != 0 || move.Y != 0) //TOdo Matthis bearbeiten WTF: what am i supposed to do
                        counter--;
                    mapPosition = mapPosition + move;
                    //Logger.Instance.Write("mapPosX: " + mapPosition.X + "mapPosY" + mapPosition.Y, Logger.level.Info);
                }
                //Schieben von Bloecken!
                //else if (map.MoveIsPossible(mapPosition, move, botPosList))
                //{
                //    //Logger.Instance.Write("moves Block from " + (mapPosition + move).ToString() + " to " + (mapPosition + move + move).ToString(), Logger.level.Info);
                //    map.MoveBlock(mapPosition, move);
                //    mapPosition = mapPosition + move;
                //    counter--;
                //}
                if (counter == 0)
                    isAlive = false;
                UpdateSpritePosition(map);
            }
        }

        public override void DrawGUI(GUI gui, float deltaTime)
        {

        }

        public override void HandleEvents()
        {

        }

        public override void Render(RenderTexture window, View view, Vector2f relViewDis)
        {
            sprite.FillColor = new Color(255, 255, 255, (byte)(127.0 + ((128.0 / 10.0) * (Double)counter)));
            sprite.Position = sprite.Position + relViewDis;
            window.Draw(sprite);
        }

        Vector2i GetMove()
        {
            Vector2i move = new Vector2i(0, 0);
            if (KeyboardInputManager.Downward(Keyboard.Key.W) || KeyboardInputManager.Downward(Keyboard.Key.Up))
            {
                move.Y = -1;
            }
            else if (KeyboardInputManager.Downward(Keyboard.Key.S) || KeyboardInputManager.Downward(Keyboard.Key.Down))
            {
                move.Y = 1;
            }
            else if (KeyboardInputManager.Downward(Keyboard.Key.A) || KeyboardInputManager.Downward(Keyboard.Key.Left))
            {
                move.X = -1;
            }
            else if (KeyboardInputManager.Downward(Keyboard.Key.D) || KeyboardInputManager.Downward(Keyboard.Key.Right))
            {
                move.X = 1;
            }
            //Logger.Instance.Write("moveX: " + move.X + "moveY" + move.Y, Logger.level.Info);
            return move;
        }

        Vector2i MakeNextPos(Vector2i nextPos, Map map)
        {
            nextPos.X = (nextPos.X > map.mapSizeX) ? map.mapSizeX : nextPos.X;
            nextPos.Y = (nextPos.Y > map.mapSizeY) ? map.mapSizeY : nextPos.Y;

            nextPos.X = (nextPos.X < 0) ? 0 : nextPos.X;
            nextPos.Y = (nextPos.Y < 0) ? 0 : nextPos.Y;
            return nextPos;
        }

        void UpdateSpritePosition(Map map)
        {
            this.sprite.Position = new Vector2f(mapPosition.X * map.GetSizePerCell() + map.GetSizePerCell() * 0.1F, mapPosition.Y * map.GetSizePerCell() + map.GetSizePerCell() * 0.1F);
        }

        Vector2f GetSpritePosition(Map map)
        {
            return new Vector2f(mapPosition.X * map.GetSizePerCell() + map.GetSizePerCell() * 0.1F, mapPosition.Y * map.GetSizePerCell() + map.GetSizePerCell() * 0.1F);
        }


    }
}

