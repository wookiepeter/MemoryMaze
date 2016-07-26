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
    public class GhostPlayer
    {
        
        RectangleShape sprite;
        RedBot redBot;
        BlueBot blueBot;
        GreenBot greenBot;
        bool iserstellt;
        public int counter { get; private set; }
        public Vector2i mapPosition { get; private set; }
        Vector2f size { get { return sprite.Size; } set { sprite.Size = value; } }
        public GhostPlayer(Vector2i position, Map map)
        {
            counter = 10;
            iserstellt = false;
            this.sprite = new RectangleShape(new Vector2f(1F, 1F));
            this.sprite.Size = new Vector2f(map.GetSizePerCell() * 0.8F, map.GetSizePerCell() * 0.8F);
            this.sprite.Texture = AssetManager.GetTexture(AssetManager.TextureName.PlayerGhost);
       
            this.mapPosition = position;
            UpdateSpritePosition(map);
        }
        public void Update(float deltaTime, Map map, List<Bot> botsList)
        {
            Vector2i move = GetMove();
            if (map.CellIsWalkable(mapPosition + move))
            {
                if(move.X != 0 || move.Y != 0) //TOdo Matthis bearbeiten
                    counter--;
                mapPosition = mapPosition + move;
                //Logger.Instance.Write("mapPosX: " + mapPosition.X + "mapPosY" + mapPosition.Y, Logger.level.Info);
                UpdateSpritePosition(map);
            }

            if (KeyboardInputManager.IsPressed(Keyboard.Key.Num1) && (!iserstellt)) //ToDo: Bedingungen zum erstelelen 
            {
                redBot = new RedBot(mapPosition, map);
                botsList.Add(redBot);
                counter = 0;
            }
            if (KeyboardInputManager.IsPressed(Keyboard.Key.Num2) && (!iserstellt)) //ToDo: Bedingungen zum erstelelen
            {
                blueBot = new BlueBot(mapPosition, map);
                botsList.Add(blueBot);
                counter = 0;
            }
            if (KeyboardInputManager.IsPressed(Keyboard.Key.Num3) && (!iserstellt)) //ToDo: Bedingungen zum erstelelen
            {
                greenBot = new GreenBot(mapPosition, map);
                botsList.Add(greenBot);
                counter = 0;
            }

        }
    
        public void Draw(RenderWindow win, View view)
        {
            view.Center = Vector2.lerp(view.Center, sprite.Position, 0.01F);
            if(!iserstellt)
                win.Draw(sprite);
            else
            {
                redBot.Render(win);
            }
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
        public int GetCount() { return counter; }
    }
}
