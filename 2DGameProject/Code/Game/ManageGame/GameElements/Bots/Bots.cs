using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;


namespace MemoryMaze
{
    public abstract class Bot
    {
        public int  counter;
        public Bot() { }
        public abstract void Update(float deltaTime, Map map);
        public abstract void HandleEvents();
        public abstract void Render(RenderWindow win);
        public bool isAlive { get; set; }

        protected Sprite sprite;
        protected Texture texture;
    }
}
