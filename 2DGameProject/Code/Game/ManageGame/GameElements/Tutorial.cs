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
        List<Sprite> lines;
        SuperText sText;
        float duration;
        float currentTime;

        public Tutorial(Texture _background, String _text, Vector2 _position, float _duration)
        {
            background = new Sprite(_background);
            position = _position;
            background.Position = position;
            duration = _duration;


        }


    }
}
