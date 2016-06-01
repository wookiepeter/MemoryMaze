using SFML.Graphics;

namespace MemoryMaze
{
    interface IGameState
    {
        GameState Update(float deltaTime);
        void Draw(RenderWindow win, View view, float deltaTime);
        void DrawGUI(GUI gui, float deltaTime);
    }
}
