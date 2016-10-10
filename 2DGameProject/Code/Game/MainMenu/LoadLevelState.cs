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
            public LevelSelectionScreen(Texture _texture, List<Vector2f> _posList, int _startIndex)
            {
                posList = _posList;
                texture = _texture;
                startIndex = _startIndex;
            }

            public Texture texture;
            public List<Vector2f> posList;
            public int startIndex;
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

        List<LevelSelectionScreen> levelSelectList;
        List<LevelSelectButton> mainButtonList;
        List<LevelSelectButton> helpButtonList;

        Vector2i currentScreenPosition;

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
            mainMap.Texture = AssetManager.GetTexture(AssetManager.TextureName.MapBackground1);
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

            mainButtonList = new List<LevelSelectButton>();
            helpButtonList = new List<LevelSelectButton>();

            levelSelectList = new List<LevelSelectionScreen>();

            // this list is used to initialize the levelscreens
            List<List<Vector2f>> posList = new List<List<Vector2f>>() {
                new List<Vector2f>() { new Vector2f(100, 200), new Vector2f(200, 200), new Vector2f( 300, 200), new Vector2f(400, 200), new Vector2f(500, 200), new Vector2f(600, 200), new Vector2f(700, 200), new Vector2f(800, 200), new Vector2f(900, 200), new Vector2f(1000, 200)},
                new List<Vector2f>() { new Vector2f(100, 200), new Vector2f(250, 200), new Vector2f(400, 200), new Vector2f(550, 200), new Vector2f(700, 200), new Vector2f(850, 200) },
                new List<Vector2f>() { new Vector2f(100, 200), new Vector2f(250, 200), new Vector2f(400, 200), new Vector2f(550, 200), new Vector2f(700, 200), new Vector2f(850, 200) },
                new List<Vector2f>() { new Vector2f(300, 200), new Vector2f(600, 200), new Vector2f(900, 200) },
                new List<Vector2f>() { new Vector2f(200, 200), new Vector2f(300, 200), new Vector2f(400, 200), new Vector2f(500, 200), new Vector2f(600, 200), new Vector2f(700, 200), new Vector2f(800, 200), new Vector2f(900, 200), new Vector2f(1000, 200) } 
            };
            List<Texture> backgroundList = new List<Texture>() { AssetManager.GetTexture(AssetManager.TextureName.MapBackground1), AssetManager.GetTexture(AssetManager.TextureName.MapBackground2), AssetManager.GetTexture(AssetManager.TextureName.MapBackground3), AssetManager.GetTexture(AssetManager.TextureName.MapBackground4), AssetManager.GetTexture(AssetManager.TextureName.MapBackground5)};
            for ( int i = 0; i < posList.Count; i++)
            {
                levelSelectList.Add(new LevelSelectionScreen(backgroundList[i], posList[i], i));
            }

            mainButtonList = new List<LevelSelectButton>();
            helpButtonList = new List<LevelSelectButton>();

            currentScreenPosition = new Vector2i(GetPositionOnCurrentLevelScreen(), 0);
            SetButtonList(mainButtonList);
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
            foreach(LevelSelectButton l in mainButtonList)
            {
                l.Update(deltaTime, win, currentLevel);
            }
            if (stopwatch.ElapsedMilliseconds > 500)
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                    return GameState.MainMenu;
                foreach(LevelSelectButton l in mainButtonList)
                {
                    l.Update(deltaTime, win, currentLevel);
                }
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
                if (KeyboardInputManager.Downward(Keyboard.Key.Left) || KeyboardInputManager.Downward(Keyboard.Key.Right))
                {
                    if (KeyboardInputManager.Downward(Keyboard.Key.Left))
                    {
                        if(currentLevel > 0)
                        {
                            currentLevel--;
                            currentScreenPosition.X -= 1;
                            if (currentScreenPosition.X < 0)
                            {
                                InitiateSlide(false);
                                SetButtonList(mainButtonList);
                                currentScreenPosition.X = GetPositionOnCurrentLevelScreen();
                            }
                        }
                    }
                    else
                    {
                        if (stars.levelIsUnlocked(currentLevel + 1))
                        {
                            currentLevel++;
                            currentScreenPosition.X = GetPositionOnCurrentLevelScreen();
                            if (GetPositionOnCurrentLevelScreen() == 0)
                            {
                                InitiateSlide(true);
                                SetButtonList(mainButtonList);
                            }
                        }
                    }
                    return GameState.LoadLevelState;
                }
                if (KeyboardInputManager.Downward(Keyboard.Key.Return) && currentScreenPosition.Y == 0)
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
            SetButtonList(mainButtonList);
            int currentScreen = GetIndexOfCurrentLevelScreen();
            if (right)
            {
                helpMap.Texture = levelSelectList[currentScreen - 1].texture;
                SetButtonList(helpButtonList, currentScreen - 1);
            }
            else
            {
                helpMap.Texture = levelSelectList[currentScreen + 1].texture;
                SetButtonList(helpButtonList, currentScreen + 1);
            }
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
            mainMap.Position = slideOffscreenStartPosition;
            helpMap.Position = slideEndPosition;
        }

        public void SlideMap(float deltaTime)
        {
            mainMap.Position = Vector2.lerp(mainMap.Position, slideEndPosition, slideSpeed * deltaTime);
            helpMap.Position = Vector2.lerp(helpMap.Position, slideOffsrceenEndPosition,  slideSpeed * deltaTime);
            if (Math.Abs(Vector2.distance( mainMap.Position, slideEndPosition)) <= 0.8f)
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
            foreach(LevelSelectButton l in mainButtonList)
            {
                l.Draw(win);
            }
            for (int i = 0; i < rectList.Count; i++)
            {
                IntRect ir = rectList[i];
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
            int currentMod = GetPositionOnCurrentLevelScreen();
            if(right)
            {
                help = help + mainButtonList.Count - currentMod;
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
                if (help >= levelSelectList[0].posList.Count - 1)
                {
                    currentLevel = help;
                    InitiateSlide(false);
                    return true;
                }
            }
            return false;
        }

        private int GetIndexOfCurrentLevelScreen()
        {
            int helpLevel = 0;
            int curIndex = 0;
            while(currentLevel > helpLevel)
            {
                helpLevel = helpLevel + levelSelectList[curIndex].posList.Count;
                if (helpLevel > currentLevel)
                    break;
                curIndex++;
            }
            return curIndex;
        }

        private int GetPositionOnCurrentLevelScreen()
        {
            int helpLevel = 0;
            int curPos = 0;
            int curIndex = 0;
            while (currentLevel > helpLevel)
            {
                helpLevel++;
                curPos++;
                if (curPos >= levelSelectList[curIndex].posList.Count)
                {
                    curIndex++;
                    curPos = 0;
                }
            }
            return curPos;
        }

        private void SetButtonList(List<LevelSelectButton> buttonList)
        {
            buttonList.RemoveAll(b => true);
            int curLevel = 0;
            int mapIndex = GetIndexOfCurrentLevelScreen();
            for (int i = 0; i < mapIndex; i++)
                curLevel += levelSelectList[i].posList.Count;
            foreach (Vector2f v in levelSelectList[mapIndex].posList)
            {
                buttonList.Add(new LevelSelectButton(v, curLevel));
                curLevel++;
            }
        }

        private void SetButtonList(List<LevelSelectButton> buttonList, int mapIndex)
        {
            if (mapIndex < 0 || mapIndex > levelSelectList.Count)
            {
                Logger.Instance.Write("Invalid mapIndex: " + mapIndex, Logger.level.Warning);
                return;
            }
            buttonList.RemoveAll(b => true);
            int curLevel = 0;
            for (int i = 0; i < mapIndex; i++)
                curLevel += levelSelectList[i].posList.Count;
            foreach (Vector2f v in levelSelectList[mapIndex].posList)
            {
                buttonList.Add(new LevelSelectButton(v, curLevel));
                curLevel++;
            }
        }
    }
}
