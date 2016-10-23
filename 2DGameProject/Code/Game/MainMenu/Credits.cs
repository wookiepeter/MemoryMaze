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
    internal class Credits : IGameState
    {

        float time = 0;
        //Image ibackground;
        //Texture tbackground;
        //Sprite sbackground;
        Sprite blackback;
        //RectangleShape blackbox;
        Text Musiker, Programmierer,Grafiker, Kreativ;
        Font fontforall, fontgamename;
        List<Text> namel;
        Sprite background;
        public Credits()
        {
            Console.WriteLine("CREDITS");
            Initialize();
        }
        public void Initialize()
        {
            // ToDo: evtl BackGround usw bearbeiten
            //ibackground = new Image("pictures/grasstitlescreen.png");
            //tbackground = new Texture(ibackground);
            //sbackground = new Sprite(tbackground);
            blackback = new Sprite(new Texture(new Image(1280, 720, new Color(0, 0, 0, 200))));
            background = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.MapBackground7));

            //
            //fontgamename = new Font("calibri.ttf");
            //gamename = new Text("Sch-Weiss", fontgamename);
            //gamename.Position = new Vector2f(220, -5);
            //gamename.CharacterSize = 240;
            //gamename.Color = new Color(255, 255, 255, 64);
            namel = new List<Text>();

            fontforall = new Font("Assets/Fonts/fixedsys.ttf");
            Programmierer = new Text("Programmierer \n Christian Sandkämper\n Matthis Hagen \n\n", fontforall);
            Programmierer.Position = new Vector2f(350, 500);
            Programmierer.Color = Color.White;
            Programmierer.Scale = new Vector2f(2, 2);


            Grafiker = new Text("Grafiker\n Frieder Prinz\n Jan-Cord Gerken \n\n\n", fontforall);
            Grafiker.Position = new Vector2f(350, 750);
            Grafiker.Color = Color.White;
            Grafiker.Scale = new Vector2f(2, 2);

            Musiker = new Text("Musik\n Titelsong: Jan-Cord Gerken \n\n", fontforall);
            Musiker.Position = new Vector2f(350, 1000);
            Musiker.Color = Color.White;
            Musiker.Scale = new Vector2f(2, 2);

            Kreativ = new Text("Level Design\n Christian Sandkämper \n Vanessa Wöhner \n\n", fontforall);
            Kreativ.Position = new Vector2f(350, 1200);
            Kreativ.Color = Color.White;
            Kreativ.Scale = new Vector2f(2, 2);

            Text[] namelist = { Musiker, Programmierer, Grafiker, Kreativ };
            namel = namelist.ToList();
        }

        public void LoadContent()
        {

        }


        public GameState Update(RenderWindow win, float deltaTime)
        {

            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                return GameState.MainMenu;
            else
                return GameState.Credits;
        }

        public void Draw(RenderWindow win, View view, float deltaTime)
        {
            //win.Clear();
            //win.Draw(sbackground);
            win.Draw(blackback);
            win.Draw(background);
            //win.Draw(gamename);
            foreach (Text names in namel)
            {
                win.Draw(names);
                names.Position = new Vector2f(names.Position.X, names.Position.Y - deltaTime*70);
           
            }
        }

        public void DrawGUI(GUI gui, float deltaTime)
        {

        }
    }
}

