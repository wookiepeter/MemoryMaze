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
        AnimatedSprite animSprite;
        Vector2 position;
        Vector2 textPosition;
        SuperText superText;
        byte maxOpacity;
        float duration;
        float currentTime;
        public int index;
        public int tutorialIndex;
        public bool shown;
        public bool iShouldKillMyself;
        Font font = new Font("Assets/Fonts/fixedsys.ttf");

        public Tutorial(AnimatedSprite _animSprite, String _text, Vector2 _textPosition, Vector2 _position, float _duration, int _index, int _tutIndex)
        {
            animSprite = _animSprite;
            position = _position;
            animSprite.Position = position;
            textPosition = _textPosition;
            duration = _duration;
            currentTime = 0;
            index = _index;
            tutorialIndex = _tutIndex;
            iShouldKillMyself = false;
            superText = new SuperText(_text, font, 0.05f);
            superText.Position = textPosition;
            superText.CharacterSize = 48;

            maxOpacity = 200;
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
                if(currentTime > (duration * 0.9F))
                {
                    GraphicHelper.SetAlpha((byte)(maxOpacity * (1F - (currentTime / duration)) / 0.1F), animSprite);
                    GraphicHelper.SetAlpha((byte)((maxOpacity + 20) * (1F - (currentTime / duration)) / 0.1F), superText);
                }
                animSprite.UpdateFrame(deltaTime);
                superText.Update(deltaTime);
                currentTime -= deltaTime;
                if (currentTime < (duration / 2))
                {
                    GraphicHelper.SetAlpha((byte) (maxOpacity * currentTime / (duration * 0.5f)), animSprite);
                    GraphicHelper.SetAlpha((byte)((maxOpacity + 20) * currentTime / (duration * 0.5f)), superText);
                }
            }
            else
            {
                iShouldKillMyself = true;
            }
        }

        public void Draw(RenderTexture win, View view, Vector2f relVieDif)
        {
            if (currentTime > 0)
            {
                animSprite.Position = position;
                win.Draw(animSprite);
                superText.Position = textPosition;
                superText.Draw(win, RenderStates.Default);
            }
        }
    }
}
