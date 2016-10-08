using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML;
using SFML.Graphics;
using SFML.Window;

namespace MemoryMaze
{
    class LevelSelectButton
    {
        Vector2 position;
        Sprite sprite;
        IntRect spriteRect;
        bool highlighted;

        public LevelSelectButton(Vector2f _position, int buttonLevel)
        {
            position = _position;
            sprite = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.LevelButtonGlow));
            sprite.Texture = AssetManager.GetTexture(AssetManager.TextureName.LevelButton);
            sprite.Origin = new Vector2f(sprite.Texture.Size.X * 0.5f, sprite.Texture.Size.Y);
            sprite.TextureRect = new IntRect(0, 0, (int)sprite.Texture.Size.X, (int)sprite.Texture.Size.Y);
            sprite.Position = position;
            spriteRect = new IntRect((int)(position.X - sprite.Texture.Size.X * 0.5f), (int)(position.Y - sprite.Texture.Size.Y*0.5f), (int)sprite.Texture.Size.X, (int)sprite.Texture.Size.Y);
            highlighted = false;
        }

        public void Update(float deltaTime, RenderWindow win)
        {
            if (MouseOnButton(win))
            {
                highlighted = true;
            }
            else
            {
                highlighted = false;
            }
            SetNewTexture();
        }

        public void Draw(RenderWindow win)
        {
            win.Draw(sprite);
        }

        private void SetNewTexture()
        {
            if (highlighted)
            {
                sprite.Texture = AssetManager.GetTexture(AssetManager.TextureName.LevelButtonGlow);
                sprite.Origin = new Vector2f(sprite.Texture.Size.X * 0.5f, sprite.Texture.Size.Y*0.5f);
            }
            else
            {
                sprite.Texture = AssetManager.GetTexture(AssetManager.TextureName.LevelButton);
                sprite.Origin = new Vector2f(sprite.Texture.Size.X * 0.5f, sprite.Texture.Size.Y * 0.5f);
            }
            sprite.TextureRect = new IntRect(0, 0, (int)sprite.Texture.Size.X, (int)sprite.Texture.Size.Y);
        }

        private bool MouseOnButton(RenderWindow win)
        {
            Vector2i mousePos = win.InternalGetMousePosition();
            return spriteRect.Contains(mousePos.X, mousePos.Y);
        }
    }
}
