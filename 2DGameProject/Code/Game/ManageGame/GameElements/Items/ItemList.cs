﻿using System;
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
            List<Vector2i> botPosList = player.getListWithPlayerAndBlueBot();
            List<Item> removeList = new List<Item>();
        

            foreach(Item item in itemList)
            {
                item.Update(map, deltaTime);
                foreach (Vector2i vec in botPosList)
                {
                    
                    if (!item.deleted)
                    {
                        if (item.position.X == vec.X && item.position.Y == vec.Y)
                        {
                            item.deleted = true;
                            player.collectItem(item);
                        }
                    } 
                }

            }
            itemList.RemoveAll(a => a.deleted == true);
        }


        public void Draw(RenderTexture win, View view, Vector2f relViewDis)
        {
            foreach(Item item in itemList)
            {
                item.Draw(win, view, relViewDis);
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
                    switch(map.GetContentOfCell(curPos))
                    {
                        case cellContent.Item: result.Add(new Key(curPos, map));
                            break;
                        case cellContent.RedItem: result.Add(new RedItem(curPos, map));
                            break;
                        case cellContent.BlueItem: result.Add(new BlueItem(curPos, map));
                            break;
                        case cellContent.GreenItem: result.Add(new GreenItem(curPos, map));
                            break;
                        case cellContent.ScoreItem: result.Add(new ScoreItem(curPos, map));
                            break;
                        default: break;
                    }           
                }
            }
            return result;
        }
    }
}
