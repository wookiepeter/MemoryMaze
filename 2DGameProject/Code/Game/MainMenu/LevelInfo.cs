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
        List<AnimatedSprite> starList;
        List<Sprite> lockedStarList;
        SuperText levelName;
        // position is not centered
        Vector2 position;

        ManageStars.Rating rating;
        LevelSelectButton button;
        Font font = new Font("Assets/Fonts/pixelhole.ttf");

        public LevelInfo(LevelSelectButton _button, ManageStars.Rating _rating)
        {
            background = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.LevelInfo));
            background.Origin = new Vector2f(background.Texture.Size.X * 0.5f, background.Texture.Size.X * 0.5f);
            background.Rotation = 90;
            button = _button;
            position = button.position + new Vector2(-background.Texture.Size.X * 0.5f, -(background.Texture.Size.Y + 200));
            background.Position = position + new Vector2(background.Texture.Size.X * 0.5f, background.Texture.Size.Y * 0.5f);
            rating = _rating;

            levelName = new SuperText("Level " + button.buttonLevel, font, 0.2f);
            levelName.Position = position + new Vector2(30, 100);
            starList = new List<AnimatedSprite>();
            lockedStarList = new List<Sprite>();
            for (int i = 0; i < 3; i++)
            {
                if (i < (int) rating)
                {
                    starList.Add(new AnimatedSprite(AssetManager.GetTexture(AssetManager.TextureName.StarRotating), 0.1f, 8));
                    starList[starList.Count-1].Position = position + new Vector2(20 + i * 50, 300);
                }
                else
                {
                    lockedStarList.Add(new Sprite(AssetManager.GetTexture(AssetManager.TextureName.Star)));
                    lockedStarList[lockedStarList.Count - 1].Position = position + new Vector2(20 + i * 50, 300);
                }
            }
        }

        public void Update(float deltaTime)
        {
            foreach (AnimatedSprite an in starList)
                an.UpdateFrame(deltaTime);
        }

        public void Draw(RenderWindow win)
        {
            Console.WriteLine("fuck you" + position);
            win.Draw(background);
            levelName.Draw(win, RenderStates.Default);
            foreach (AnimatedSprite an in starList)
                win.Draw(an);
            foreach (Sprite s in lockedStarList)
                win.Draw(s);
        }

        public void SetNewLevel(LevelSelectButton _button, ManageStars.Rating rating)
        {
            background.Texture = AssetManager.GetTexture(AssetManager.TextureName.LevelInfo);
            background.Origin = new Vector2f(background.Texture.Size.X * 0.5f, background.Texture.Size.X * 0.5f);
            button = _button;
            position = button.position + new Vector2(-background.Texture.Size.X * 0.5f, -(background.Texture.Size.Y));
            background.Position = position + new Vector2(background.Texture.Size.X * 0.5f, background.Texture.Size.Y * 0.5f);

            levelName = new SuperText("Level " + button.buttonLevel, font, 0.2f);
            levelName.Position = position + new Vector2(30, 100);
            starList = new List<AnimatedSprite>();
            lockedStarList = new List<Sprite>();
            for (int i = 0; i < 3; i++)
            {
                if (i < (int)rating)
                {
                    starList.Add(new AnimatedSprite(AssetManager.GetTexture(AssetManager.TextureName.StarRotating), 0.1f, 8));
                    starList[starList.Count - 1].Position = position + new Vector2(20 + i * 50, 300);
                }
                else
                {
                    lockedStarList.Add(new Sprite(AssetManager.GetTexture(AssetManager.TextureName.Star)));
                    lockedStarList[lockedStarList.Count - 1].Position = position + new Vector2(20 + i * 50, 300);
                }
            }
        }
    }
}
