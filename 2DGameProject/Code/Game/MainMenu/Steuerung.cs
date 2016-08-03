using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

namespace MemoryMaze 
{
    class Steuerung : IGameState
    {
        Font font;
        Text text;
        public Steuerung()
        {
            font = new Font("Assets/Fonts/calibri.ttf");
            text = new Text("Hier kommen eine schoene Erklärung von unserem Grafiker Frieder \n\n\n @Frieder, sieh es als Todo Liste hier ^.^", font);
            text.Position = new Vector2f(200, 200);

        }
        public GameState Update(RenderWindow win, float deltaTime)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                return GameState.MainMenu;
            else
                return GameState.Steuerung;
        }

        public void Draw(RenderWindow win, View view, float deltaTime)
        {
            win.Draw(text);
        }

        public void DrawGUI(GUI gui, float deltaTime)
        {

        }


    }
}
