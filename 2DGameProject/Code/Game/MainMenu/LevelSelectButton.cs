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
        // sprite Position is centered
        public Vector2 position;
        Vector2i screenPosition;
        Sprite sprite;
        IntRect spriteRect;
        public bool highlighted;
        public int buttonLevel;
        public ManageStars.Rating rating;

        public LevelSelectButton(Vector2f _position, int _buttonLevel, Vector2i _screenPosition, ManageStars.Rating _rating)
        {
            position = _position;
            sprite = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.LevelButtonGlow));
            sprite.Texture = AssetManager.GetTexture(AssetManager.TextureName.LevelButton);
            sprite.Origin = new Vector2f(sprite.Texture.Size.X * 0.5f, sprite.Texture.Size.Y);
            sprite.TextureRect = new IntRect(0, 0, (int)sprite.Texture.Size.X, (int)sprite.Texture.Size.Y);
            sprite.Position = position;
            spriteRect = new IntRect((int)(position.X - sprite.Texture.Size.X * 0.5f), (int)(position.Y - sprite.Texture.Size.Y*0.5f), (int)sprite.Texture.Size.X, (int)sprite.Texture.Size.Y);
            highlighted = false;
            buttonLevel = _buttonLevel;
            screenPosition = _screenPosition;
            rating = _rating;
        }

        public void Update(float deltaTime, RenderWindow win, Vector2i currentPosition)
        {
            if (screenPosition.X == currentPosition.X && currentPosition.Y == 0)
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
            switch (rating)
            {
                case ManageStars.Rating.Fail:
                    if (highlighted)
                        sprite.Texture = AssetManager.GetTexture(AssetManager.TextureName.LevelButtonGlow);
                    else
                        sprite.Texture = AssetManager.GetTexture(AssetManager.TextureName.LevelButton);
                    break;
                case ManageStars.Rating.Bronze:
                    if (highlighted)
                        sprite.Texture = AssetManager.GetTexture(AssetManager.TextureName.BotBronzeDone);
                    else
                        sprite.Texture = AssetManager.GetTexture(AssetManager.TextureName.BotBronze);
                    break;
                case ManageStars.Rating.Silver:
                    if (highlighted)
                        sprite.Texture = AssetManager.GetTexture(AssetManager.TextureName.BotSilverDone);
                    else
                        sprite.Texture = AssetManager.GetTexture(AssetManager.TextureName.BotSilver);
                    break;
                case ManageStars.Rating.Gold:
                    if (highlighted)
                        sprite.Texture = AssetManager.GetTexture(AssetManager.TextureName.BotGoldDone);
                    else
                        sprite.Texture = AssetManager.GetTexture(AssetManager.TextureName.BotGold);
                    break;
                case ManageStars.Rating.Skipped:
                    if (highlighted)
                        sprite.Texture = AssetManager.GetTexture(AssetManager.TextureName.SkipMedalDone);
                    else
                        sprite.Texture = AssetManager.GetTexture(AssetManager.TextureName.SkipMedal);
                    break;
            }

            sprite.Origin = new Vector2f(sprite.Texture.Size.X * 0.5f, sprite.Texture.Size.Y * 0.5f);
            sprite.TextureRect = new IntRect(0, 0, (int)sprite.Texture.Size.X, (int)sprite.Texture.Size.Y);
        }

        private bool MouseOnButton(RenderWindow win)
        {
            Vector2i mousePos = win.InternalGetMousePosition();
            return spriteRect.Contains(mousePos.X, mousePos.Y);
        }
    }
}
