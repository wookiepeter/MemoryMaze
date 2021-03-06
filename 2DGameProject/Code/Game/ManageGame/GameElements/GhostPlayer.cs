﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML;
using SFML.Graphics;
using SFML.Window;


namespace MemoryMaze
{
    public class GhostPlayer
    {
        
        public AnimatedSprite sprite { get; private set; }
        RedBot redBot;
        BlueBot blueBot;
        GreenBot greenBot;
        bool iserstellt;
        public int counter { get;  set; }
        public Vector2i mapPosition { get; private set; }
        Vector2f size { get { return new Vector2f(sprite.spriteSize.X, sprite.spriteSize.Y); } }

        public GhostPlayer(Vector2i position, Map map)
        {
            counter = 10;//ANzahl der Schritte
            iserstellt = false;
            this.sprite = new AnimatedSprite(AssetManager.GetTexture(AssetManager.TextureName.PlayerGhostAnimated), 0.1F, 4, new Vector2i(64,64), new Vector2i(0,0));
       
            this.mapPosition = position;
            UpdateSpritePosition(map);
        }
        public void Update(float deltaTime, Map map, Player player)//List<Bot> botsList, bool b_redbot, bool b_bluebot, bool b_greenbot)
        {
            Vector2i move = GetMove();
            if (map.CellIsWalkable(mapPosition + move))
            {
                if(move.X != 0 || move.Y != 0) //TOdo Matthis bearbeiten
                    counter--;
                mapPosition = mapPosition + move;
                //Logger.Instance.Write("mapPosX: " + mapPosition.X + "mapPosY" + mapPosition.Y, Logger.level.Info);
            }
            UpdateSpritePosition(map);
            CreateBot(map, player); //player.botList, player.redbot, player.bluebot, player.greenbot);

            sprite.UpdateFrame(deltaTime);
        }
    
        public void Draw(RenderTexture win, View view, Vector2f relViewDis)
        {
            view.Center = Vector2.lerp(view.Center, sprite.Position, 0.01F);
            sprite.Position = sprite.Position + relViewDis;
            if (!iserstellt)
                win.Draw(sprite);
         /*   else
            {
                redBot.Render(win, view);
            }*/
        }
        public void CreateBot()
        {
            MusicManager.PlaySound(AssetManager.SoundName.CreateBot);
        }


        void  CreateBot(Map map, Player player) //List<Bot> botsList, bool b_redbot, bool b_bluebot, bool b_greenbot)
        {
            if (KeyboardInputManager.IsPressed(Keyboard.Key.Num2) && (!iserstellt) && (!player.redbot)) //ToDo: Bedingungen zum erstelelen 
            {
                if (player.redItemCounter > 0)
                {
                    redBot = new RedBot(mapPosition, map);
                    player.botList.Add(redBot);
                    player.controllid = 1;
                    counter = 0;
                    player.redItemCounter--;
                    CreateBot();

                }


            }
            if (KeyboardInputManager.IsPressed(Keyboard.Key.Num2) && (!iserstellt) && (player.redbot))
            {

                if (player.redItemCounter > 0)
                {
                    player.scoreCounter -= player.botList.Find(b => b.id == 1).counter;
                    player.botList.Remove(player.botList.Find(b => b.id == 1));
                    redBot = new RedBot(mapPosition, map);
                    player.botList.Add(redBot);
                    player.controllid = 1;
                    counter = 0;
                    player.redItemCounter--;
                    CreateBot();
                }

            }

            if (KeyboardInputManager.IsPressed(Keyboard.Key.Num4) && (!iserstellt) && (!player.bluebot)) //ToDo: Bedingungen zum erstelelen
            {
                if(player.blueItemCounter > 0)
                {
                    blueBot = new BlueBot(mapPosition, map);
                    player.botList.Add(blueBot);
                    player.controllid = 2;
                    counter = 0;
                    player.blueItemCounter--;
                    CreateBot();
                }

            }
            if (KeyboardInputManager.IsPressed(Keyboard.Key.Num4) && (!iserstellt) && (player.bluebot))
            {
                if (player.blueItemCounter > 0)
                {
                    player.scoreCounter -= player.botList.Find(b => b.id == 2).counter;
                    player.botList.Remove(player.botList.Find(b => b.id == 2));

                    blueBot = new BlueBot(mapPosition, map);
                    player.botList.Add(blueBot);
                    player.controllid = 2;
                    counter = 0;
                    player.blueItemCounter--;
                    CreateBot();
                }

            }
            if (KeyboardInputManager.IsPressed(Keyboard.Key.Num3) && (!iserstellt) && (!player.greenbot)) //ToDo: Bedingungen zum erstelelen
            {
                if(player.greenItemCounter > 0)
                {
                    greenBot = new GreenBot(mapPosition, map);
                    player.botList.Add(greenBot);
                    player.controllid = 3;
                    counter = 0;
                    player.greenItemCounter--;
                    CreateBot();
                }

            }
            if (KeyboardInputManager.IsPressed(Keyboard.Key.Num3) && (!iserstellt) && (player.greenbot))
            {
                if(player.greenItemCounter > 0)
                {
                    player.scoreCounter -= player.botList.Find(b => b.id == 3).counter;
                    player.botList.Remove(player.botList.Find(b => b.id == 3));
                    greenBot = new GreenBot(mapPosition, map);
                    player.botList.Add(greenBot);
                    player.controllid = 3;
                    counter = 0;
                    player.greenItemCounter--;
                    CreateBot();
                }

            }
        }
        Vector2i GetMove()
        {
            Vector2i move = new Vector2i(0, 0);
            if (KeyboardInputManager.Downward(Keyboard.Key.W) || KeyboardInputManager.Downward(Keyboard.Key.Up))
            {
                move.Y = -1;
   
            }
            else if (KeyboardInputManager.Downward(Keyboard.Key.S) || KeyboardInputManager.Downward(Keyboard.Key.Down))
            {
                move.Y = 1;

            }
            else if (KeyboardInputManager.Downward(Keyboard.Key.A) || KeyboardInputManager.Downward(Keyboard.Key.Left))
            {
                move.X = -1;

            }
            else if (KeyboardInputManager.Downward(Keyboard.Key.D) || KeyboardInputManager.Downward(Keyboard.Key.Right))
            {
                move.X = 1;

            }
            //Logger.Instance.Write("moveX: " + move.X + "moveY" + move.Y, Logger.level.Info);
            
            return move;
        }
        Vector2i MakeNextPos(Vector2i nextPos, Map map)
        {
            nextPos.X = (nextPos.X > map.mapSizeX) ? map.mapSizeX : nextPos.X;
            nextPos.Y = (nextPos.Y > map.mapSizeY) ? map.mapSizeY : nextPos.Y;

            nextPos.X = (nextPos.X < 0) ? 0 : nextPos.X;
            nextPos.Y = (nextPos.Y < 0) ? 0 : nextPos.Y;
            return nextPos;
        }

        void UpdateSpritePosition(Map map)
        {
            this.sprite.Position = new Vector2f(mapPosition.X * map.GetSizePerCell(), mapPosition.Y * map.GetSizePerCell());
        }

        Vector2f GetSpritePosition(Map map)
        {
            return new Vector2f(mapPosition.X * map.GetSizePerCell(), mapPosition.Y * map.GetSizePerCell());
        }
        public int GetCount() { return counter; }
    }
}
