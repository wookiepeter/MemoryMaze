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

        public InGameState()
        {
            game = new Game();
        }
       
        public GameState Update(float deltaTime)
        {
            nextGameState = game.Update(deltaTime);
            return nextGameState;
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
