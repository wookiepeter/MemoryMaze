using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

namespace MemoryMaze
{
    class LoadLevelState : IGameState
    {
        Font font;
        Text playOn, levelSelect, control, back;
        Sprite background;

        List<Text> textlist;
        List<IntRect> list;


        public LoadLevelState()
        {
            Initialisation();
            //background = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.MainMenuBackground));
        }
        void Initialisation()
        {
            font = new Font("Assets/Fonts/calibri.ttf");
            list = new List<IntRect>();

            list.Add(new IntRect(100, 280, 420, 100));  //MainMenu      0
            list.Add(new IntRect(250, 370, 220, 60));   //Start         1
            list.Add(new IntRect(250, 420, 220, 60));   //Tutorial      2
            list.Add(new IntRect(250, 470, 220, 60));   //Steuerung     3

            //Initialisieren von  Text

            playOn = new Text("Continue Game ", font);
            playOn.Position = new Vector2f(250, 400);
            playOn.CharacterSize = 70;

            levelSelect = new Text("Levels", font);
            levelSelect.Position = new Vector2f(250, 500);
            levelSelect.CharacterSize = 40;

            control = new Text("Settings", font);
            control.Position = new Vector2f(250, 600);
            control.CharacterSize = 40;

            back = new Text("Back", font);
            back.Position = new Vector2f(250, 700);
            back.CharacterSize = 40;

            Text[] array = { playOn, levelSelect, control, back};
            textlist = array.ToList();

        }
        public bool IsMouseInRectangle(IntRect rect, RenderWindow win)                          //Ist die Maus über ein IntRect
        {
            Vector2i mouse = Mouse.GetPosition() - win.Position;
            return (rect.Left < mouse.X && rect.Left + rect.Width > mouse.X
                        && rect.Top < mouse.Y && rect.Top + rect.Height > mouse.Y);
        }
        public GameState Update(RenderWindow win, float deltaTime)
        {
            throw new NotImplementedException();

        }
        public void DrawGUI(GUI gui, float deltaTime)
        {
            throw new NotImplementedException();
        }
        public void Draw(RenderWindow win, View view, float deltaTime)
        {
            throw new NotImplementedException();
        }

    }
}
