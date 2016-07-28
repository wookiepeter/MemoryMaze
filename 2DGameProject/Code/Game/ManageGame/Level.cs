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

        // all variables initialized here need to be initialized in the copyconstructor too
        public Level(String mapfile, int sizePerCell, Vector2i position)
        {
            map = new Map(mapfile, sizePerCell);
            player = new Player(position, map);
            itemList = new ItemList(map);
            // deletes all items from map AFTER they have been saved in the itemList
            // to simplify the placing of items without cluttering the map with extra blocks
            map.RemoveAllItems();
        }

        // Constructor for the Copy function
        public Level(Map _map, Player _player, ItemList _itemList)
        {
            map = _map;
            player = _player;
            itemList = _itemList;
        }

        public Level Copy()
        {
            return new Level(map.Copy(), player.Copy(), itemList.Copy());
        }

        public int update(float deltaTime)
        {
            mapStatus = 0;
            map.Update(deltaTime);
            player.Update(deltaTime, map);
            // TODO: check if the order is correct
            itemList.Update(map, deltaTime);
            if (map.CellIsGoal(player.mapPosition))
                mapStatus = 1;
            if (KeyboardInputManager.Upward(Keyboard.Key.Back))
                mapStatus = 2;
            return mapStatus;
        }

        public void draw(RenderWindow win, View view)
        {
            map.Draw(win, view);
            player.Draw(win, view);
            itemList.Draw(win, view);
        }

        public void DrawGUI(GUI gui, float deltaTime)
        {
            map.DrawGUI(gui, deltaTime);
            player.DrawGUI(gui, deltaTime);
        }
    }
}
