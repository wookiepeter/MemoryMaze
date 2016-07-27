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
        RectangleShape sprite;
        public Vector2i mapPosition { get; private set; }
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
        public override void Update(float deltaTime, Map map, int controllid)
        {
            Vector2i move = GetMove();
            if (controllid == id)
            {
                if (map.CellIsWalkable(mapPosition + move))
                {
                    if (move.X != 0 || move.Y != 0) //TOdo Matthis bearbeiten
                        counter--;
                    mapPosition = mapPosition + move;
                    //Logger.Instance.Write("mapPosX: " + mapPosition.X + "mapPosY" + mapPosition.Y, Logger.level.Info);
                    UpdateSpritePosition(map);
                }
                else if (map.MoveIsPossible(mapPosition, move))
                {
                    //Logger.Instance.Write("moves Block from " + (mapPosition + move).ToString() + " to " + (mapPosition + move + move).ToString(), Logger.level.Info);
                    map.MoveBlock(mapPosition, move);
                    mapPosition = mapPosition + move;
                }
                if (counter == 0)
                    isAlive = false;
            }
        }

        public override void DrawGUI(GUI gui, float deltaTime)
        {

        }

        public override void HandleEvents()
        {

        }

        public override void Render(RenderWindow window)
        {
            window.Draw(sprite);
        }

        Vector2i GetMove()
        {
            Vector2i move = new Vector2i(0, 0);
            if (KeyboardInputManager.Downward(Keyboard.Key.Up))
            {
                move.Y = -1;
            }
            else if (KeyboardInputManager.Downward(Keyboard.Key.Down))
            {
                move.Y = 1;
            }
            else if (KeyboardInputManager.Downward(Keyboard.Key.Left))
            {
                move.X = -1;
            }
            else if (KeyboardInputManager.Downward(Keyboard.Key.Right))
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

