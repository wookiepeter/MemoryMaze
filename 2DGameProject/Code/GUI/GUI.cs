using SFML.Graphics;
using System;
using System.Collections.Generic;

namespace MemoryMaze
{
    class GUI
    {
        RenderWindow win;
        public View view { get { return win.GetView(); } }
        
        public GUI(RenderWindow win, View view)
        {
            this.win = win;
        }

        public void Draw<T>(T transformDrawable) where T : Transformable, Drawable
        {
            Vector2 originalScale = transformDrawable.Scale;
            Vector2 originalPosition = transformDrawable.Position;

            // modify drawable, to fit it in the gui
            float viewScale = (float)view.Size.X / win.Size.X;

            transformDrawable.Scale *= viewScale;
            transformDrawable.Position = view.Center - view.Size / 2F + transformDrawable.Position * viewScale;

            win.Draw(transformDrawable);

            // reset to originalValues
            transformDrawable.Scale = originalScale;
            transformDrawable.Position = originalPosition;
        }
    }
    
}