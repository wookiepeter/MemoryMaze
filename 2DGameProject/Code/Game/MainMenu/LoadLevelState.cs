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
        SuperText lastScreen, nextScreen, worldName;

        RectangleShape debugRect;
        RectangleShape debugButtonRect;
        RectangleShape screenButtons;           // used to draw the buttons for switching between screens

        RectangleShape mainMap;
        RectangleShape helpMap;
        Vector2f slideOffscreenStartPosition;
        Vector2f slideEndPosition;
        Vector2f slideOffsrceenEndPosition;
        Vector2f worldNameRealPos, worldNameFakePos, worldNameRealPosTarget, worldNameFakeposTarget;
        float slideSpeed;
        bool sliding;

        int currentLevel;
        ManageProfiles profiles;
        ManageStars stars;

        List<LevelSelectionScreen> levelSelectList;
        List<LevelSelectButton> mainButtonList;
        List<Vector2f> mainButtonTargetList;
        List<LevelSelectButton> helpButtonList;
        List<Vector2f> helpButtonTargetList;
        Button leftButton, rightButton;
        LevelInfo levelInfo;

        Vector2i currentScreenPosition;
        Text mousePos;

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

            screenButtons = new RectangleShape();
            screenButtons.Texture = AssetManager.GetTexture(AssetManager.TextureName.LevelButtonOptions);
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
            slideSpeed = 10f;
            sliding = false;

            lastScreen = new SuperText("<", font, 1);
            lastScreen.CharacterSize = 200;
            lastScreen.Position = new Vector2f(275, 445);
            lastScreen.minFrequency = 5;
            lastScreen.maxFrequency = 10;
            nextScreen = new SuperText(">", font, 1);
            nextScreen.CharacterSize = 200;
            nextScreen.Position = new Vector2f(865, 445);
            nextScreen.minFrequency = 5;
            nextScreen.maxFrequency = 10;

            worldName = new SuperText("1", font, 0.2f);
            worldName.CharacterSize = 145;
            worldName.Position = new Vector2f(440, 500);
            worldNameFakePos = new Vector2f(-1000, -1000);
            worldNameRealPos = worldName.Position;

            profiles = new ManageProfiles();
            profiles = profiles.loadManageProfiles();
            stars = new ManageStars();
            stars = stars.unsafelyLoadManageStars(profiles.getActiveProfileName());
            currentLevel = stars.EverythingUnlocked()? stars.GetLastSelectedLevel() : stars.getIndexOfFirstUnsolvedLevel();

            mainButtonList = new List<LevelSelectButton>();
            mainButtonTargetList = new List<Vector2f>();
            helpButtonList = new List<LevelSelectButton>();
            helpButtonTargetList = new List<Vector2f>();

            levelSelectList = new List<LevelSelectionScreen>();

            // this list is used to initialize the levelscreens
            List<List<Vector2f>> posList = new List<List<Vector2f>>() {
                new List<Vector2f>() { new Vector2f(115, 380), new Vector2f(205, 385), new Vector2f(325, 355), new Vector2f(445, 305), new Vector2f(505, 375),
                    new Vector2f(675, 345), new Vector2f(785, 275), new Vector2f(865, 245), new Vector2f(965, 205), new Vector2f(1045, 225),new Vector2f(1105,345), new Vector2f( 1115, 515)},

                new List<Vector2f>() { new Vector2f(225,395), new Vector2f(435,315), new Vector2f(535,325), new Vector2f(555,395), new Vector2f(715,385), new Vector2f(735,495),
                    new Vector2f(875,435), new Vector2f(935,245), new Vector2f(955,175), new Vector2f(1055,195), new Vector2f(1175,215), new Vector2f(1235,345)},

                new List<Vector2f>() { new Vector2f(355, 455), new Vector2f(515, 425), new Vector2f(695, 295), new Vector2f(825, 285), new Vector2f(935, 245), new Vector2f(1035, 445),
                    new Vector2f(1155, 405), },//new Vector2f(1095, 535), new Vector2f(1145, 425) },

                new List<Vector2f>() { new Vector2f(75,425), new Vector2f(155,545), new Vector2f(245,455), new Vector2f(345,355), new Vector2f(485,265), new Vector2f(615,245),
                    new Vector2f(685,255), new Vector2f(715,305), new Vector2f(775,375), new Vector2f(835,415), new Vector2f(995,425), new Vector2f(1075,465), new Vector2f(1155,475), new Vector2f(1215,405) } 
            };
            List<Texture> backgroundList = new List<Texture>() { AssetManager.GetTexture(AssetManager.TextureName.MapBackground1), AssetManager.GetTexture(AssetManager.TextureName.MapBackground2), AssetManager.GetTexture(AssetManager.TextureName.MapBackground3), AssetManager.GetTexture(AssetManager.TextureName.MapBackground4)};
            int curStartIndex = 0;
            for ( int i = 0; i < posList.Count; i++)
            {
                levelSelectList.Add(new LevelSelectionScreen(backgroundList[i], posList[i], curStartIndex));
                curStartIndex += posList[i].Count ;
            }

            mainButtonList = new List<LevelSelectButton>();
            helpButtonList = new List<LevelSelectButton>();

            leftButton = new Button(new Vector2f(300, 600), new Vector2i(0, 1), AssetManager.GetTexture(AssetManager.TextureName.LevelButtonOptions), AssetManager.GetTexture(AssetManager.TextureName.LevelButtonOptionsGlow), 90);
            rightButton = new Button(new Vector2f(900, 600), new Vector2i(1, 1), AssetManager.GetTexture(AssetManager.TextureName.LevelButtonOptions), AssetManager.GetTexture(AssetManager.TextureName.LevelButtonOptionsGlow), 90);

            currentScreenPosition = new Vector2i(GetPositionOnCurrentLevelScreen(), 0);
            worldName.DisplayedString = "World " + GetIndexOfCurrentLevelScreen();
            mainMap.Texture = levelSelectList[GetIndexOfCurrentLevelScreen()].texture;
            SetButtonList(mainButtonList);
            levelInfo = new LevelInfo(mainButtonList[GetPositionOnCurrentLevelScreen()], new Vector2f(25, 25),  stars.GetScoreOfLevel(currentLevel));
            SetCurrentLevelInfo();
            mousePos = new Text("", new Font("Assets/Fonts/fixedsys.ttf"), 20);
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
            lastScreen.Update(deltaTime);
            nextScreen.Update(deltaTime);
            mousePos.Position = (Vector2)win.InternalGetMousePosition();
            mousePos.DisplayedString = win.InternalGetMousePosition().ToString(); 
            foreach(LevelSelectButton l in mainButtonList)
            {
                l.Update(deltaTime, win, currentScreenPosition);
            }
            SetCurrentLevelInfo();
            levelInfo.Update(deltaTime, currentScreenPosition);
            leftButton.Update(deltaTime, win, currentScreenPosition);
            rightButton.Update(deltaTime, win, currentScreenPosition);
            if (stopwatch.ElapsedMilliseconds > 500)
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
                {
                    stars.lastSelectedLevel = currentLevel;
                    stars.saveManageStars(profiles.getActiveProfileName());
                    return GameState.MainMenu;
                }
                if (sliding)
                {
                    SlideMap(deltaTime);
                    return GameState.LoadLevelState;
                }
                bool soundactiv = false;
                if(KeyboardInputManager.Downward(Keyboard.Key.Up) || KeyboardInputManager.Downward(Keyboard.Key.Down))
                {
                    int bottomLength = 1;
                    if(KeyboardInputManager.Downward(Keyboard.Key.Up) && currentScreenPosition.Y != 0)
                    {
                        soundactiv = true;
                        currentScreenPosition.Y = 0;
                        currentScreenPosition.X = GetPositionOnCurrentLevelScreen();
                    }
                    if (KeyboardInputManager.Downward(Keyboard.Key.Down) && currentScreenPosition.Y != 1)
                    {
                        soundactiv = true;
                        currentScreenPosition.Y = 1;
                        float help = (float) currentScreenPosition.X * (float) bottomLength / (float) (mainButtonList.Count - 1);
                        currentScreenPosition.X = (int) Math.Round(help);
                    }
                }

                if ((KeyboardInputManager.Downward(Keyboard.Key.Left) || KeyboardInputManager.Downward(Keyboard.Key.Right)) & currentScreenPosition.Y == 1)
                {
                    int bottomLength = 1;
                    if(KeyboardInputManager.Downward(Keyboard.Key.Right) && currentScreenPosition.X < bottomLength)
                    {
                        soundactiv = true;
                        currentScreenPosition.X += 1;
                    }
                    if(KeyboardInputManager.Downward(Keyboard.Key.Left) && currentScreenPosition.X > 0)
                    {
                        soundactiv = true;
                        currentScreenPosition.X -= 1;
                    }
                }
                if ((KeyboardInputManager.Downward(Keyboard.Key.Left) || KeyboardInputManager.Downward(Keyboard.Key.Right)) && currentScreenPosition.Y == 0)
                {
                    if (KeyboardInputManager.Downward(Keyboard.Key.Left))
                    {
                        if(currentLevel > 0)
                        {
                            soundactiv = true;
                            currentLevel--;
                            currentScreenPosition.X -= 1;
                            if (currentScreenPosition.X < 0)
                            {
                                InitiateSlide(false);
                                currentScreenPosition.X = GetPositionOnCurrentLevelScreen();
                            }
                        }
                    }
                    else
                    {
                        if (stars.levelIsUnlocked(currentLevel + 1))
                        {
                            soundactiv = true;
                            currentLevel++;
                            currentScreenPosition.X = GetPositionOnCurrentLevelScreen();
                            if (GetPositionOnCurrentLevelScreen() == 0)
                            {
                                InitiateSlide(true);
                            }
                        }
                    }
                    if (soundactiv)
                        MusicManager.PlaySound(AssetManager.SoundName.MenueClick);
                    else if (KeyboardInputManager.Downward(Keyboard.Key.Up) || KeyboardInputManager.Downward(Keyboard.Key.Down) || KeyboardInputManager.Downward(Keyboard.Key.Right) || KeyboardInputManager.Downward(Keyboard.Key.Left))
                    {
                        MusicManager.PlaySound(AssetManager.SoundName.Wall);
                    }

                    return GameState.LoadLevelState;
                }

                if (soundactiv)
                    MusicManager.PlaySound(AssetManager.SoundName.MenueClick);
                else if(KeyboardInputManager.Downward(Keyboard.Key.Up) || KeyboardInputManager.Downward(Keyboard.Key.Down) || KeyboardInputManager.Downward(Keyboard.Key.Right) || KeyboardInputManager.Downward(Keyboard.Key.Left))
                {
                    MusicManager.PlaySound(AssetManager.SoundName.Wall);
                }
                    


                if (KeyboardInputManager.Downward(Keyboard.Key.Return) && currentScreenPosition.Y == 0)
                {
                    return StartLevelIfUnlocked();
                }
                if (KeyboardInputManager.Downward(Keyboard.Key.Return) && currentScreenPosition.Y == 1)                       //Wurde die LinkeMaustaste gedrückt?
                {
                    //Console.WriteLine("Der Index in der SwitchAnweisung: " + index);
                    switch (currentScreenPosition.X)                                                  //Bin mit der Maus über den Index: SwitchCaseWeg
                    {                                                               //bearbeitet das aktuelle TextFeld
                                                                                    //
                        case 0:
                            CanSlide(false);
                            return GameState.LoadLevelState;
                        case 1:
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
            helpButtonTargetList.RemoveAll(b => true);
            mainButtonTargetList.RemoveAll(b => true);
            int currentScreen = GetIndexOfCurrentLevelScreen();
            worldNameFakePos = worldNameRealPos;
            worldNameRealPosTarget = worldNameRealPos;
            if (right)
            {
                helpMap.Texture = levelSelectList[currentScreen - 1].texture;
                SetButtonList(helpButtonList, currentScreen - 1);
                slideOffscreenStartPosition = new Vector2f(mainMap.Position.X + 1280, mainMap.Position.Y);
                slideOffsrceenEndPosition = new Vector2f(mainMap.Position.X - 1280, mainMap.Position.Y);
                foreach (LevelSelectButton l in helpButtonList)
                    helpButtonTargetList.Add(new Vector2f(l.position.X - 1280, l.position.Y));
                worldNameRealPos = new Vector2f(worldNameRealPos.X - 1280, worldNameRealPos.Y);
                worldNameFakeposTarget = new Vector2f(worldNameFakePos.X + 1280, worldNameFakePos.Y);
            }
            else
            {
                helpMap.Texture = levelSelectList[currentScreen + 1].texture;
                SetButtonList(helpButtonList, currentScreen + 1);
                slideOffscreenStartPosition = new Vector2f(mainMap.Position.X - 1280, mainMap.Position.Y);
                slideOffsrceenEndPosition = new Vector2f(mainMap.Position.X + 1280, mainMap.Position.Y);
                foreach (LevelSelectButton l in helpButtonList)
                    helpButtonTargetList.Add(new Vector2f(l.position.X + 1280, l.position.Y));
                worldNameRealPos = new Vector2f(worldNameRealPos.X + 1280, worldNameRealPos.Y);
                worldNameFakeposTarget = new Vector2f(worldNameFakePos.X - 1280, worldNameFakePos.Y);
            }
            foreach (LevelSelectButton l in mainButtonList)
            {
                mainButtonTargetList.Add(new Vector2f(l.position.X, l.position.Y));
                l.position = (!right)?l.position + new Vector2(-1280, 0) : l.position + new Vector2(1280, 0);
            }
            worldName.DisplayedString = "World " + GetIndexOfCurrentLevelScreen();
            mainMap.Position = slideOffscreenStartPosition;
            mainMap.Texture = levelSelectList[currentScreen].texture;
            helpMap.Position = slideEndPosition;
        }

        public void SlideMap(float deltaTime)
        {
            mainMap.Position = Vector2.lerp(mainMap.Position, slideEndPosition, slideSpeed * deltaTime);
            helpMap.Position = Vector2.lerp(helpMap.Position, slideOffsrceenEndPosition,  slideSpeed * deltaTime);
            worldNameFakePos = Vector2.lerp(worldNameFakePos, worldNameFakeposTarget, slideSpeed * deltaTime);
            worldNameRealPos = Vector2.lerp(worldNameRealPos, worldNameRealPosTarget, slideSpeed * deltaTime);
            int i = 0;
            foreach (LevelSelectButton l in mainButtonList)
                l.position = Vector2.lerp(l.position, mainButtonTargetList[i++],slideSpeed * deltaTime);
            i = 0;
            foreach (LevelSelectButton l in helpButtonList)
                l.position = Vector2.lerp(l.position, helpButtonTargetList[i++], slideSpeed * deltaTime);
            if (Math.Abs(Vector2.distance( mainMap.Position, slideEndPosition)) <= 0.8f)
            {
                sliding = false;
                mainMap.Position = slideEndPosition;
                helpMap.Position = slideOffscreenStartPosition;
                worldNameRealPos = worldNameRealPosTarget;
                i = 0;
                foreach (LevelSelectButton l in mainButtonList)
                    l.position = mainButtonTargetList[i++];
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
            worldName.Position = worldNameRealPos;
            worldName.Draw(win, RenderStates.Default);
            worldName.Position = worldNameFakePos;
            worldName.Draw(win, RenderStates.Default);
            leftButton.Draw(win);
            rightButton.Draw(win);
            foreach (LevelSelectButton l in mainButtonList)
            {
                l.Draw(win);
            }
            levelInfo.Draw(win);
            foreach (LevelSelectButton l in mainButtonList)
            {
                if(l.highlighted)
                    l.Draw(win);
                if (l.buttonLevel == currentLevel)
                    l.Draw(win);
            }
            lastScreen.Draw(win, RenderStates.Default);
            nextScreen.Draw(win, RenderStates.Default);
            win.Draw(mousePos);
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
                    //currentScreenPosition.X = GetPositionOnCurrentLevelScreen();
                    InitiateSlide(true);
                    currentScreenPosition.X = 1;
                    return true;
                }
                else
                {
                    if (stars.getIndexOfFirstUnsolvedLevel() == 0 && currentLevel != 0)
                    {
                        currentLevel = mainButtonList[mainButtonList.Count - 1].buttonLevel;
                        Console.WriteLine("currentLevel: " + currentLevel);
                    }
                    else
                    {
                        while (stars.getIndexOfFirstUnsolvedLevel() > currentLevel)
                        {
                            currentLevel++;
                        }
                    }
                    currentScreenPosition.X = GetPositionOnCurrentLevelScreen();
                    Console.WriteLine("currentScreenPos: " + currentScreenPosition.X);
                    currentScreenPosition.Y = 0;
                }
            }
            else
            {
                help = help - currentMod - 1;
                if (help >= levelSelectList[0].posList.Count - 1)
                {
                    currentLevel = help;
                    //currentScreenPosition.X = GetPositionOnCurrentLevelScreen();
                    InitiateSlide(false);
                    currentScreenPosition.X = 0;
                    return true;
                }
                else if (currentLevel <= levelSelectList[0].posList.Count )
                {
                    currentLevel = 0;
                    currentScreenPosition.X = GetPositionOnCurrentLevelScreen();
                    currentScreenPosition.Y = 0;
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

        private void SetCurrentLevelInfo()
        {
            levelInfo.SetNewLevel(mainButtonList[GetPositionOnCurrentLevelScreen()], stars.GetScoreOfLevel(currentLevel));
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
                buttonList.Add(new LevelSelectButton(v, curLevel, new Vector2i(curLevel - levelSelectList[mapIndex].startIndex, 0)));
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
                buttonList.Add(new LevelSelectButton(v, curLevel, new Vector2i(currentLevel - levelSelectList[mapIndex].startIndex, 0)));
                curLevel++;
            }
        }
    }
}
