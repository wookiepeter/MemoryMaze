using System;
using SFML;
using SFML.Graphics;
using SFML.Window;

namespace GameProject2D
{
    class InGameState : IGameState
    {
        Game game;

        public InGameState()
        {
            game = new Game();
        }
       
        public GameState Update(float deltaTime)
        {
            game.Update(deltaTime);
            return GameState.InGame;
        }

        public void Draw(RenderWindow win, View view, float deltaTime)
        {
            game.draw(win, view);            

        }

        public void DrawGUI(GUI gui, float deltaTime)
        {

        }
    }
}
