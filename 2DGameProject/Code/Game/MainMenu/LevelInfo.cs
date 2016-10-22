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

            starList = new List<Sprite>();
            starList.Add(new Sprite(AssetManager.GetTexture(AssetManager.TextureName.BotBronze)));
            starList[0].Position = new Vector2f(50, 150);
            starList.Add(new Sprite(AssetManager.GetTexture(AssetManager.TextureName.BotSilver)));
            starList[1].Position = new Vector2f(130, 150);
            starList.Add(new Sprite(AssetManager.GetTexture(AssetManager.TextureName.BotGold)));
            starList[2].Position = new Vector2f(210, 150);

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
            lines = new List<RectangleShape>();
            foreach (Vector2 v in cornerPositions)
            {
                lines.Add(GenerateLine(v, button.position, 2));
            }

            for (int i = 0; i <= 2; i++)
            {
                Console.WriteLine("rating: " + rating + " i " + i);
                if (i < (int)rating)
                {
                    starList[i].Color = new Color(255, 255, 255);
                    GraphicHelper.SetAlpha(255, starList[i]);
                }
                else
                {
                    starList[i].Color = new Color(20, 20, 20);
                    GraphicHelper.SetAlpha(120, starList[i]);
                }
            }
        }

        private void SetAllAphas(float percentage)
        {
            foreach(RectangleShape rec in lines)
                GraphicHelper.SetAlpha((byte)(255* percentage / 100), rec);
            GraphicHelper.SetAlpha((byte)(255 * percentage / 100), background);
            GraphicHelper.SetAlpha((byte)(255 * percentage / 100), levelName);
            for (int i = 0; i <= 2; i++)
            {
                if (i < (int)rating)
                {
                    starList[i].Color = new Color(255, 255, 255);
                    GraphicHelper.SetAlpha(255, starList[i]);
                }
                else
                {
                    starList[i].Color = new Color(20, 20, 20);
                    GraphicHelper.SetAlpha(120, starList[i]);
                }
            }
            foreach(Sprite star in starList)
            {
                GraphicHelper.SetAlpha((byte)(star.Color.A * percentage / 100), star);
            }

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
            Console.WriteLine(" rotation: " + (Vector2.angleBetween(new Vector2(1, 0), new Vector2(0, 1)) / Math.PI * 180));
            result.Rotation = (float)(Vector2.angleBetween(help, new Vector2(1, 0)) / Math.PI * 180);
            result.FillColor = defaultLineColor;
            return result;
        }

    }
}