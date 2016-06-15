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
    public class Player
    {
        RectangleShape sprite;
        Vector2i mapPosition;
        Vector2f size { get { return sprite.Size; } set { sprite.Size = value; } }
        
        public Player(Vector2i position, Map map)
        {
            this.sprite = new RectangleShape(new Vector2f(1F, 1F));
            this.sprite.Size = new Vector2f(map.getSizePerCell() * 0.8F, map.getSizePerCell() * 0.8F);
            this.sprite.Texture = AssetManager.GetTexture(AssetManager.TextureName.Player);

            this.mapPosition = position;
            updateSpritePosition(map);
        }
        
        public void update(float deltaTime, Map map)
        {
            Vector2i move = getMove();
            if (map.cellIsWalkable(mapPosition + move))
            {
                mapPosition = mapPosition + move;
                Logger.Instance.write("mapPosX: " + mapPosition.X + "mapPosY" + mapPosition.Y, Logger.level.Info);
                updateSpritePosition(map);
            }
            else if(map.cellIsMovable(mapPosition + move) && map.moveIsPossible(mapPosition, move))
            {
                Logger.Instance.write("moves Block from " + (mapPosition + move).ToString() + " to " + (mapPosition + move + move).ToString(), Logger.level.Info);
                map.moveBlock(mapPosition, move);
                mapPosition = mapPosition + move;
            }
        }

        public void draw(RenderWindow win, View view)
        {
            view.Center = Vector2.lerp(view.Center, sprite.Position, 0.01F);
            win.Draw(sprite);
        }

        Vector2i getMove()
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
            else  if (KeyboardInputManager.Downward(Keyboard.Key.Left))
            {
                move.X = -1;
            }
            else if (KeyboardInputManager.Downward(Keyboard.Key.Right))
            {
                move.X = 1;
            }
            //Console.WriteLine("moveX: " + move.X + "moveY" + move.Y);
            Logger.Instance.write("moveX: " + move.X + "moveY" + move.Y, 2);
            return move;
        }

        Vector2i makeNextPos(Vector2i nextPos, Map map)
        {
            nextPos.X = (nextPos.X > map.mapSizeX) ? map.mapSizeX : nextPos.X;
            nextPos.Y = (nextPos.Y > map.mapSizeY) ? map.mapSizeY : nextPos.Y;

            nextPos.X = (nextPos.X < 0) ? 0 : nextPos.X;
            nextPos.Y = (nextPos.Y < 0) ? 0 : nextPos.Y;
            return nextPos;
        }

        void updateSpritePosition(Map map)
        {
            this.sprite.Position = new Vector2f(mapPosition.X * map.getSizePerCell() + map.getSizePerCell() * 0.1F, mapPosition.Y * map.getSizePerCell() + map.getSizePerCell() * 0.1F);
        }

        Vector2f getSpritePosition(Map map)
        {
            return new Vector2f(mapPosition.X * map.getSizePerCell() + map.getSizePerCell()*0.1F, mapPosition.Y * map.getSizePerCell() + map.getSizePerCell()*0.1F);
        }
    }
}
