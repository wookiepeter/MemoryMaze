using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

namespace MemoryMaze
{
    public class GraphicHelper
    {
        public GraphicHelper()
        {

        }

        public static void SetAlpha(byte alpha, Text text)
        {
            Color help = text.Color;
            help.A = alpha;
            text.Color = help;
        }

        public static void SetAlpha(byte alpha, Sprite sprite)
        {
            Color help = sprite.Color;
            help.A = alpha;
            sprite.Color = help;
        }

        public static void SetAlpha(byte alpha, RectangleShape rec)
        {
            Color help = rec.FillColor;
            help.A = alpha;
            rec.FillColor = help;
        }

        public static void SetAlpha(byte alpha, SuperText superText)
        {
            Color help = superText.Color;
            help.A = alpha;
            superText.Color = help;
        }
    }
}
