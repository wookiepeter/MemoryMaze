﻿using System;
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
        Text funBenni;
        Text funJohannes;
        Boolean funacitvBenni, funactivJoh;
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
            funactivJoh = false;
            funacitvBenni = false;
            font = new Font("Assets/Fonts/calibri.ttf");
            list = new List<IntRect>();
 
            list.Add(new IntRect(100, 280, 420, 100));  //MainMenu      0
            list.Add(new IntRect(250, 370, 220, 60));   //Start         1
            list.Add(new IntRect(250, 420, 220, 60));   //Steuerung     2
            list.Add(new IntRect(250, 470, 220, 60));   //credit        3
            list.Add(new IntRect(250, 520, 220, 60));   //End           4
            list.Add(new IntRect(100, 100, 60, 60));    //johfeld       5
            
            
            //Witze hahahahhahaha witzig faggot....stfu das ist witzig..ne ist es nicht...ohh mr. Ernst! :/
            funBenni = new Text("Benni heisst Online: KleinerHoden, hihi", font);
            funBenni.Position = new Vector2f(800, 250);
            funBenni.CharacterSize = 30;
            funBenni.Color = Color.Red;
            funBenni.Rotation = 45;

            funJohannes = new Text("Johannes hatte frueher Locken, true Story!", font);
            funJohannes.Position = new Vector2f(400, 650);
            funJohannes.CharacterSize = 40;
            funJohannes.Color = Color.Red;


            //Initializiere alle Texte
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


            //Alle Texte in ein Array Speichern -> Liste übertragen!
            Text[] array = { mainmenu, start, steuerung, credits, exit, gamename };
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

            int index = -1;

            for (int e = 0; e < 6; e++)
            {
                if (IsMouseInRectangle(list[e], win))                           //Geht die Liste mit rectInt duch!
                {
                    index = e;                                                  //Maus war auf einem -> der index wird gespeichert! (nummer des Rectint)
                    break;
                }
            }
            if (Mouse.IsButtonPressed(Mouse.Button.Left))                       //Wurde die LinkeMaustaste gedrückt?
            {
                Console.WriteLine(index);
                switch (index)                                                  //Bin mit der Maus über den Index: SwitchCaseWeg
                {                                                               //bearbeitet das aktuelle TextFeld
                    //start
                    case 0: funacitvBenni = true; stopwatch.Restart();
                        break;
                    case 1: return GameState.InGame;
                    //end
                    case 2: return GameState.Steuerung;
                    //credits
                    case 3: return GameState.Credits;
                    case 4: return GameState.None;
                    case 5: funactivJoh = true; stopwatch.Restart();
                        break;
                        //    case 5: break;
                        //    case 6: break;
                        //    case 7: break;
                        //    default: break;
                }
            }
            else
            {
                if (index != -1 && index != 0 && index != 5)
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
            //FunSachenAufrufen
            if (funacitvBenni && stopwatch.Elapsed.Seconds < 3)
                gui.Draw(funBenni);
            else
                funacitvBenni = false;

            if (funactivJoh && stopwatch.Elapsed.Seconds < 3)
                gui.Draw(funJohannes);
            else
                funactivJoh = false;

            //Alle Texte aus der Liste zeichnen
            foreach (Text txt in textlist)
            {
                gui.Draw(txt);
                txt.Color = Color.White;
            }
        }
    }
}
