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
    class LevelInfo
    {
        Sprite background;
        List<Sprite> starList;
        SuperText levelName;
        // position is not centered
        Vector2 position;

        ManageStars.Rating rating;
        LevelSelectButton button;
        Font font = new Font("Assets/Fonts/pixelhole.ttf");

        public LevelInfo(LevelSelectButton _button, Vector2f _position, ManageStars.Rating _rating)
        {
            position = _position;
            background = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.LevelInfo));
            background.Origin = new Vector2f(background.Texture.Size.X * 0.5f, background.Texture.Size.X * 0.5f);
            background.Position = (Vector2f)position + background.Origin;

            starList = new List<Sprite>();
            starList.Add(new Sprite(AssetManager.GetTexture(AssetManager.TextureName.BotBronze)));
            starList[0].Position = new Vector2f(20, 150);
            starList.Add(new Sprite(AssetManager.GetTexture(AssetManager.TextureName.BotSilver)));
            starList[1].Position = new Vector2f(100, 150);
            starList.Add(new Sprite(AssetManager.GetTexture(AssetManager.TextureName.BotGold)));
            starList[2].Position = new Vector2f(180, 150);

            SetNewLevel(_button, _rating);
        }

        public void Update(float deltaTime)
        {
        }

        public void Draw(RenderWindow win)
        {
            win.Draw(background);
            levelName.Draw(win, RenderStates.Default);
            foreach (Sprite s in starList)
                win.Draw(s);
        }

        public void SetNewLevel(LevelSelectButton _button, ManageStars.Rating _rating)
        {
            button = _button;
            rating = _rating;
            
            levelName = new SuperText("Level " + (button.buttonLevel + 1), font, 0.2f);
            levelName.CharacterSize = 50;
            levelName.Position = position + new Vector2(15, -10);
            for (int i = 0; i <= 2; i++)
            {
                Console.WriteLine("rating: " + rating + " i " + i);
                if (i < (int)rating)
                {
                    Console.WriteLine("i get here");
                    SetAlpha(255, starList[i]);
                }
                else
                {
                    SetAlpha(120, starList[i]);
                }
            }
        }

        private void SetAlpha(byte alpha, Sprite sprite)
        {
            Color help = sprite.Color;
            help.A = alpha;
            sprite.Color = help;
        }

        public int GetInfoButtonLevel()
        {
            return button.buttonLevel;
        }
    }
}
