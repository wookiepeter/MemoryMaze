﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

namespace MemoryMaze
{
    

    public abstract class Lever
    {
        protected Vector2i position;
        protected Sprite sprite = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.Lever));

        // Position of the sprite based on sizepercell and its position
        protected Vector2f exactPosition;

        protected List<MapManipulation> mapManilList;

        protected bool active = false;

        public abstract Lever Copy();

        public abstract void Update(Player player, Map map, float deltaTime);

        public abstract void Draw(RenderTexture win, View view, Vector2f relViewDis);

        protected abstract void Execute(Map map, Player player);
        
    }
}
