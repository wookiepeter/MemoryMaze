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

        int mapStatus = 0;
        int playerScore = 0;
        int[] ratingNumbers = new int[3];
        public int keysToUnlock { get; private set; }

        Text guiScore = new Text("", new Font("Assets/Fonts/calibri.ttf"), 30);

        // all variables initialized here need to be initialized in the copyconstructor too
        public Level(String mapfile, int sizePerCell, Vector2i position, int _keysToUnlock)
        {
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
            map = _map;
            player = _player;
            itemList = _itemList;
            trapHandler = _trapHandler;
            keysToUnlock = _keysToUnlock;
            levelution = _levelution;
            transporterHandler = _transporter;
            this.setScoreCounter(_playerScore);
            ratingNumbers = _ratingNumbers;
        }

        public Level Copy()
        {
            return new Level(map.Copy(), player.Copy(), itemList.Copy(), trapHandler.Copy(), levelution.Copy(), transporterHandler.Copy(), playerScore, keysToUnlock, ratingNumbers);
        }

        public int update(float deltaTime)
        {
            mapStatus = 0;
            playerScore = player.scoreCounter;
            map.Update(deltaTime);
            player.Update(deltaTime, map);
            itemList.Update(map, player, deltaTime);
            trapHandler.Update(map, player, deltaTime);
            levelution.Update(player, map, deltaTime);
            transporterHandler.Update(player, deltaTime);
            if (KeyboardInputManager.Upward(Keyboard.Key.Y))
                mapStatus = 1;
            if (map.CellIsGoal(player.mapPosition) && player.keyCounter >= keysToUnlock)
            {
                addScoreFromBots();
                mapStatus = 1;
            }
            if (KeyboardInputManager.Upward(Keyboard.Key.Back))
                mapStatus = 2;
            return mapStatus;
        }

        public void Draw(RenderTexture win, View view, Vector2f relViewDif, float deltaTime)        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
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
            //Logger.Instance.Write("tMap: " + tMap + " tPlayer: " + tPlayer + " tItem: " + tItems + " tTraps: " + tTraps + " all: " + watch.ElapsedTicks, 0);
        }

        public void DrawGUI(GUI gui, float deltaTime)
        {
            map.DrawGUI(gui, deltaTime);
            player.DrawGUI(gui, deltaTime);

            updateGuiText(gui);
            gui.Draw(guiScore);
        }

        private void updateGuiText(GUI gui)
        {
            guiScore.Position = new Vector2f(gui.view.Size.X-50, 25);
            guiScore.DisplayedString = "" + playerScore;
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
            for (int i = 0; i < 3;i++)
            {
                if (ratingNumbers[i] < playerScore)
                {
                    return (ManageStars.Rating)i;
                }
            }
            return ManageStars.Rating.Gold;
        }

        private void addScoreFromBots()
        {
            playerScore += player.addScoreFromBots();
        }
    }
}
