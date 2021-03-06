﻿using System;
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
        Sprite background = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.MainMenuBackground));


        public TutorialState()
        {
            background.Color = new Color(255,255,255,80);

            Sprites.Add(new AnimatedSprite(AssetManager.GetTexture(AssetManager.TextureName.TutorialMove), 0.2F, 8));
            Sprites.Add(new AnimatedSprite(AssetManager.GetTexture(AssetManager.TextureName.TutorialReset), 0.2F, 2));
            Sprites.Add(new AnimatedSprite(AssetManager.GetTexture(AssetManager.TextureName.TutorialGhostSpawn), 0.2F, 4));
            Sprites.Add(new AnimatedSprite(AssetManager.GetTexture(AssetManager.TextureName.TutorialGhostMove), 0.2F, 8));
            Sprites.Add(new AnimatedSprite(AssetManager.GetTexture(AssetManager.TextureName.TutorialSpawnBot), 0.2F, 3));
            Sprites.Add(new AnimatedSprite(AssetManager.GetTexture(AssetManager.TextureName.TutorialSwitch), 0.2F, 4));
            Sprites.Add(new AnimatedSprite(AssetManager.GetTexture(AssetManager.TextureName.TutorialDeleteBot), 0.2F, 2));

            Texts.Add(new SuperText("Move", FontLoader.Instance.LoadFont("Assets/Fonts/fixedsys.ttf"), 0.1F));
            Texts.Add(new SuperText("Reset", FontLoader.Instance.LoadFont("Assets/Fonts/fixedsys.ttf"), 0.1F));
            Texts.Add(new SuperText("Scout", FontLoader.Instance.LoadFont("Assets/Fonts/fixedsys.ttf"), 0.1F));
            Texts.Add(new SuperText("Move\nScout", FontLoader.Instance.LoadFont("Assets/Fonts/fixedsys.ttf"), 0.1F));
            Texts.Add(new SuperText("Spawn\nBot", FontLoader.Instance.LoadFont("Assets/Fonts/fixedsys.ttf"), 0.1F));
            Texts.Add(new SuperText("Switch", FontLoader.Instance.LoadFont("Assets/Fonts/fixedsys.ttf"), 0.1F));
            Texts.Add(new SuperText("Delete", FontLoader.Instance.LoadFont("Assets/Fonts/fixedsys.ttf"), 0.1F));

            Sprites[0].Position = new Vector2f(100, 70);
            for (int i = 1; i < Sprites.Count; i++)
            {
                if(i == 2)
                    Sprites[i].Position = new Vector2f(500, 70);
                else
                    Sprites[i].Position = Sprites[i - 1].Position + new Vector2f(0, Sprites[i - 1].TextureRect.Height + 20);
            }

            for (int i = 0; i < Texts.Count; i++)
            {
                Texts[i].Position = Sprites[i].Position + new Vector2f(130, 10);
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
            gui.Draw(background);

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

