using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using System.Diagnostics;

namespace MemoryMaze
{
    internal class TutorialState : IGameState
    {
        List<AnimatedSprite> Sprites = new List<global::AnimatedSprite>();
        List<SuperText> Texts = new List<SuperText>();


        public TutorialState()
        {
            Sprites.Add(new AnimatedSprite(AssetManager.GetTexture(AssetManager.TextureName.TutorialMove), 0.2F, 8));
            Sprites.Add(new AnimatedSprite(AssetManager.GetTexture(AssetManager.TextureName.TutorialReset), 0.2F, 2));
            Sprites.Add(new AnimatedSprite(AssetManager.GetTexture(AssetManager.TextureName.TutorialGhostSpawn), 0.2F, 4));
            Sprites.Add(new AnimatedSprite(AssetManager.GetTexture(AssetManager.TextureName.TutorialGhostMove), 0.2F, 8));
            Sprites.Add(new AnimatedSprite(AssetManager.GetTexture(AssetManager.TextureName.TutorialSpawnBot), 0.2F, 3));
            Sprites.Add(new AnimatedSprite(AssetManager.GetTexture(AssetManager.TextureName.TutorialSwitch), 0.2F, 4));
            Sprites.Add(new AnimatedSprite(AssetManager.GetTexture(AssetManager.TextureName.TutorialDeleteBot), 0.2F, 2));

            Sprites[0].Position = new Vector2f(100, 100);
            for (int i = 1; i < Sprites.Count; i++)
            {
                Sprites[i].Position = Sprites[i-1].Position + new Vector2f(0, Sprites[i - 1].TextureRect.Height + 20);
            }

            Texts.Add(new SuperText("Move", new Font("Assets/Fonts/fixedsys.ttf"), 0.1F));
            Texts.Add(new SuperText("Reset", new Font("Assets/Fonts/fixedsys.ttf"), 0.1F));
            Texts.Add(new SuperText("Scout", new Font("Assets/Fonts/fixedsys.ttf"), 0.1F));
            Texts.Add(new SuperText("Move\nScout", new Font("Assets/Fonts/fixedsys.ttf"), 0.1F));
            Texts.Add(new SuperText("Spawn\nBot", new Font("Assets/Fonts/fixedsys.ttf"), 0.1F));
            Texts.Add(new SuperText("Switch", new Font("Assets/Fonts/fixedsys.ttf"), 0.1F));
            Texts.Add(new SuperText("Delete", new Font("Assets/Fonts/fixedsys.ttf"), 0.1F));

            for(int i = 0; i < Texts.Count; i++)
            {
                Texts[i].Position = Sprites[i].Position + new Vector2f(30, 10);
            }
        }

        public void LoadContent()
        {

        }


        public GameState Update(RenderWindow win, float deltaTime)
        {
            if (KeyboardInputManager.Downward(Keyboard.Key.Escape))
            {
                return GameState.MainMenu;
            }
            return GameState.Tutorial;
        }

        public void Draw(RenderWindow win, View view, float deltaTime)
        {
            
        }

        public void DrawGUI(GUI gui, float deltaTime)
        {
            foreach (AnimatedSprite sprite in Sprites)
            {
                sprite.UpdateFrame(deltaTime);
                gui.Draw(sprite);
            }
            foreach (SuperText text in Texts)
            {
                text.Update(deltaTime);
                gui.Draw(text);
            }
        }
    }
}

