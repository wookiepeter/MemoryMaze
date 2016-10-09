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
    public class Key : Item
    {
        AnimatedSprite sprite = new AnimatedSprite(AssetManager.GetTexture(AssetManager.TextureName.KeyAnimated), 0.2F, 8);
        AnimatedSprite particlesSprite = new AnimatedSprite(AssetManager.GetTexture(AssetManager.TextureName.ParticlesAnimated), 0.055F, 13);

        // This doesn't check if the item is valid
        public Key(Vector2i _position, Map map)
        {
            Logger.Instance.Write(_position.ToString(), 0);
            position = _position;
            deleted = false;
            exactPosition = new Vector2f(position.X * map.sizePerCell, position.Y * map.sizePerCell);
            sprite.Scale = new Vector2f((float)map.sizePerCell/(float)sprite.spriteSize.X, (float)map.sizePerCell / (float)sprite.spriteSize.Y);
            particlesSprite.Scale = new Vector2f((float)map.sizePerCell / (float)particlesSprite.spriteSize.X, (float)map.sizePerCell / (float)particlesSprite.spriteSize.Y);
        }

        // CopyConstructor
        public Key(Key _key)
        {
            position = _key.position;
            exactPosition = _key.exactPosition;
            sprite.Position = _key.sprite.Position;
            sprite.Scale = _key.sprite.Scale;
            particlesSprite.Position = _key.particlesSprite.Position;
            particlesSprite.Scale = _key.particlesSprite.Scale;
        }

        override public Item Copy()
        {
            return new Key(this);
        }

        override public void Update(Map map, float deltaTime)
        {
            if (!map.CellIsWalkable(position))
                deleted = true;
            sprite.UpdateFrame(deltaTime);
            particlesSprite.UpdateFrame(deltaTime);
        }

        override public void Draw(RenderTexture win, View view, Vector2f relViewDis)
        {
            sprite.Position = exactPosition + relViewDis;
            particlesSprite.Position = exactPosition + relViewDis;

            win.Draw(particlesSprite);
            win.Draw(sprite);
        }

    }
}
