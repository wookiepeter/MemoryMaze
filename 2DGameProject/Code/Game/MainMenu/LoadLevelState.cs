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
        struct LevelSelectionScreen
        {
            LevelSelectionScreen(Texture _texture, List<Vector2f> _posList)
            {
                posList = _posList;
                texture = _texture;
            }

            public Texture texture;
            public List<Vector2f> posList;
        }

        Font font;
        LevelSelectButton test;

        List<IntRect> rectList;
        Stopwatch stopwatch;
        SuperText lastScreen, nextScreen;

        RectangleShape debugRect;
        RectangleShape debugButtonRect;
        RectangleShape levelButtons;            // used to draw all the positions of any buttons
        RectangleShape screenButtons;           // used to draw the buttons for switching between screens

        RectangleShape mainMap;
        RectangleShape helpMap;
        Vector2f slideOffscreenStartPosition;
        Vector2f slideEndPosition;
        Vector2f slideOffsrceenEndPosition;
        float slideSpeed;
        bool sliding;

        int currentLevel;
        ManageProfiles profiles;
        ManageStars stars;

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
            font = new Font("Assets/Fonts/pixelhole.ttf");
            rectList = new List<IntRect>();

            rectList.Add(new IntRect(45, 245, 85, 185));        //Left           0
            rectList.Add(new IntRect(600, 600, 80, 80));        //Levels         1
            rectList.Add(new IntRect(900, 600, 80, 80));        //Settings       2   
            rectList.Add(new IntRect(1145, 245, 85, 185));      //Right          3   

            for (int i = 0; i < 5; i++)
            {
                rectList.Add(new IntRect(255 + (int) (200 * i), Rand.IntValue(200, 500), 50, 50)); // 4 - 8
            }

            test = new LevelSelectButton(new Vector2f(200, 200), 10);

            debugRect = new RectangleShape();
            levelButtons = new RectangleShape();
            levelButtons.Texture = AssetManager.GetTexture(AssetManager.TextureName.LevelButton);
            screenButtons = new RectangleShape();
            screenButtons.Texture = AssetManager.GetTexture(AssetManager.TextureName.LevelButtonMedium);
            screenButtons.Size = new Vector2f(screenButtons.Texture.Size.X, screenButtons.Texture.Size.Y);
            screenButtons.Rotation = 90;
            screenButtons.FillColor = new Color(255, 255, 255, 200);
            screenButtons.Origin = new Vector2f(0, screenButtons.TextureRect.Height);
            debugButtonRect = new RectangleShape();
            mainMap = new RectangleShape(new Vector2f(1280, 720));
            mainMap.Position = new Vector2f(0, 0);
            mainMap.Texture = AssetManager.GetTexture(AssetManager.TextureName.MapBackground);
            helpMap = new RectangleShape(mainMap);
            helpMap.Position = new Vector2f(-1000, -1000);
            slideSpeed = 10;
            sliding = false;

            lastScreen = new SuperText("<", font, 1);
            lastScreen.CharacterSize = 200;
            lastScreen.Position = new Vector2f(60, 175);
            lastScreen.minFrequency = 5;
            lastScreen.maxFrequency = 10;
            nextScreen = new SuperText(">", font, 1);
            nextScreen.CharacterSize = 200;
            nextScreen.Position = new Vector2f(1160, 175);
            nextScreen.minFrequency = 5;
            nextScreen.maxFrequency = 10;

            profiles = new ManageProfiles();
            profiles = profiles.loadManageProfiles();
            stars = new ManageStars();
            stars = stars.unsafelyLoadManageStars(profiles.getActiveProfileName());
            currentLevel = stars.getIndexOfFirstUnsolvedLevel() ;
        }

        public bool IsMouseInRectangle(IntRect rect, RenderWindow win)                          //Ist die Maus über ein IntRect
        {
            Vector2i mouse = win.InternalGetMousePosition();
            return (rect.Left < mouse.X && rect.Left + rect.Width > mouse.X
                        && rect.Top < mouse.Y && rect.Top + rect.Height > mouse.Y);
        }

        public void LoadContent()
        {

        }

        public GameState Update(RenderWindow win, float deltaTime)
        {
            int index = -1;
            debugRect.Position = new Vector2f(-1000, -1000);
            lastScreen.Update(deltaTime);
            nextScreen.Update(deltaTime);
            test.Update(deltaTime, win);
            if (stopwatch.ElapsedMilliseconds > 500)
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                    return GameState.MainMenu;
                if (sliding)
                {
                    SlideMap(deltaTime);
                    return GameState.LoadLevelState;
                }
                for (int e = 0; e < rectList.Count; e++)
                {
                    if (IsMouseInRectangle(rectList[e], win))                           //Geht die Liste mit rectInt duch!
                    {
                        index = e;                                                  //Maus war auf einem -> der index wird gespeichert! (nummer des Rectint)
                        break;
                    }
                }
                if (KeyboardInputManager.Downward(Keyboard.Key.Left))
                {
                    if (KeyboardInputManager.Downward(Keyboard.Key.Left))
                    {
                        if (currentLevel > 0)
                        {
                            currentLevel--;
                            if (currentLevel % 5 == 4)
                                InitiateSlide(false);
                        }
                    }
                    else
                    {
                        if (stars.levelIsUnlocked(currentLevel + 1))
                        {
                            currentLevel++;
                            if (currentLevel % 5 == 0)
                                InitiateSlide(true);
                        }
                    }
                    return GameState.LoadLevelState;
                }
                if (KeyboardInputManager.Downward(Keyboard.Key.Return))
                {
                    return StartLevelIfUnlocked();
                }
                if (Mouse.IsButtonPressed(Mouse.Button.Left))                       //Wurde die LinkeMaustaste gedrückt?
                {
                    //Console.WriteLine("Der Index in der SwitchAnweisung: " + index);
                    switch (index)                                                  //Bin mit der Maus über den Index: SwitchCaseWeg
                    {                                                               //bearbeitet das aktuelle TextFeld
                                                                                    //
                        case 0:
                            CanSlide(false);
                            return GameState.LoadLevelState;
                        //
                        case 1:
                            return StartLevelIfUnlocked();
                        //
                        case 2:
                            return GameState.Steuerung;
                        //Choose ur saveslot
                        case 3:
                            CanSlide(true);
                            return GameState.LoadLevelState;
                        default:
                            return GameState.LoadLevelState;
                    }
                }
                else
                {
                    if (index != -1)
                    { 
                        IntRect curRect = rectList[index];
                        debugRect.Position = new Vector2f(curRect.Left, curRect.Top);
                        debugRect.Size = new Vector2f(curRect.Width, curRect.Height);
                        debugRect.FillColor = Color.Cyan;
                    }
                }
            }
            return GameState.LoadLevelState;
        }

        /// <summary>
        /// Initializes the Sliding of the map
        /// </summary>
        /// <param name="right">true -> right; false -> left</param>
        public void InitiateSlide(bool right)
        {
            sliding = true;
            slideEndPosition = mainMap.Position;
            if (right)
            {
                slideOffscreenStartPosition = new Vector2f(mainMap.Position.X - 1280, mainMap.Position.Y);
                slideOffsrceenEndPosition = new Vector2f(mainMap.Position.X + 1280, mainMap.Position.Y);
            }
            else
            {
                slideOffscreenStartPosition = new Vector2f(mainMap.Position.X + 1280, mainMap.Position.Y);
                slideOffsrceenEndPosition = new Vector2f(mainMap.Position.X - 1280, mainMap.Position.Y);
            }
            helpMap.Position = slideOffscreenStartPosition;
        }

        public void SlideMap(float deltaTime)
        {
            mainMap.Position = Vector2.lerp(mainMap.Position, slideOffsrceenEndPosition, slideSpeed * deltaTime);
            helpMap.Position = Vector2.lerp(helpMap.Position, slideEndPosition,  slideSpeed * deltaTime);
            if (Math.Abs(Vector2.distance( helpMap.Position, slideEndPosition)) <= 0.8f)
            {
                sliding = false;
                mainMap.Position = slideEndPosition;
                helpMap.Position = slideOffscreenStartPosition;
            }
        }

        public void DrawGUI(GUI gui, float deltaTime)
        {

        }

        public void Draw(RenderWindow win, View view, float deltaTime)
        {
            win.Clear(Color.Black);
            win.Draw(mainMap);
            win.Draw(helpMap);
            test.Draw(win);
            for (int i = 0; i < rectList.Count; i++)
            {
                IntRect ir = rectList[i];
                if (i > 3 && i == 4 + (currentLevel % 5))
                {
                    debugButtonRect.Position = new Vector2f(ir.Left, ir.Top);
                    debugButtonRect.Size = new Vector2f(ir.Width, ir.Height);
                    debugButtonRect.FillColor = Color.Blue;
                    win.Draw(debugButtonRect);

                }
                if (i < 4)
                {
                    if (i != 0 && i != 3)
                    {
                        debugButtonRect.Position = new Vector2f(ir.Left, ir.Top);
                        debugButtonRect.Size = new Vector2f(ir.Width, ir.Height);
                        debugButtonRect.FillColor = Color.Black;
                        win.Draw(debugButtonRect);
                    }
                    else
                    {
                        screenButtons.Position = new Vector2f(ir.Left, ir.Top);
                        win.Draw(screenButtons);
                    }
                }
                else
                {
                    levelButtons.Position = new Vector2f(ir.Left, ir.Top);
                    levelButtons.Size = new Vector2f(ir.Width, ir.Height);
                    win.Draw(levelButtons);
                }
            }
            win.Draw(debugRect);
            lastScreen.Draw(win, RenderStates.Default);
            nextScreen.Draw(win, RenderStates.Default);
        }

        private GameState StartLevelIfUnlocked()
        {
            if (stars.levelIsUnlocked(currentLevel))
            {
                Logger.Instance.Write("Level " + currentLevel + " is starting...", Logger.level.Info);
                ProfileConstants.levelToPlay = currentLevel;
                return GameState.StartGameAtLevel;
            }
            else
            {
                Logger.Instance.Write("Level " + currentLevel + " is not unlocked yet", Logger.level.Info);
                return GameState.LoadLevelState;
            }

        }

        private bool CanSlide(bool right)
        {
            int help = currentLevel;
            int currentMod = help % 5;
            if(right)
            {
                help = help + 5 - currentMod;
                if (stars.levelIsUnlocked(help))
                {
                    currentLevel = help;
                    InitiateSlide(true);
                    return true;
                }
            }
            else
            {
                help = help - currentMod - 1;
                if (help >= 4)
                {
                    currentLevel = help;
                    InitiateSlide(false);
                    return true;
                }
            }
            return false;
        }

    }
}
