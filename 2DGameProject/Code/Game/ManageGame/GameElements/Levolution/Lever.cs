using System;
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
        public Vector2i position;
        protected Sprite sprite = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.LeverOpen));

        // Position of the sprite based on sizepercell and its position
        public Vector2f exactPosition;

        public List<MapManipulation> mapManilList;

        protected bool active = false;

        public abstract Lever Copy();

        public abstract void Update(Player player, Map map, float deltaTime);

        public abstract void Draw(RenderTexture win, View view, Vector2f relViewDis);

        protected abstract void Execute(Map map, Player player);
        
    }
}
