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
        Text Musiker, Programmierer,Grafiker;
        Font fontforall, fontgamename;
        List<Text> namel;
        public Credits()
        {
            Initialize();
        }
        public void Initialize()
        {
            // ToDo: evtl BackGround usw bearbeiten
            //ibackground = new Image("pictures/grasstitlescreen.png");
            //tbackground = new Texture(ibackground);
            //sbackground = new Sprite(tbackground);
            blackback = new Sprite(new Texture(new Image(1280, 720, new Color(0, 0, 0, 200))));
            //
            //fontgamename = new Font("calibri.ttf");
            //gamename = new Text("Sch-Weiss", fontgamename);
            //gamename.Position = new Vector2f(220, -5);
            //gamename.CharacterSize = 240;
            //gamename.Color = new Color(255, 255, 255, 64);
            namel = new List<Text>();

            fontforall = new Font("Assets/Fonts/calibri.ttf");
            Programmierer = new Text("Programmierer \n Christian Sandkämper\n Matthis Hagen \n\n", fontforall);
            Programmierer.Position = new Vector2f(450, 500);
            Programmierer.Color = Color.White;
            Programmierer.Scale = new Vector2f(2, 2);


            Grafiker = new Text("Grafiker \n Frieda Prinz \n\n", fontforall);
            Grafiker.Position = new Vector2f(450, 750);
            Grafiker.Color = Color.White;
            Grafiker.Scale = new Vector2f(2, 2);

            Musiker = new Text("Musik\n Benjamin Blüh...ähm Parske \n\n", fontforall);
            Musiker.Position = new Vector2f(450, 950);
            Musiker.Color = Color.White;
            Musiker.Scale = new Vector2f(2, 2);


            Text[] namelist = { Musiker, Programmierer, Grafiker };
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

