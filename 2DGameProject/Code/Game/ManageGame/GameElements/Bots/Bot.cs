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
        public int id { get; set; }
        public Vector2i mapPosition { get; set; }
        public Bot() { }
        public abstract void Update(float deltaTime, Map map, int controllid, List<Vector2i> botPosList);
        public abstract void HandleEvents();
        public abstract void Render(RenderTexture win, View view, Vector2f relViewDis);
        virtual public void DrawGUI(GUI gui, float deltaTime)
        {
            
        }
        public bool isAlive { get; set; }

        public RectangleShape sprite { get; protected set; }
        protected Texture texture;
    }
}
