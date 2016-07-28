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
    public abstract class Item
    {
        public Vector2i position;
        Sprite sprite;
        public Boolean deleted;

        public Item() { }
        public Item(Vector2i _position) { }
        public Item(Item _item) { }

        abstract public Item Copy();
        abstract public void Update(Map map, float deltaTime);
        abstract public void Draw(RenderWindow win, View view);
    }
}
