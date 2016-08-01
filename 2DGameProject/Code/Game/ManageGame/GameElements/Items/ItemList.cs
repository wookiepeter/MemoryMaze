using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using SFML.Audio;


namespace MemoryMaze
{
    class ItemList
    {
        List<Item> itemList;

        public ItemList(Map map)
        {
            itemList = new List<Item>();
            foreach(Item item in getItemsFromMap(map))
            {
                itemList.Add(item.Copy());
            }
        }

        public ItemList(ItemList _itemList)
        {
            itemList = new List<Item>();
            foreach(Item item in _itemList.itemList)
            {
                itemList.Add(item.Copy());
            }
        }

        public ItemList Copy()
        {
            return new ItemList(this);
        }

        public void Update(Map map, Player player, float deltaTime)
        {
            List<Vector2i> botPosList = player.getListOfBotPositions();
            List<Item> removeList = new List<Item>();
            foreach(Item item in itemList)
            {
                item.Update(map, deltaTime);
                int i = 0;
                foreach (Vector2i vec in botPosList)
                {
                    if (!item.deleted)
                    {
                        Logger.Instance.Write("bot: " + i + " x: " + item.position.X + " - " + vec.X + " y: " + item.position.Y + " - " + vec.Y, 0);
                        if (item.position.X == vec.X && item.position.Y == vec.Y)
                        {
                            item.deleted = true;
                            player.collectItem(item);
                        }
                    }
                    i++;      
                }
            }
            itemList.RemoveAll(a => a.deleted == true);
        }

        public void Draw(RenderWindow win, View view)
        {
            foreach(Item item in itemList)
            {
                item.Draw(win, view);
            }
        }

        private List<Item> getItemsFromMap(Map map)
        {
            List<Item> result = new List<Item>();
            for(int j = 0; j < map.mapSizeY; j++)
            {
                for(int i = 0; i < map.mapSizeX; i++)
                {
                    Vector2i curPos = new Vector2i(i, j);
                    switch(map.getContentOfCell(curPos))
                    {
                        case cellContent.Item: result.Add(new RedItem(curPos, map));
                            break;
                        default: break;
                    }           
                }
            }
            return result;
        }
    }
}
