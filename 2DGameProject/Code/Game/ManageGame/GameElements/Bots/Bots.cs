using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;


namespace MemoryMaze
{
    abstract class Bots
    {
        public Bots() { }
        public abstract void Update();
        public abstract void HandleEvents();
        public abstract void Render(RenderWindow win);
        public bool isAlive { get; set; }

        protected Sprite sprite;
        protected Texture texture;
    }
}
