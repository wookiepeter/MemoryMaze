using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using SFML.Audio;

namespace MemoryMaze
{
    class AntivirTrap
    {
        Sprite sprite = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.TrapTile));


        public Vector2i position;
        public Boolean deleted;



        public AntivirTrap() { }
        public AntivirTrap(Vector2i _position, Map map)
        {
            Console.WriteLine("SpritePosition: " +sprite.Position.ToString());
            Logger.Instance.Write("Position: "+position.ToString(), 0);
            position = _position;
            sprite.Position = new Vector2f(position.X * map.sizePerCell, position.Y * map.sizePerCell );
            sprite.Scale = new Vector2f((float)map.sizePerCell  / (float)sprite.Texture.Size.X, (float)map.sizePerCell  / (float)sprite.Texture.Size.Y);


        }
        public AntivirTrap(AntivirTrap trap)
        {
            position = trap.position;
            sprite.Position = trap.sprite.Position;

        }
        public AntivirTrap Copy()
        {
            return new AntivirTrap(this);
        }
        public void Update(Map map, float deltaTime)
        {
            //if (!map.CellIsWalkable(position))
            //    deleted = true;
        }

        public void Draw(RenderWindow win, View view)
        {

            win.Draw(sprite);
        }
    }
}
