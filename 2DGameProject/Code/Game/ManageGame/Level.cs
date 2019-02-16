using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using SFML.Graphics;
using SFML.Window;
using SFML.Audio;

namespace MemoryMaze
{
    class Level
    {
        //Contains and manages the actual Levels
        Player player;
        Map map;
        ItemList itemList;
        TrapHandler trapHandler;
        LevelutionHandler levelution;
        TransportHandler transporterHandler;
        MapFromTxt mapFromText = new MapFromTxt();

        Sprite background = new Sprite();

        Tutorial currentTutorial;

        int mapStatus = 0;
        int playerScore = 0;
        int[] ratingNumbers = new int[3];
        public int keysToUnlock { get; private set; }

        int[] lastLevelIndex = { 11, 23, 30, 44 };

        SuperText guiScoreNumber;
        SuperText guiScore;
        Sprite guiScoreBox;

        SuperText guiLevelNumber;
        SuperText guiLevel;
        Sprite guiLevelBox;

        Stopwatch hackWatch;
        bool finished = false;

        AnimatedSprite endAnimation;
        Sprite endSprite;
        SuperText endText;
        Sprite endMedal;

        // crashes sometimes when loading different levels relatively fast 
        /// <summary>
        /// the loading calls in this function seem to crash in some case -> be sure to handle any exceptions thrown at you!
        /// </summary>
        private void InitializeStrings()
        {
            guiScoreNumber = new SuperText("", FontLoader.Instance.LoadFont("Assets/Fonts/fixedsys.ttf"), 0.1F);
            guiScore = new SuperText("Steps", FontLoader.Instance.LoadFont("Assets/Fonts/fixedsys.ttf"), 0.5F);
            guiScoreBox = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.HUDSteps));

