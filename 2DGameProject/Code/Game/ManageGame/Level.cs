using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

namespace MemoryMaze
{
    class Level
    {
        //Contains and manages the actual Levels
        Player player;
        Map map;
        ItemList itemList;

        int mapStatus = 0;
        int playerScore = 0;
        public int keysToUnlock { get; private set; }

        Text guiScore = new Text("", new Font("Assets/Fonts/calibri.ttf"), 30);

        // all variables initialized here need to be initialized in the copyconstructor too
        public Level(String mapfile, int sizePerCell, Vector2i position, int _keysToUnlock)
        {
            map = new Map(mapfile, sizePerCell);
            player = new Player(position, map);
            itemList = new ItemList(map);
            keysToUnlock = _keysToUnlock;
            // deletes all items from map AFTER they have been saved in the itemList
            // to simplify the placing of items without cluttering the map with extra blocks
            map.RemoveAllItems();
        }

        // Constructor for the Copy function
        public Level(Map _map, Player _player, ItemList _itemList, int _playerScore, int _keysToUnlock)
        {
            map = _map;
            player = _player;
            itemList = _itemList;
            keysToUnlock = _keysToUnlock;
            this.setScoreCounter(_playerScore);
        }

        public Level Copy()
        {
            return new Level(map.Copy(), player.Copy(), itemList.Copy(), playerScore, keysToUnlock);
        }

        public int update(float deltaTime)
        {
            mapStatus = 0;
            playerScore = player.scoreCounter;
            map.Update(deltaTime);
            player.Update(deltaTime, map);
            itemList.Update(map, player, deltaTime);
            if (map.CellIsGoal(player.mapPosition) && player.keyCounter >= keysToUnlock)
                mapStatus = 1;
            if (KeyboardInputManager.Upward(Keyboard.Key.Back))
                mapStatus = 2;
            return mapStatus;
        }

        public void Draw(RenderTexture win, View view)
        {
            map.Draw(win, view);
            player.Draw(win, view);
            itemList.Draw(win, view);
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
    }
}
