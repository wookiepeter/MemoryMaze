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
            itemList = getItemsFromMap(map);
        }

        public ItemList(ItemList _itemList)
        {
            itemList = _itemList.itemList;
        }

        public ItemList Copy()
        {
            return new ItemList(this);
        }

        public void Update(Map map, float deltaTime)
        {
            List<Item> removeList = new List<Item>();
            foreach(Item item in itemList)
            {
                item.Update(map, deltaTime);
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
                        case cellContent.Item: result.Add(new Key(curPos, map)); break;
                        default: break;
                    }           
                }
            }
            return result;
        }
    }
}
