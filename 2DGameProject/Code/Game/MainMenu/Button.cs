using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML;
using SFML.Window;
using SFML.Graphics;

namespace MemoryMaze
{
    class Button
    {
        public Vector2 position;
        Sprite sprite;
        IntRect spriteRect;
        bool highlighted;
        public Vector2i buttonPosition;
        Texture defaultTexture, glowTexture;

        public Button(Vector2f _position, Vector2i _buttonPosition, Texture _defaultTexture, Texture _glowTexture)
        {
            position = _position;
            defaultTexture = _defaultTexture;
            glowTexture = _glowTexture;
            sprite = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.LevelButtonGlow));
            sprite.Texture = defaultTexture;
            sprite.Origin = new Vector2f(sprite.Texture.Size.X * 0.5f, sprite.Texture.Size.Y);
            sprite.TextureRect = new IntRect(0, 0, (int)sprite.Texture.Size.X, (int)sprite.Texture.Size.Y);
            sprite.Rotation = 90;
            sprite.Position = position;
            spriteRect = new IntRect((int)(position.X - sprite.Texture.Size.X * 0.5f), (int)(position.Y - sprite.Texture.Size.Y * 0.5f), (int)sprite.Texture.Size.X, (int)sprite.Texture.Size.Y);
            highlighted = false;
            buttonPosition = _buttonPosition;
        }

        public void Update(float deltaTime, RenderWindow win, Vector2i curPosition)
        {
            if (buttonPosition.Equals(curPosition))
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
            sprite.Position = position;
            win.Draw(sprite);
        }

        private void SetNewTexture()
        {
            if (highlighted)
            {
                sprite.Texture = glowTexture;
                sprite.Origin = new Vector2f(sprite.Texture.Size.X * 0.5f, sprite.Texture.Size.Y * 0.5f);
            }
            else
            {
                sprite.Texture = defaultTexture;
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
