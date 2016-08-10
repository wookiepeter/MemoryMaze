using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using System.Diagnostics;

namespace MemoryMaze
{
    class LoadLevelState : IGameState
    {
        Font font;
        Text playOn, levelSelect, control, back;
        Sprite background;

        List<Text> textlist;
        List<IntRect> list;
        Stopwatch stopwatch;

        public LoadLevelState()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine("LOADLEVELSTATE");
            Initialisation();
            //background = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.MainMenuBackground));
        }
        void Initialisation()
        {
            font = new Font("Assets/Fonts/calibri.ttf");
            list = new List<IntRect>();

            list.Add(new IntRect(250, 420, 220, 60));   //ContinueGame   0
            list.Add(new IntRect(250, 470, 220, 60));   //Levels         1
            list.Add(new IntRect(250, 520, 220, 60));   //Settings       2   
            list.Add(new IntRect(250, 570, 220, 60));   //Back           3   

            //Initialisieren von  Text

            playOn = new Text("Continue Game ", font);
            playOn.Position = new Vector2f(250, 400);
            playOn.CharacterSize = 40;

            levelSelect = new Text("Levels", font);
            levelSelect.Position = new Vector2f(250, 450);
            levelSelect.CharacterSize = 40;

            control = new Text("Settings", font);
            control.Position = new Vector2f(250, 500);
            control.CharacterSize = 40;

            back = new Text("Back", font);
            back.Position = new Vector2f(250, 550);
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
        public void LoadContent()
        {

        }
        public GameState Update(RenderWindow win, float deltaTime)
        {
            //if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            //    return GameState.MainMenu;
            int index = -1;
            if (stopwatch.Elapsed.Milliseconds > 500)
            {

            
                for (int e = 0; e < 4; e++)
                {   
                    if (IsMouseInRectangle(list[e], win))                           //Geht die Liste mit rectInt duch!
                    {
                        index = e;                                                  //Maus war auf einem -> der index wird gespeichert! (nummer des Rectint)
                        break;
                    }
                }
                if (Mouse.IsButtonPressed(Mouse.Button.Left))                       //Wurde die LinkeMaustaste gedrückt?
                {
                    //Console.WriteLine("Der Index in der SwitchAnweisung: " + index);
                    switch (index)                                                  //Bin mit der Maus über den Index: SwitchCaseWeg
                    {                                                               //bearbeitet das aktuelle TextFeld
                    //start
                    case 0: return GameState.InGame;
                    //Levels
                    //case 1: return GameState.Intro; //LevelsStarten
                    //Steuerung
                    case 2: return GameState.Steuerung;
                    //MainMenu
                    case 3: return GameState.MainMenu;
                   //case 4: 
                   //case 5: 
                        //case 6:

                        //    case 5: break;
                        //    case 6: break;
                        //    case 7: break;
                        //    default: break;
                    }
                }
                else
                {
                if (index != -1)
                    textlist[index].Color = Color.Blue;
                }
            }

            return GameState.LoadLevelState;
        }
        public void DrawGUI(GUI gui, float deltaTime)
        {
            foreach (Text txt in textlist)
            {
                gui.Draw(txt);
                txt.Color = Color.White;
            }
        }
        public void Draw(RenderWindow win, View view, float deltaTime)
        {

        }

    }
}
