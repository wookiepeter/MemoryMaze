using System;
using SFML;
using SFML.Graphics;
using SFML.Window;

namespace MemoryMaze
{
    class InGameState : IGameState
    {
        Game game;

        GameState nextGameState;

        // Render/Drawstuff
        bool isInitialized = false;
        RenderTexture multTexture;
        RenderTexture backgroundMult;

        Sprite overlay;

        RenderStates multState;
        RenderStates add;

        View _view;

        public InGameState(int id)
        {
            game = new Game(id);
        }
        
        public GameState Update(RenderWindow win, float deltaTime)
        {
            nextGameState = game.Update(deltaTime);

            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                game.SaveGame();
                return GameState.LoadLevelState;
            }
            return nextGameState;
        }

        public void Draw(RenderWindow win, View view, float deltaTime)
        {
            if(!isInitialized)
            {
                InitializeRenderShit(win);
                isInitialized = true;
            }

            //  backgroundMult.Display();
            if(_view == null)
            {
                _view = new View(win.GetView());
                backgroundMult.SetView(_view);
            }

            // Draws the Game.Draw on to backgroundMult
            backgroundMult.Clear(Color.Black);

            game.Draw(backgroundMult, _view, deltaTime);

            //win.SetView(view);
            // "buffers" backgroundMult
            backgroundMult.Display();
            
            // Draws background in win
            win.Draw(new Sprite(backgroundMult.Texture));

            // processes lightMask
            multTexture.Clear();
            multTexture.Draw(overlay, add);
            multTexture.Display();

            //Console.WriteLine(" view.Center " + view.Center + " multTexture.Center" + multTexture.GetView().Center + " win.Center " + win.GetView().Center + " multTexture.Center " + multTexture.GetView().Center);
            // resets view of window
            //win.SetView(_view);

            // multiplies lightMask with the Texture already in the window (because multstate)
            //win.Draw(new Sprite(multTexture.Texture), multState);
        }

        private void InitializeRenderShit(RenderWindow win)
        {
            multTexture = new RenderTexture(win.Size.X, win.Size.Y);
            backgroundMult = new RenderTexture(win.Size.X, win.Size.Y);

            overlay = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.Overlay));
            overlay.Scale = new Vector2f(1, 1);
            overlay.Position = new Vector2f((float)win.Size.X/2f - (float)overlay.TextureRect.Width/2f, 
                (float)win.Size.Y/2f - (float)overlay.TextureRect.Height/2f);

            multState = new RenderStates(BlendMode.Multiply);
            add = new RenderStates(BlendMode.Add);
        }

        public void DrawGUI(GUI gui, float deltaTime)
        {
            game.DrawGUI(gui, deltaTime);
        }
    }
}
