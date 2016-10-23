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
        Sprite sprite, iconSprite;
        IntRect spriteRect;
        bool highlighted;
        bool isIconButton;
        public Vector2i buttonPosition;
        Texture defaultTexture, glowTexture, iconTexture;
        public byte currentAlpha;

        public Button(Vector2f _position, Vector2i _buttonPosition, Texture _defaultTexture, Texture _glowTexture, float _rotation) : this(_position, _buttonPosition, _defaultTexture, _glowTexture)
        {
            sprite.Rotation = _rotation;
        }
            
        public Button(Vector2f _position, Vector2i _buttonPosition, Texture _defaultTexture, Texture _glowTexture)
        {
            position = _position;
            defaultTexture = _defaultTexture;
            glowTexture = _glowTexture;
            sprite = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.LevelButtonGlow));
            sprite.Texture = defaultTexture;
            sprite.Origin = new Vector2f(sprite.Texture.Size.X * 0.5f, sprite.Texture.Size.Y);
            sprite.TextureRect = new IntRect(0, 0, (int)sprite.Texture.Size.X, (int)sprite.Texture.Size.Y);
            sprite.Position = position;
            spriteRect = new IntRect((int)(position.X - sprite.Texture.Size.X * 0.5f), (int)(position.Y - sprite.Texture.Size.Y * 0.5f), (int)sprite.Texture.Size.X, (int)sprite.Texture.Size.Y);
            highlighted = false;
            buttonPosition = _buttonPosition;
            isIconButton = false;
            currentAlpha = 255;
        }

        public Button(Vector2f _position, Vector2i _buttonPosition, Texture _defaultTexture, Texture _glowTexture, Texture _iconTexture)
            : this(_position, _buttonPosition, _defaultTexture, _glowTexture)
        {
            iconTexture = _iconTexture;
            iconSprite = new Sprite(iconTexture);
            iconSprite.Origin = new Vector2f(iconSprite.Texture.Size.X * 0.5f, iconSprite.Texture.Size.Y * 0.5f);
            iconSprite.TextureRect = new IntRect(0, 0, (int)iconSprite.Texture.Size.X, (int)iconSprite.Texture.Size.Y);
            iconSprite.Position = position;
            isIconButton = true;
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
            if(isIconButton)
            {
                iconSprite.Position = position;
                win.Draw(iconSprite);
            }
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
            GraphicHelper.SetAlpha(currentAlpha, sprite);
            if (isIconButton)
                GraphicHelper.SetAlpha(currentAlpha, iconSprite);
            sprite.TextureRect = new IntRect(0, 0, (int)sprite.Texture.Size.X, (int)sprite.Texture.Size.Y);
        }

        private bool MouseOnButton(RenderWindow win)
        {
            Vector2i mousePos = win.InternalGetMousePosition();
            return spriteRect.Contains(mousePos.X, mousePos.Y);
        }
    }
}
