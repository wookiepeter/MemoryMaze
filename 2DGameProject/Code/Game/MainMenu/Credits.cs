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
        SuperText Musiker, Programmierer,Grafiker, Kreativ, AssetsText;
        Font fontforall, fontgamename;
        List<SuperText> namel;
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
            namel = new List<SuperText>();

            fontforall = FontLoader.Instance.LoadFont("Assets/Fonts/fixedsys.ttf");
            Programmierer = new SuperText("Programmierer \n Christian Sandkämper\n Matthis Hagen \n\n", fontforall, 0.1f);
            Programmierer.Position = new Vector2f(350, 500);
            Programmierer.Color = Color.White;
            Programmierer.Scale = new Vector2f(2, 2);


            Grafiker = new SuperText("Grafiker\n Frieder Prinz\n Jan-Cord Gerken \n\n\n", fontforall, 0.1f);
            Grafiker.Position = new Vector2f(350, 750);
            Grafiker.Color = Color.White;
            Grafiker.Scale = new Vector2f(2, 2);

            Musiker = new SuperText("Musik\n Titelsong: Jan-Cord Gerken \n\n", fontforall, 0.1f);
            Musiker.Position = new Vector2f(350, 1000);
            Musiker.Color = Color.White;
            Musiker.Scale = new Vector2f(2, 2);

            Kreativ = new SuperText("Level Design\n Christian Sandkämper \n Vanessa Wöhner \n\n", fontforall, 0.1f);
            Kreativ.Position = new Vector2f(350, 1200);
            Kreativ.Color = Color.White;
            Kreativ.Scale = new Vector2f(2, 2);

			AssetsText = new SuperText("Assets \n https://creativecommons.org/licenses/by/3.0/ \n\n", fontforall, 0.1f);
			AssetsText.Position = new Vector2f(350, 1400);
			AssetsText.Color = Color.White;
			AssetsText.Scale = new Vector2f(2, 2);


			SuperText[] namelist = { Musiker, Programmierer, Grafiker, Kreativ };
            namel = namelist.ToList();
            foreach (SuperText sup in namel)
            {
                sup.minFrequency = 2;
                sup.maxFrequency = 6;   
            }
        }

        public void LoadContent()
        {

        }

        public GameState Update(RenderWindow win, float deltaTime)
        {
            foreach (SuperText sup in namel)
            {
                sup.Update(deltaTime);
            }
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
            foreach (SuperText names in namel)
            {
                names.Draw(win, RenderStates.Default);
                names.Position = new Vector2f(names.Position.X, names.Position.Y - deltaTime*70);
           
            }
        }

        public void DrawGUI(GUI gui, float deltaTime)
        {

        }
    }
}

