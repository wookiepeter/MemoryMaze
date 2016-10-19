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
        Font font, sexyFont;
        Stopwatch stopwatch;
        Stopwatch stopwatch1; //nach 0,5 sek kann man im Menu was anklicken
        Text exit, start, credits, loadGame, mainmenu, tutorial;
        SuperText gameName;
        String gameTitle, currentTitleString, currentlyAppendedLetters;
        float currentDeltaSum, anotherDeltaSum; 
        Text funBenni;

        Boolean funacitvBenni, funactivJoh;
        Sprite background;
        Sprite shinyEffectBarSprite;
        Color MainTitleColor;
        Color ProfileNameColor;
        Color MenuTextColor;

        bool settingNewProfile = false;
        String currentInput;

        SuperText profileOneText, profileTwoText, profileThreeText;
        List<SuperText> superTextList;
        List<Text> textlist;
        
        RectangleShape debugRect = new RectangleShape();
        RectangleShape debugButtonsRect = new RectangleShape();
        List<Button> buttonList;

        ManageProfiles profiles = new ManageProfiles();

        AnimatedSprite testSpriteForCord;

        Vector2i currentScreenPosition;

        public MainMenuState()
        {
            Console.WriteLine("MAINMENUSTATE");
            profiles = new ManageProfiles();
            profiles = profiles.loadManageProfiles();
            Initialisation();
            background = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.MainMenuBackground));
            shinyEffectBarSprite = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.ShinyEffectBar));
            shinyEffectBarSprite.Position = new Vector2(0, -3.9F * shinyEffectBarSprite.TextureRect.Height);

            testSpriteForCord = new AnimatedSprite(AssetManager.GetTexture(AssetManager.TextureName.KeyAnimated), 0.1F, 8);
            testSpriteForCord.Position = new Vector2f(20, 100);
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
            MainTitleColor = new Color(0, 2, 42);
            ProfileNameColor = new Color(125, 253, 108);
            MenuTextColor = new Color(114, 217, 100);

            buttonList = new List<Button>();
            buttonList.Add(new Button(new Vector2f(550, 330), new Vector2i(0, 0), AssetManager.GetTexture(AssetManager.TextureName.ProfileButton), AssetManager.GetTexture(AssetManager.TextureName.ProfileButtonGlow)));
            buttonList.Add(new Button(new Vector2f(550, 460), new Vector2i(0, 1), AssetManager.GetTexture(AssetManager.TextureName.ProfileButton), AssetManager.GetTexture(AssetManager.TextureName.ProfileButtonGlow)));
            buttonList.Add(new Button(new Vector2f(550, 590), new Vector2i(0, 2), AssetManager.GetTexture(AssetManager.TextureName.ProfileButton), AssetManager.GetTexture(AssetManager.TextureName.ProfileButtonGlow)));
            buttonList.Add(new Button(new Vector2f(1060, 330), new Vector2i(1, 0), AssetManager.GetTexture(AssetManager.TextureName.ProfileDeleteButton), AssetManager.GetTexture(AssetManager.TextureName.ProfileDeleteButtonGlow)));
            buttonList.Add(new Button(new Vector2f(1060, 460), new Vector2i(1, 1), AssetManager.GetTexture(AssetManager.TextureName.ProfileDeleteButton), AssetManager.GetTexture(AssetManager.TextureName.ProfileDeleteButtonGlow)));
            buttonList.Add(new Button(new Vector2f(1060, 590), new Vector2i(1, 2), AssetManager.GetTexture(AssetManager.TextureName.ProfileDeleteButton), AssetManager.GetTexture(AssetManager.TextureName.ProfileDeleteButtonGlow)));
            buttonList.Add(new Button(new Vector2f(600, 700), new Vector2i(0, 3), AssetManager.GetTexture(AssetManager.TextureName.LevelButtonOptions), AssetManager.GetTexture(AssetManager.TextureName.LevelButtonOptionsGlow)));
            buttonList.Add(new Button(new Vector2f(800, 700), new Vector2i(1, 3), AssetManager.GetTexture(AssetManager.TextureName.LevelButtonOptions), AssetManager.GetTexture(AssetManager.TextureName.LevelButtonOptionsGlow)));
            buttonList.Add(new Button(new Vector2f(1000, 700), new Vector2i(2, 3), AssetManager.GetTexture(AssetManager.TextureName.LevelButtonOptions), AssetManager.GetTexture(AssetManager.TextureName.LevelButtonOptionsGlow)));

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

            //Initializiere alle Texte
            gameName = new SuperText("", sexyFont, 0.1f);
            gameName.Position = new Vector2f(55, -45);            gameName.CharacterSize = 240;
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

            currentScreenPosition = new Vector2i(0, 0);

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
            foreach(Button b in buttonList)
            {
                b.Update(deltaTime, win, currentScreenPosition);
            }
            if (stopwatch1.ElapsedMilliseconds > 200)
            {
                int index = -1;
                debugRect.Position = new Vector2f(-1000, -1000);                    // moves this out of the picture... 
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
                else if (KeyboardInputManager.Downward(Keyboard.Key.Up) || KeyboardInputManager.Downward(Keyboard.Key.Down) || KeyboardInputManager.Downward(Keyboard.Key.Right) || KeyboardInputManager.Downward(Keyboard.Key.Left))
                {
                    if( KeyboardInputManager.Downward(Keyboard.Key.Up) && currentScreenPosition.Y > 0)
                    {
                        currentScreenPosition.Y -= 1;
                        if (currentScreenPosition.X == 2)
                            currentScreenPosition.X = 1;
                        else if (currentScreenPosition.X == 1 && currentScreenPosition.Y == 2)
                            currentScreenPosition.X = 0;
                    }
                    if ( KeyboardInputManager.Downward(Keyboard.Key.Down) && currentScreenPosition.Y < 3)
                    {
                        currentScreenPosition.Y += 1;
                        if (currentScreenPosition.Y == 3 && currentScreenPosition.X == 1)
                            currentScreenPosition.X = 2;
                    }
                    if ( KeyboardInputManager.Downward(Keyboard.Key.Right) && currentScreenPosition.X < 1)
                    {
                        currentScreenPosition.X += 1;
                    }
                    else if (KeyboardInputManager.Downward(Keyboard.Key.Right) && currentScreenPosition.X < 3 && currentScreenPosition.Y == 3)
                    {
                        currentScreenPosition.X = 2;
                    }
                    if ( KeyboardInputManager.Downward(Keyboard.Key.Left) && currentScreenPosition.X > 0)
                    {
                        currentScreenPosition.X -= 1;
                    }
                }
                else if (KeyboardInputManager.Downward(Keyboard.Key.Return))                       //Wurde die LinkeMaustaste gedrückt?
                {
                    //Console.WriteLine("Der Index in der SwitchAnweisung: " + index);
                    switch (IndexFromScreenPos())                                                  //Bin mit der Maus über den Index: SwitchCaseWeg
                    {                                                               //bearbeitet das aktuelle TextFeld
                                                                                    //start
                        case 0:
                            ProfileConstants.activeProfile = MemoryMaze.profiles.one;
                            return GameState.LoadLevelState;
                        case 3:
                            ProfileConstants.activeProfile = MemoryMaze.profiles.one;
                            settingNewProfile = true;
                            currentInput = "";
                            break;
                        case 1:
                            ProfileConstants.activeProfile = MemoryMaze.profiles.two;
                            return GameState.LoadLevelState;
                        case 4:
                            ProfileConstants.activeProfile = MemoryMaze.profiles.two;
                            settingNewProfile = true;
                            currentInput = "";
                            break;
                        case 2:
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
            }
            UpdateMainTitle(deltaTime);

            foreach (SuperText s in superTextList)
                s.Update(deltaTime);
            return GameState.MainMenu;
        }

        int IndexFromScreenPos()
        {
            int index = 0;
            while (index < buttonList.Count)
            {
                if (currentScreenPosition.Equals(buttonList[index].buttonPosition))
                    break;
                index++;
            }
            return index;
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
            // draw background
            win.Draw(background);

            // draw background-Effect
            shinyEffectBarSprite.Position = new Vector2(shinyEffectBarSprite.Position.X, ((shinyEffectBarSprite.Position.Y + deltaTime * 400)));
            if(shinyEffectBarSprite.Position.Y > win.Size.Y)
            {
                shinyEffectBarSprite.Position = new Vector2(0, -Rand.Value(1, 5) * shinyEffectBarSprite.TextureRect.Height);
            }
            win.Draw(shinyEffectBarSprite);

            // Highlights the currently hovered Clickbox
            win.Draw(debugRect);

            // Make fancy text fancy
            // Shadow
            gameName.Draw(win, RenderStates.Default);
            gameName.Position = (Vector2)gameName.Position + Vector2.One * 5;
            gameName.Draw(win, RenderStates.Default);
            gameName.Position = (Vector2)gameName.Position + Vector2.One * 4;
            gameName.Draw(win, RenderStates.Default);
            gameName.Position = (Vector2)gameName.Position + Vector2.One * 2;
            gameName.Draw(win, RenderStates.Default);
            gameName.Position = (Vector2)gameName.Position - Vector2.One * 1;
            gameName.Color = Color.White;
            // Actual Text
            gameName.Draw(win, RenderStates.Default);
            gameName.Position = (Vector2)gameName.Position - Vector2.One * 10;
            gameName.Color = MainTitleColor;

            foreach (Button b in buttonList)
            {
                b.Draw(win);
            }

            foreach (SuperText s in superTextList)
            {
                s.Draw(win, RenderStates.Default);
            }

            testSpriteForCord.UpdateFrame(deltaTime);
            //win.Draw(testSpriteForCord);
        }

        public void DrawGUI(GUI gui, float deltaTime)
        {
            //FunSachenAufrufen
            if (funacitvBenni && stopwatch.Elapsed.Seconds < 3)
                gui.Draw(funBenni);
            else
                funacitvBenni = false;

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
