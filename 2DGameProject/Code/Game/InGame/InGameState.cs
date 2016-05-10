using System;
using SFML;
using SFML.Graphics;
using SFML.Window;

namespace GameProject2D
{
    class InGameState : IGameState
    {
        Player player;
        
        public InGameState()
        {
            player = new Player(new Vector2f(100F, 100F));
            
        }
       
        public GameState Update(float deltaTime)
        {
            player.update(deltaTime);
            return GameState.InGame;
        }

        public void Draw(RenderWindow win, View view, float deltaTime)
        {
            view.Center = player.
            player.draw(win, view);
        }

        public void DrawGUI(GUI gui, float deltaTime)
        {
        }

       
    }
}
