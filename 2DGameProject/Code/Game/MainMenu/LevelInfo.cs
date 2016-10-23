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
        Sprite screenShot;
        Sprite medal;
        List<RectangleShape> lines;
        List<Vector2> cornerPositions;
        SuperText levelName;
        // position is not centered
        Vector2 position;

        ManageStars.Rating rating;
        LevelSelectButton button;
        Font font = new Font("Assets/Fonts/pixelhole.ttf");
        Color defaultLineColor;
        bool highlighted;

        public LevelInfo(LevelSelectButton _button, Vector2f _position, ManageStars.Rating _rating)
        {
            position = _position;
            background = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.LevelInfo));
            background.Origin = new Vector2f(background.Texture.Size.X * 0.5f, background.Texture.Size.X * 0.5f);
            background.Position = (Vector2f)position + background.Origin;

            screenShot = new Sprite(AssetManager.GetScreenShot(_button.buttonLevel));
            screenShot.Position = position + new Vector2(20, 20);

            medal = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.BotBronze));

            defaultLineColor = new Color(255, 255, 255);
            float cornerDist = 10;
            cornerPositions = new List<Vector2>();
            cornerPositions.Add(position + new Vector2(cornerDist, cornerDist));
            cornerPositions.Add(position + new Vector2(background.Texture.Size.X, 0) + new Vector2(-cornerDist, cornerDist));
            cornerPositions.Add(position + new Vector2(0, background.Texture.Size.Y) + new Vector2(cornerDist, -cornerDist));
            cornerPositions.Add(position + new Vector2(background.Texture.Size.X, background.Texture.Size.Y) + new Vector2(-cornerDist, -cornerDist));

            SetNewLevel(_button, _rating);
        }

        public void Update(float deltaTime, Vector2i currentScreenPosition)
        {
            // updates highlighted
            if ((currentScreenPosition.Y == 1 && highlighted) || (currentScreenPosition.Y == 0 && !highlighted))
            {
                highlighted = !highlighted;
                SetAllAphas((highlighted) ? 100 : 50);
            }
        }

        public void Draw(RenderWindow win)
        {
            foreach (RectangleShape rec in lines)
                win.Draw(rec);
            win.Draw(background);
            win.Draw(screenShot);
            levelName.Draw(win, RenderStates.Default);
            win.Draw(medal);
        }

        public void SetNewLevel(LevelSelectButton _button, ManageStars.Rating _rating)
        {
            button = _button;
            rating = _rating;
            
            levelName = new SuperText("Level " + (button.buttonLevel + 1), font, 0.2f);
            levelName.CharacterSize = 50;
            levelName.Position = position + new Vector2(23, 205);

            screenShot.Texture = AssetManager.GetScreenShot(button.buttonLevel);

            lines = new List<RectangleShape>();
            foreach (Vector2 v in cornerPositions)
            {
                lines.Add(GenerateLine(v, button.position, 2));
            }

            switch(rating)
            {
                case ManageStars.Rating.Fail:
                    SetMedal(AssetManager.GetTexture(AssetManager.TextureName.BotBronze));
                    break;
                case ManageStars.Rating.Bronze:
                    SetMedal(AssetManager.GetTexture(AssetManager.TextureName.BotBronze));
                    break;
                case ManageStars.Rating.Silver:
                    SetMedal(AssetManager.GetTexture(AssetManager.TextureName.BotSilver));
                    break;
                case ManageStars.Rating.Gold:
                    SetMedal(AssetManager.GetTexture(AssetManager.TextureName.BotGold));
                    break;
                case ManageStars.Rating.Skipped:
                    SetMedal(AssetManager.GetTexture(AssetManager.TextureName.SkipMedal));
                    break; 
            }
            if (rating == ManageStars.Rating.Fail)
                medal.Color = new Color(20, 20, 20);
            else
                medal.Color = new Color(255, 255, 255);
            SetAllAphas(highlighted? 100 : 50);
        }

        void SetMedal(Texture text)
        {
            medal.Texture = text;
            medal.Position = position + new Vector2(270, 205);
        }

        private void SetAllAphas(float percentage)
        {
            foreach(RectangleShape rec in lines)
                GraphicHelper.SetAlpha((byte)(175* percentage / 100), rec);
            GraphicHelper.SetAlpha((byte)(255 * percentage / 100), background);
            GraphicHelper.SetAlpha((byte)(255 * percentage / 100), screenShot);
            GraphicHelper.SetAlpha((byte)(255 * percentage / 100), levelName);
        }

        public int GetInfoButtonLevel()
        {
            return button.buttonLevel;
        }

        public RectangleShape GenerateLine(Vector2f start, Vector2f end, int thickness)
        {
            RectangleShape result = new RectangleShape(new Vector2(Vector2.distance(start, end), thickness));
            result.Origin = new Vector2f(result.Size.X * 0.5f, result.Size.Y * 0.5f);
            result.Position = new Vector2f((start.X + end.X) * 0.5f, (start.Y + end.Y) * 0.5f);
            Vector2 help = end - start;
            float rotation = (Vector2.angleBetween(help, new Vector2(1, 0)) * Helper.RadianToDegree);
            if (start.Y > end.Y)
                rotation *= -1;

            result.Rotation = rotation;
            result.FillColor = defaultLineColor;
            return result;
        }

    }
}