            guiLevelNumber = new SuperText("", FontLoader.Instance.LoadFont("Assets/Fonts/fixedsys.ttf"), 0.1F);
            guiLevel = new SuperText("Level", FontLoader.Instance.LoadFont("Assets/Fonts/fixedsys.ttf"), 0.5F);
            guiLevelBox = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.HUDSteps));
        }

        private void SafelyInitializeStrings()
        {
            for (int i = 0; i < 100; i++)
            {
                try
                {
                    InitializeStrings();
                } catch (SFML.LoadingFailedException e)
                {
                    Console.WriteLine("Failed to load Texts for the " + i + " time.");
                    continue;
                }
                break;
            }
        }

        // all variables initialized here need to be initialized in the copyconstructor too
        public Level(String mapfile, int sizePerCell, Vector2i position, int _keysToUnlock)
        {
            SafelyInitializeStrings();

            map = new Map(mapfile, sizePerCell);
            player = new Player(position, map);
            itemList = new ItemList(map);
            trapHandler = new TrapHandler(map);
            List<MapManipulation> list = new List<MapManipulation>();
            keysToUnlock = _keysToUnlock;
            ratingNumbers = mapFromText.getRatingNumbersFromMap(mapfile);
            // deletes all items from map AFTER they have been saved in the itemList
            // to simplify the placing of items without cluttering the map with extra blocks
            map.RemoveAllItems();
            map.RemoveAllTraps();
            levelution = mapFromText.getLevelutionHandler(mapfile, map);
            transporterHandler = mapFromText.getTransFromMap(mapfile, map);

        }

        // Constructor for the Copy function
        public Level(Map _map, Player _player, ItemList _itemList,TrapHandler _trapHandler, 
            LevelutionHandler _levelution, TransportHandler _transporter, int _playerScore, int _keysToUnlock, int[] _ratingNumbers)
        {
            SafelyInitializeStrings();

            map = _map;
            player = _player;
            itemList = _itemList;
            trapHandler = _trapHandler;
            keysToUnlock = _keysToUnlock;
            levelution = _levelution;
            transporterHandler = _transporter;
            this.setScoreCounter(_playerScore);
            ratingNumbers = _ratingNumbers;

            guiScoreNumber.CharacterSize = 50;
            guiScore.CharacterSize = 18;

            guiLevelNumber.CharacterSize = 50;
            guiLevel.CharacterSize = 18;
        }

        public Level Copy()
        {
            return new Level(map.Copy(), player.Copy(), itemList.Copy(), trapHandler.Copy(), levelution.Copy(), transporterHandler.Copy(), playerScore, keysToUnlock, ratingNumbers);
        }

        public int Update(float deltaTime, ManageStars.Rating rating, int curIndex, List<Tutorial> tutorials)
        {
            if (finished)
            {
                endAnimation.UpdateFrame(deltaTime);
                endText.Update(deltaTime);
                if(KeyboardInputManager.Downward(Keyboard.Key.Space))
                    mapStatus = 1;
            }
            else
            {
                background = new Sprite(AssetManager.backgroundTextures[0]);
                background.Position = new Vector2f(0, 0);
                getBackground(curIndex);
                mapStatus = 0;
                playerScore = player.scoreCounter;
                map.Update(deltaTime, player.keyCounter);
                player.Update(deltaTime, map);
                itemList.Update(map, player, deltaTime);
                trapHandler.Update(map, player, deltaTime);
                levelution.Update(player, map, deltaTime);
                transporterHandler.Update(player, deltaTime);
                checkTutorialNeed(rating, curIndex, tutorials);
                if (currentTutorial != null)
                {
                    currentTutorial.Update(deltaTime);
                }
                if (KeyboardInputManager.Upward(Keyboard.Key.T))
                {
                    return 3;
                }
                if (KeyboardInputManager.Upward(Keyboard.Key.Y))
                    mapStatus = 1;
                if (map.CellIsGoal(player.mapPosition) && player.keyCounter >= keysToUnlock)
                {
                    endSprite = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.LevelInfo));
                    endSprite.Position = new Vector2f(450, 220);
                    GraphicHelper.SetAlpha(200, endSprite);
                    SetEndMedal();
                    endMedal.Position = endSprite.Position + new Vector2f(150, 100);
                    endAnimation = new AnimatedSprite(AssetManager.GetTexture(AssetManager.TextureName.SpaceBar), 0.2f, 3);
                    endAnimation.Position = (Vector2)endSprite.Position + new Vector2(125, 200);
                    endText = new SuperText("Congratulations", FontLoader.Instance.LoadFont("Assets/Fonts/fixedsys.ttf"), 0.1f);
                    endText.Position = (Vector2)endSprite.Position + new Vector2(20, 25);
                    endText.CharacterSize = 40;
                    
                    finished = true;
                    addScoreFromBots();
                    CheckLevel();
                    Logger.Instance.Write("\n" + "Rating: " + playerScore + "\n" + "Bronze: " + ratingNumbers[0] + "\n" + "Silber: " + ratingNumbers[1] + "\n" + "Gold: " + ratingNumbers[2] + "\n" + "Sie haben " + CheckLevel() + " erreicht", Logger.level.Info);
                }
                if (KeyboardInputManager.Upward(Keyboard.Key.Back))
                {
                    MusicManager.StopSound();
                    foreach (Tutorial tut in tutorials)
                    {
                        if (tut.index == curIndex)
                        {
                            tut.shown = false;
                        }
                    }
                    mapStatus = 2;
                }
                guiLevelNumber.DisplayedString = "" + (curIndex + 1);
            }
            return mapStatus;
        }

        private String CheckLevel()
        {
            if (playerScore <= ratingNumbers[2])
                return "Gold";
            else if (playerScore <= ratingNumbers[1])
                return "Silber";
            else
                return "Bronze";
        }

        public void Draw(RenderTexture win, View view, Vector2f relViewDif, float deltaTime)        {
            Stopwatch watch = new Stopwatch();

            watch.Start();
            win.Draw(background);
            map.Draw(win, view, relViewDif);
            levelution.Draw(win, view, relViewDif);
            long tMap = watch.ElapsedTicks;
            player.Draw(win, view, relViewDif, deltaTime);
            long tPlayer = watch.ElapsedTicks - tMap;
            itemList.Draw(win, view, relViewDif);
            long tItems = watch.ElapsedTicks- tPlayer - tMap;
            trapHandler.Draw(win, view, relViewDif);
            long tTraps = watch.ElapsedTicks- tItems - tPlayer - tMap;
            transporterHandler.Draw(win, view, relViewDif);
            levelution.DrawOutlines(win, view, relViewDif);
            if (currentTutorial != null)
            {
                currentTutorial.Draw(win, view, relViewDif);
            }
            if(finished == true)
            {
                win.Draw(endSprite);
                win.Draw(endAnimation);
                win.Draw(endMedal);
                endText.Draw(win, RenderStates.Default);
            }
            //Logger.Instance.Write("tMap: " + tMap + " tPlayer: " + tPlayer + " tItem: " + tItems + " tTraps: " + tTraps + " all: " + watch.ElapsedTicks, 0);
        }

        public void DrawGUI(GUI gui, float deltaTime)
        {
            map.DrawGUI(gui, deltaTime);
            player.DrawGUI(gui, deltaTime);

            // GUI
            // Score Box
            guiScoreBox.Position = new Vector2f(gui.view.Size.X - 30 - guiScoreBox.Texture.Size.X, 30);
            gui.Draw(guiScoreBox);
            guiScoreNumber.Position = guiScoreBox.Position + new Vector2f(13, 0);
            guiScoreNumber.DisplayedString = (playerScore <= 999 ? "" + playerScore : "X_X");
            guiScoreNumber.Update(deltaTime);
            gui.Draw(guiScoreNumber);
            guiScore.Position = guiScoreNumber.Position + new Vector2f(0, guiScoreNumber.CharacterSize);
            guiScore.Update(deltaTime);
            gui.Draw(guiScore);
            // Level Box
            guiLevelBox.Position = gui.view.Size - new Vector2f(guiLevelBox.TextureRect.Width + 30, guiLevelBox.TextureRect.Height + 30);
            gui.Draw(guiLevelBox);
            guiLevelNumber.Position = guiLevelBox.Position + new Vector2f(13, 0);
            guiLevelNumber.Update(deltaTime);
            gui.Draw(guiLevelNumber);
            guiLevel.Position = guiLevelNumber.Position + new Vector2f(0, guiLevelNumber.CharacterSize);
            guiLevel.Update(deltaTime);
            gui.Draw(guiLevel);
        }

        public void setScoreCounter(int score)
        {
            playerScore = score;
            player.scoreCounter = score;
        }

        public int getScoreCounter()
        {
            return playerScore;
        }

        public ManageStars.Rating getRating()
        {
            for (int i = 2; i > 0; i--)
            {
                if (ratingNumbers[i] >= playerScore)
                {
                    return (ManageStars.Rating)i+1;
                }
            }
            return ManageStars.Rating.Bronze;
        }

        private void addScoreFromBots()
        {
            playerScore += player.addScoreFromBots();
        }

        private void checkTutorialNeed(ManageStars.Rating levelRating, int curIndex, List<Tutorial> tutoList)
        {
            Tutorial nextTutorial = tutoList.Find(t => t.index == curIndex && !t.shown);
            if (nextTutorial != null)
            {
                if (hackWatch == null)
                {
                    hackWatch = new Stopwatch();
                    hackWatch.Start();
                }
                if (levelRating == ManageStars.Rating.Fail)
                {
                    switch (curIndex)
                    {
                        // sort the cases latest to earliest
                        case 0:
                            if (hackWatch.ElapsedMilliseconds > 1000)
                                nextTutorial.ActivateSecretPowers();
                            break;
                        case 1:
                            if (KeyboardInputManager.PressedKeys().Count > 0 && player.mapPosition.Equals(new Vector2i(4,3)))
                                nextTutorial.ActivateSecretPowers();
                            break;
                        case 12:
                            if (player.redbot && player.botList.Count > 0 && player.botList[0].mapPosition.Equals(new Vector2i(4, 3)) && nextTutorial.tutorialIndex == 4)
                            { nextTutorial.ActivateSecretPowers(); break; }
                            else if (player.redbot && KeyboardInputManager.PressedKeys().Count != 0 && nextTutorial.tutorialIndex == 3)
                            { nextTutorial.ActivateSecretPowers(); break; }
                            else if (nextTutorial.tutorialIndex == 2 && player.ghostaktiv && player.ghostPlayer.mapPosition.Equals(new Vector2i(7, 3)))
                            { nextTutorial.ActivateSecretPowers(); break; }
                            else if (player.ghostaktiv && nextTutorial.tutorialIndex == 1)
                            { nextTutorial.ActivateSecretPowers(); break; }
                            else if (player.redItemCounter > 0 && nextTutorial.tutorialIndex == 0)
                            { nextTutorial.ActivateSecretPowers(); break; }
                            break;
                    }
                    if (nextTutorial.shown)
                    {
                        currentTutorial = nextTutorial;
                    }
                }
            }
        }
        
        void getBackground(int currentIndex)
        {
            for(int i = 0; i < lastLevelIndex.Count(); i++)
            {
                if (currentIndex <= lastLevelIndex[i])
                {
                    background.Texture = AssetManager.backgroundTextures[i];
                    break;
                }
            }
            background.Color = new Color(15, 15, 15);
        }        

        void SetEndMedal()
        {
            switch(getRating())
            {
                case ManageStars.Rating.Bronze:
                    endMedal = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.BotBronze));
                    break;
                case ManageStars.Rating.Silver:
                    endMedal = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.BotSilver));
                    break;
                case ManageStars.Rating.Gold:
                    endMedal = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.BotGold));
                    break;
            }
        }
    }
}
