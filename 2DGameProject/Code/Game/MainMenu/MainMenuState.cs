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
    class MainMenuState : IGameState
    {
        Font font;
        Stopwatch stopwatch;
        Text gamename, exit, start, credits, steuerung, mainmenu;
        Text fun;
        Boolean funacitv;
        Sprite background;

        List<Text> textlist;
        List<IntRect> list;

        public MainMenuState()
        {
            Initialisation();
            background = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.MainMenuBackground));

        }
        public void Initialisation()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            funacitv = false;
            font = new Font("Assets/Fonts/calibri.ttf");
            list = new List<IntRect>();
 
            list.Add(new IntRect(100, 280, 420, 100));  //MainMenu
            list.Add(new IntRect(250, 370, 220, 60));   //Start
            list.Add(new IntRect(250, 520, 220, 60));   //End
            list.Add(new IntRect(250, 470, 220, 60));   //credit
            list.Add(new IntRect(250, 420, 220, 60));   //Steuerung

            fun = new Text("Benni heisst Online: KleinerHoden, hihi", font);
            fun.Position = new Vector2f(800, 250);
            fun.CharacterSize = 30;
            fun.Color = Color.Red;
            fun.Rotation = 45;

            gamename = new Text("MemoryMaze!", font);
            gamename.Position = new Vector2f(100, -5);
            gamename.CharacterSize = 180;

            mainmenu = new Text("SpielMenü", font);
            mainmenu.Position = new Vector2f(425, 225);
            mainmenu.CharacterSize = 70;


            credits = new Text("Credits", font);
            credits.Position = new Vector2f(250, 450);
            credits.CharacterSize = 40;

            start = new Text("Spiel starten", font);
            start.Position = new Vector2f(250, 350);
            start.CharacterSize = 40;

            exit = new Text("Spiel beenden", font);
            exit.Position = new Vector2f(250, 500);
            exit.CharacterSize = 40;

            steuerung = new Text("Steuerung", font);
            steuerung.Position = new Vector2f(250, 400);
            steuerung.CharacterSize = 40;

            Text[] array = { mainmenu, start, exit, credits, steuerung, gamename };
            textlist = array.ToList();

        }
        public bool IsMouseInRectangle(IntRect rect, RenderWindow win)
        {
            Vector2i mouse = Mouse.GetPosition() - win.Position;
            return (rect.Left < mouse.X && rect.Left + rect.Width > mouse.X
                        && rect.Top < mouse.Y && rect.Top + rect.Height > mouse.Y);
        }

        public GameState Update(RenderWindow win, float deltaTime)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Return))
            {
                return GameState.InGame;
            }
            int index = -1;

            for (int e = 0; e < 5; e++)
            {
                if (IsMouseInRectangle(list[e], win))
                {
                    index = e;
                    break;
                }
            }
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                Console.WriteLine(index);
                switch (index)
                {
                    //start
                    case 0: funacitv = true; stopwatch.Restart();
                        break;
                    case 1: return GameState.InGame;
                    //end
                    case 2: return GameState.None;
                    //credits
                    case 3: return GameState.Credits;
                    case 4: return GameState.Steuerung;
                        //    case 5: break;
                        //    case 6: break;
                        //    case 7: break;
                        //    default: break;
                }
            }
            else
            {
                if (index != -1 && index != 0)
                {
                    textlist[index].Color = Color.Blue;
                }
            }



            return GameState.MainMenu;
        }

        public void Draw(RenderWindow win, View view, float deltaTime)
        {
            //win.Draw(background);
        }

        public void DrawGUI(GUI gui, float deltaTime)
        {
            if (funacitv && stopwatch.Elapsed.Seconds < 3)
            {
                gui.Draw(fun);

            }
            //gui.Draw(background);
            foreach (Text txt in textlist)
            {
                gui.Draw(txt);
                txt.Color = Color.White;
            }
        }
    }
}
