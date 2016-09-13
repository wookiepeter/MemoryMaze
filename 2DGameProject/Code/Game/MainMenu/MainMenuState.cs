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
        Stopwatch stopwatch1; //nach 0,5 sek kann man im Menu was anklicken
        Text exit, start, credits, loadGame, mainmenu, tutorial;
        SuperText gameName;
        String gameTitle, currentTitleString, currentlyAppendedLetters;
        float currentDeltaSum, anotherDeltaSum, enoughDeltaSums, andADealtTimeCounter;
        int currentlySwitchedTitle, currentlySwitchedCharinTitle;
        Text funBenni;
        Text funJohannes;
        Boolean funacitvBenni, funactivJoh;
        Sprite background;
        Color MainTitleColor;
        Color ProfileNameColor;
        Color MenuTextColor;

        char[] PossibleChars;

        List<Text> textlist;
        List<IntRect> list;

        public MainMenuState()
        {
            Console.WriteLine("MAINMENUSTATE");
            Initialisation();
            background = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.MainMenuBackground));

        }
        public void Initialisation()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            stopwatch1 = new Stopwatch();
            stopwatch1.Start();
            funactivJoh = false;
            funacitvBenni = false;
            font = new Font("Assets/Fonts/calibri.ttf");
            list = new List<IntRect>();
            MainTitleColor = new Color(8, 45, 3);
            ProfileNameColor = new Color(125, 253, 108);
            MenuTextColor = new Color(114, 217, 100);
 
            list.Add(new IntRect(100, 280, 420, 100));  //MainMenu      0
            list.Add(new IntRect(250, 370, 220, 60));   //Start         1
            list.Add(new IntRect(250, 420, 220, 60));   //Tutorial      2
            list.Add(new IntRect(250, 470, 220, 60));   //Steuerung     3
            list.Add(new IntRect(250, 520, 220, 60));   //credit        4
            list.Add(new IntRect(250, 570, 220, 60));   //End           5
            list.Add(new IntRect(100, 100, 60, 60));    //johfeld       6

            gameTitle = "RAMification!";
            currentTitleString = "";
            currentlyAppendedLetters = "012";
            currentDeltaSum = 0;
            enoughDeltaSums = 0;
            andADealtTimeCounter = 0;
            currentlySwitchedCharinTitle = 0;
            PossibleChars = new char[] { '_', '$', '#', '%', '=', '&', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            
            // Witze hahahahhahaha witzig faggot....stfu das ist witzig..ne ist es nicht...ohh mr. Ernst! :/
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
            gameName = new SuperText("", new Font("Assets/Fonts/pixelhole.ttf"), 0.1f);
            gameName.Position = new Vector2f(60, -50);
            gameName.CharacterSize = 240;
            gameName.Color = MainTitleColor;

            mainmenu = new Text("SpielMenü", font);
            mainmenu.Position = new Vector2f(425, 225);
            mainmenu.CharacterSize = 70;


            credits = new Text("Credits", font);
            credits.Position = new Vector2f(250, 500);
            credits.CharacterSize = 40;

            tutorial = new Text("Tutorial starten", font);
            tutorial.Position = new Vector2f(250, 400);
            tutorial.CharacterSize = 40;

            start = new Text("Spiel starten", font);
            start.Position = new Vector2f(250, 350);
            start.CharacterSize = 40;

            exit = new Text("Spiel beenden", font);
            exit.Position = new Vector2f(250, 550);
            exit.CharacterSize = 40;

            loadGame = new Text("Spiel laden", font);
            loadGame.Position = new Vector2f(250, 450);
            loadGame.CharacterSize = 40;


            //Alle Texte in ein Array Speichern -> Liste übertragen!
            Text[] array = { mainmenu, start,tutorial,  loadGame, credits, exit};
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
            gameName.Update(deltaTime);
            if (stopwatch1.ElapsedMilliseconds > 500)
            {
                int index = -1;

                for (int e = 0; e < 7; e++)
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
                        case 0:
                            funacitvBenni = true; stopwatch.Restart();
                            break;
                        case 1: return GameState.ChooseSaveSlotState;
                        //end
                        case 2: return GameState.Intro;
                        //LoadLevel
                        case 3: return GameState.LoadLevelState;
                        case 4: return GameState.Credits;
                        case 5: return GameState.None;
                        case 6:
                            funactivJoh = true; stopwatch.Restart();
                            break;
                            //    case 5: break;
                            //    case 6: break;
                            //    case 7: break;
                            //    default: break;
                    }
                }
                else
                {
                    if (index != -1 && index != 0 && index != 6)
                    {
                        
                    }
                }
            }
            UpdateMainTitle(deltaTime);
            return GameState.MainMenu;
        }

        void UpdateMainTitle(float deltaTime)
        {
            currentDeltaSum += deltaTime;
            anotherDeltaSum += deltaTime;
            if ((currentDeltaSum > 0.25) && (gameTitle.Length > currentTitleString.Length))
            {
                currentDeltaSum = 0;
                currentTitleString += gameTitle[currentTitleString.Length];
            }
            if (anotherDeltaSum > 0.05)
            {
                int lettersToAppend = ((gameTitle.Length - currentTitleString.Length) < 3) ? gameTitle.Length - currentTitleString.Length : 3;
                if (currentlyAppendedLetters.Length > lettersToAppend)
                {
                    currentlyAppendedLetters = currentlyAppendedLetters.Remove(gameTitle.Length - currentTitleString.Length);
                }
                if (lettersToAppend > 0)
                {
                    anotherDeltaSum = 0;
                    int randomIndex = Rand.IntValue(0, lettersToAppend);
                    currentlyAppendedLetters = currentlyAppendedLetters.Remove(randomIndex, 1);
                    currentlyAppendedLetters = currentlyAppendedLetters.Insert(randomIndex, ((char)Rand.IntValue(32, 126)).ToString());
                }
            }
            gameName.DisplayedString = currentTitleString + currentlyAppendedLetters;
        }

        public void Draw(RenderWindow win, View view, float deltaTime)
        {
            win.Draw(background);
            gameName.Draw(win, RenderStates.Default);
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
            }
            
        }
    }
}
