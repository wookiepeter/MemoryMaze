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
        public Vector2i mapPosition { get; private set; }
        Vector2f size { get { return sprite.Size; } set { sprite.Size = value; } }
        public GhostPlayer(Vector2i position, Map map)
        {
            this.sprite = new RectangleShape(new Vector2f(1F, 1F));
            this.sprite.Size = new Vector2f(map.GetSizePerCell() * 0.8F, map.GetSizePerCell() * 0.8F);
            this.sprite.Texture = AssetManager.GetTexture(AssetManager.TextureName.PlayerGhost);
       
            this.mapPosition = position;
            UpdateSpritePosition(map);
        }
        public void Update(float deltaTime, Map map)
        {
            Vector2i move = GetMove();
            if (map.CellIsWalkable(mapPosition + move))
            {
                mapPosition = mapPosition + move;
                //Logger.Instance.Write("mapPosX: " + mapPosition.X + "mapPosY" + mapPosition.Y, Logger.level.Info);
                UpdateSpritePosition(map);
            }
            else if (map.CellIsMovable(mapPosition + move) && map.MoveIsPossible(mapPosition, move))
            {
                //Logger.Instance.Write("moves Block from " + (mapPosition + move).ToString() + " to " + (mapPosition + move + move).ToString(), Logger.level.Info);
                map.MoveBlock(mapPosition, move);
                mapPosition = mapPosition + move;
            }
        }

        public void Draw(RenderWindow win, View view)
        {
            view.Center = Vector2.lerp(view.Center, sprite.Position, 0.01F);
            win.Draw(sprite);
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
            Logger.Instance.Write("moveX: " + move.X + "moveY" + move.Y, Logger.level.Info);
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
