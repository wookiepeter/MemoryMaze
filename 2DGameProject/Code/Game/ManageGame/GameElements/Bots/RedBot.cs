using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;


namespace MemoryMaze
{
    class RedBot : Bot
    {
       
        RectangleShape sprite;
        RectangleShape guiSprite;
        public Vector2i mapPosition { get; private set; }
        Vector2f size { get { return sprite.Size; } set { sprite.Size = value; } }

        public RedBot(Vector2i position, Map map)
        {
            //ToDO Texturen/Spritre festlegen
            id = 1;
            this.counter = 10;
            this.isAlive = true;
            this.sprite = new RectangleShape(new Vector2f(1F, 1F));
            this.sprite.Size = new Vector2f(map.GetSizePerCell() * 0.8F, map.GetSizePerCell() * 0.8F);
            this.sprite.Texture = AssetManager.GetTexture(AssetManager.TextureName.RedBot);
            this.guiSprite = new RectangleShape(new Vector2f(2F, 2F));
            this.guiSprite.Size = new Vector2f(100, 100);
            this.guiSprite.Texture = AssetManager.GetTexture(AssetManager.TextureName.RedBot);
            this.guiSprite.Position = new Vector2f(25, 25);
            this.mapPosition = position;
            UpdateSpritePosition(map);
                
        }

        public override void Update(float deltaTime, Map map, int controllid)
        {
            Vector2i move = GetMove();
            if(controllid == id)
            {
                //Schaut nach ob man gehen kann
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
                else if (map.StrongMoveIsPossible(mapPosition, move))
                {
                    map.MoveBlock(mapPosition + move, move);    // moves first Block
                    map.MoveBlock(mapPosition, move);           // moves second Block
                    mapPosition = mapPosition + move;

                }

            }
            
            if (counter == 0)
                isAlive = false;
        }

        public override void DrawGUI(GUI gui, float deltaTime)
        {
            gui.Draw(guiSprite);
        }

        public override void HandleEvents()
        {

        }

        public override void Render(RenderWindow window)
        {
          
                 window.Draw(sprite);
        }

        Vector2i GetMove()
        { //Gewünschter Move zurück

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

