﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

namespace MemoryMaze
{
    public class BlueLever : Lever
    {
        public BlueLever(Vector2i _position, Map map, List<MapManipulation> _mapManiList) : base ()
        {
            position = _position;
            mapManilList = new List<MapManipulation>();
            foreach (MapManipulation mapmani in _mapManiList)
            {
                mapManilList.Add(mapmani);
            }
            exactPosition = new Vector2f(position.X * map.GetSizePerCell() + (float)map.GetSizePerCell() * 0.25f,
                position.Y * map.GetSizePerCell() + (float)map.GetSizePerCell() * 0.25f);
            sprite.Scale = new Vector2f((float)map.GetSizePerCell() * 0.5f / (float)sprite.Texture.Size.X,
                (float)map.GetSizePerCell() * 0.5f / (float)sprite.Texture.Size.Y);
        }

        private BlueLever(BlueLever _lever)
        {
            position = _lever.position;
            sprite.Position = _lever.sprite.Position;
            sprite.Scale = _lever.sprite.Scale;
            mapManilList = new List<MapManipulation>();
            exactPosition = _lever.exactPosition;
            foreach (MapManipulation mani in _lever.mapManilList)
            {
                mapManilList.Add(mani.Copy());
            }
        }

        override public Lever Copy()
        {
            return new BlueLever(this);
        }

        override public void Update(Player player, Map map, float deltaTime)
        {
            Console.WriteLine(player.getListOfBotPositions().Count);
            if (VirusOnLever(player.getListOfBotPositions(), map))
            {
                if(!active)
                {
                    Execute(map, player);
                    active = !active;
                }
            }
            else
            {
                if(active)
                {
                    Execute(map, player);
                    active = !active;
                }
            }
        }

        protected Boolean VirusOnLever(List<Vector2i> botPosList, Map map)
        {
            if (botPosList.Contains(position))
                return true;
            return false;
        }

        override protected void Execute(Map map, Player player)
        {
            foreach (MapManipulation mani in mapManilList)
            {
                if (!player.getListOfBotPositions().Contains(mani.position))
                    mani.execute(map);
            }
        }

        public override void Draw(RenderTexture win, View view, Vector2f relViewDis)
        {
            sprite.Position = exactPosition + relViewDis;
            win.Draw(sprite);
        }
    }
}
