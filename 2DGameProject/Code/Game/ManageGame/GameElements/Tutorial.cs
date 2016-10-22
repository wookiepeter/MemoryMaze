using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

namespace MemoryMaze
{
    class Tutorial
    {
        Sprite background;
        Vector2 position;
        Vector2 textPosition;
        SuperText superText;
        float duration;
        float currentTime;
        public int index;
        public int tutorialIndex;
        public bool shown;
        public bool iShouldKillMyself;
        Font font = new Font("Assets/Fonts/calibri.ttf");

        public Tutorial(Texture _background, String _text, Vector2 _position, float _duration, int _index, int _tutIndex)
        {
            background = new Sprite(_background);
            position = _position;
            textPosition = position + new Vector2(25, 25);
            background.Position = position;
            duration = _duration;
            currentTime = 0;
            index = _index;
            tutorialIndex = _tutIndex;
            iShouldKillMyself = false;
            superText = new SuperText(_text, font, 0.05f);
        }
        
        /// <summary>
        /// this actually activates the Tutorial window
        /// </summary>
        public void ActivateSecretPowers()
        {
            Console.WriteLine("Activate supersecret Powers");
            currentTime = duration;
            shown = true;
        }

        public void Update(float deltaTime)
        {
            if (currentTime >= 0)
            {
                superText.Update(deltaTime);
                currentTime -= deltaTime;
                if (currentTime < (duration / 2))
                {
                    GraphicHelper.SetAlpha((byte) (255 * currentTime / (duration * 0.5f)), background);
                    GraphicHelper.SetAlpha((byte)(255 * currentTime / (duration * 0.5f)), superText);
                }
            }
            else
            {
                Console.WriteLine("Too soon");
                iShouldKillMyself = true;
            }
        }

        public void Draw(RenderTexture win, View view, Vector2f relVieDif)
        {
            if (currentTime > 0)
            {
                background.Position = position;
                win.Draw(background);
                superText.Position = textPosition;
                superText.Draw(win, RenderStates.Default);
            }
        }
    }
}
