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
        Font font, sexyFont;
        Stopwatch stopwatch;
        Stopwatch stopwatch1; //nach 0,5 sek kann man im Menu was anklicken
        Text exit, start, credits, loadGame, mainmenu, tutorial;
        SuperText gameName;
        String gameTitle, currentTitleString, currentlyAppendedLetters;
        float currentDeltaSum, anotherDeltaSum; 
        Text funBenni;
        Text funJohannes;
        Boolean funacitvBenni, funactivJoh;
        Sprite background;
        Color MainTitleColor;
        Color ProfileNameColor;
        Color MenuTextColor;

        bool settingNewProfile = false;
        String currentInput;

        SuperText profileOneText, profileTwoText, profileThreeText;
        List<SuperText> superTextList;
        List<Text> textlist;
        List<IntRect> rectList;

        RectangleShape debugRect = new RectangleShape();
        RectangleShape debugButtonsRect = new RectangleShape();

        ManageProfiles profiles = new ManageProfiles();

        public MainMenuState()
        {
            Console.WriteLine("MAINMENUSTATE");
            profiles = new ManageProfiles();
            profiles = profiles.loadManageProfiles();
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
            sexyFont = new Font("Assets/Fonts/pixelhole.ttf");
            rectList = new List<IntRect>();
            MainTitleColor = new Color(8, 45, 3);
            ProfileNameColor = new Color(125, 253, 108);
            MenuTextColor = new Color(114, 217, 100);
 
            // MainMenuField was too big -> had to make it smaller
            
            rectList.Add(new IntRect(250, 310, 500, 80));   //One           0
            rectList.Add(new IntRect(800, 310, 80, 80));    //OneReset      1
            rectList.Add(new IntRect(250, 410, 500, 80));   //Two           2
            rectList.Add(new IntRect(800, 410, 80, 80));    //TwoReset      3
            rectList.Add(new IntRect(250, 510, 500, 80));   //Three         4
            rectList.Add(new IntRect(800, 510, 80, 80));    //ThreeReset    5
            rectList.Add(new IntRect(600, 610, 80, 80));    //Options       6
            rectList.Add(new IntRect(700, 610, 80, 80));    //Credits       7
            rectList.Add(new IntRect(800, 610, 80, 80));    //Exit          8
            rectList.Add(new IntRect(100, 100, 60, 60));    //johfeld       9

            gameTitle = "RAMification!";
            currentTitleString = "";
            currentlyAppendedLetters = "012";
            currentDeltaSum = 0;
            
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
            gameName = new SuperText("", sexyFont, 0.1f);
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

            profileOneText = new SuperText(profiles.getProfileName(MemoryMaze.profiles.one), font, 0.15f);
            profileOneText.Position = new Vector2f(250, 300);
            profileOneText.CharacterSize = 70;
            profileTwoText = new SuperText(profiles.getProfileName(MemoryMaze.profiles.two), font, 0.15f);
            profileTwoText.Position = new Vector2f(250, 400);
            profileTwoText.CharacterSize = 70;
            profileThreeText = new SuperText(profiles.getProfileName(MemoryMaze.profiles.three), font, 0.15f);
            profileThreeText.Position = new Vector2f(250, 500);
            profileThreeText.CharacterSize = 70;
            superTextList = new List<SuperText>{ profileOneText, profileTwoText, profileThreeText};

            //Alle Texte in ein Array Speichern -> Liste übertragen!
            Text[] array = { mainmenu, start,tutorial,  loadGame, credits, exit};
            textlist = array.ToList();

        }
        public bool IsMouseInRectangle(IntRect rect, RenderWindow win)                          //Ist die Maus über ein IntRect
        {
            Vector2i mouse = win.InternalGetMousePosition();                                    // @chris -> das nehmen um die interne mausposition zu kriegen
            return (rect.Left < mouse.X && rect.Left + rect.Width > mouse.X
                        && rect.Top < mouse.Y && rect.Top + rect.Height > mouse.Y);
        }

        public GameState Update(RenderWindow win, float deltaTime)
        {
            gameName.Update(deltaTime);

            if (stopwatch1.ElapsedMilliseconds > 500)
            {
                int index = -1;
                debugRect.Position = new Vector2f(-1000, -1000);                    // moves this out of the picture... 
                for (int e = 0; e < rectList.Count; e++)
                {
                    if (IsMouseInRectangle(rectList[e], win))                           //Geht die Liste mit rectInt duch!
                    {
                        index = e;                                                  //Maus war auf einem -> der index wird gespeichert! (nummer des Rectint)
                        break;
                    }
                }
                if (settingNewProfile)
                {
                    List<char> charList = KeyboardInputManager.getCharInput();
                    foreach (char c in charList)
                        currentInput += c;
                    UpdateSelectedProfileText(currentInput);
                    if (KeyboardInputManager.Downward(Keyboard.Key.Back))
                    {
                        if (currentInput != "")
                        {
                            currentInput = currentInput.Remove(currentInput.Length - 1);
                        }
                    }
                    if ((KeyboardInputManager.IsPressed(Keyboard.Key.Escape) || Mouse.IsButtonPressed(Mouse.Button.Left)) && settingNewProfile)
                    {
                        settingNewProfile = false;
                        stopwatch.Restart();
                        UpdateActiveProfileText();
                        return GameState.MainMenu;
                    }
                    if (KeyboardInputManager.IsPressed(Keyboard.Key.Return))
                    {
                        if (currentInput != "")
                        {
                            profiles.setProfile(currentInput, ProfileConstants.activeProfile);
                            settingNewProfile = false;
                            UpdateActiveProfileText();
                        }
                        else
                        {
                            Logger.Instance.Write("ProfileName cannot be empty", Logger.level.Info);
                        }
                    }
                }
                else if (Mouse.IsButtonPressed(Mouse.Button.Left))                       //Wurde die LinkeMaustaste gedrückt?
                {
                    //Console.WriteLine("Der Index in der SwitchAnweisung: " + index);
                    switch (index)                                                  //Bin mit der Maus über den Index: SwitchCaseWeg
                    {                                                               //bearbeitet das aktuelle TextFeld
                                                                                    //start
                        case 0:
                            ProfileConstants.activeProfile = MemoryMaze.profiles.one;
                            return GameState.LoadLevelState;
                        case 1:
                            ProfileConstants.activeProfile = MemoryMaze.profiles.one;
                            settingNewProfile = true;
                            currentInput = "";
                            break;
                        case 2:
                            ProfileConstants.activeProfile = MemoryMaze.profiles.two;
                            return GameState.LoadLevelState;
                        case 3:
                            ProfileConstants.activeProfile = MemoryMaze.profiles.two;
                            settingNewProfile = true;
                            currentInput = "";
                            break;
                        case 4:
                            ProfileConstants.activeProfile = MemoryMaze.profiles.three;
                            return GameState.LoadLevelState;
                        case 5:
                            ProfileConstants.activeProfile = MemoryMaze.profiles.three;
                            settingNewProfile = true;
                            currentInput = "";
                            break;
                        case 6: return GameState.Intro;
                        case 7: return GameState.Credits;
                        case 8: return GameState.None;
                        case 9:
                            funactivJoh = true; stopwatch.Restart();
                            break;
                        default: break;
                    }
                }
                else
                {
                    if (index >= 0 && index <= 9)
                    {
                        IntRect rect = rectList[index];
                        debugRect.Size = new Vector2f(rect.Width, rect.Height);
                        debugRect.Position = new Vector2f(rectList[index].Left, rectList[index].Top);
                        debugRect.FillColor = Color.Cyan;
                    }
                }
            }
            UpdateMainTitle(deltaTime);

            foreach (SuperText s in superTextList)
                s.Update(deltaTime);
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
            // draw all rectangleshapes to see the Clickboxes
            foreach(IntRect r in rectList)
            {
                debugButtonsRect.Size = new Vector2f(r.Width, r.Height);
                debugButtonsRect.Position = new Vector2f(r.Left, r.Top);
                debugButtonsRect.FillColor = Color.Black;
                win.Draw(debugButtonsRect);
            }
            // Highlights the currently hovered Clickbox
            win.Draw(debugRect);
            gameName.Draw(win, RenderStates.Default);
            foreach (SuperText s in superTextList)
            {
                s.Draw(win, RenderStates.Default);
            }
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
        }

        private void UpdateActiveProfileText()
        {
            switch (ProfileConstants.activeProfile)
            {
                case MemoryMaze.profiles.one:
                    profileOneText.DisplayedString = profiles.getActiveProfileName();
                    break;
                case MemoryMaze.profiles.two:
                    profileTwoText.DisplayedString = profiles.getActiveProfileName();
                    break;
                case MemoryMaze.profiles.three:
                    profileThreeText.DisplayedString = profiles.getActiveProfileName();
                    break;
            }
        }

        private void UpdateSelectedProfileText(String currentInput)
        {
            switch (ProfileConstants.activeProfile)
            {
                case MemoryMaze.profiles.one:
                    profileOneText.DisplayedString = currentInput;
                    break;
                case MemoryMaze.profiles.two:
                    profileTwoText.DisplayedString = currentInput;
                    break;
                case MemoryMaze.profiles.three:
                    profileThreeText.DisplayedString = currentInput;
                    break;
            }
        }
    }
}
