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
            player = new Player(new Vector2f(10F, 10F));
        }

        public GameState Update(float deltaTime)
        {
            player.update(deltaTime);
            return GameState.InGame;
        }

        public void Draw(RenderWindow win, View view, float deltaTime)
        {
            player.draw(win, view);
        }

        public void DrawGUI(GUI gui, float deltaTime)
        {
        }

        public Player getPlayer()
        {
            return player;
        }
    }
}
