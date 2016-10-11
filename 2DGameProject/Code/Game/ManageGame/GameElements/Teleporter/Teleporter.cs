﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

namespace MemoryMaze
{
    class Teleporter : Transporter
    {
        bool entranceDisabled;
        bool exitDisabled;

        public Teleporter(Vector2i _entrance, Vector2i _exit, Map map)
        {
            entrance = _entrance;
            exit = _exit;

            entranceSprite = new AnimatedSprite(AssetManager.GetTexture(AssetManager.TextureName.Teleporter), 0.1F, 13);
            exitSprite = new AnimatedSprite(AssetManager.GetTexture(AssetManager.TextureName.Teleporter), 0.1F, 13);

            //NEXTTIME beim nächsten mal können wir für sowas eine gemeinsam genutzte Methode in der Basisklasse erstellen
            entranceExactPosition = new Vector2f(entrance.X * map.sizePerCell, entrance.Y * map.sizePerCell);
            entranceSprite.Scale = new Vector2f((float)map.sizePerCell / (float)entranceSprite.spriteSize.X, (float)map.sizePerCell / (float)entranceSprite.spriteSize.Y);

            exitExactPosition = new Vector2f(exit.X * map.sizePerCell, exit.Y * map.sizePerCell);
            exitSprite.Scale = new Vector2f((float)map.sizePerCell / (float)exitSprite.spriteSize.X, (float)map.sizePerCell / (float)exitSprite.spriteSize.Y);

            entranceDisabled  = false;
            exitDisabled = false;
        }

        private Teleporter(Teleporter _transporter)
        {
            entrance = _transporter.entrance;
            exit = _transporter.exit;

            entranceSprite = _transporter.entranceSprite;
            exitSprite = _transporter.exitSprite;

            entranceExactPosition = _transporter.entranceExactPosition;
            entranceSprite.Scale = _transporter.entranceSprite.Scale;

            exitExactPosition = _transporter.exitExactPosition;
            exitSprite.Scale = _transporter.exitSprite.Scale;

            entranceDisabled = _transporter.entranceDisabled;
            exitDisabled = _transporter.exitDisabled;
        }

        public override Transporter Copy()
        {
            return new Teleporter(this);
        }

        public override void Update(Player player, float deltaTime)
        {
            ((AnimatedSprite)entranceSprite).UpdateFrame(deltaTime);
            ((AnimatedSprite)exitSprite).UpdateFrame(deltaTime);


            bool entranceInList = player.getListWithPlayerAndBlueBot().Contains(entrance);
            bool exitInList = player.getListWithPlayerAndBlueBot().Contains(exit);
            if (entranceInList || exitInList)
            {
                if (entranceInList && !entranceDisabled)
                {
                    if (player.mapPosition.Equals(entrance))
                    {
                        player.mapPosition = exit;
                        exitDisabled = true;
                    }
                    else
                    {
                        player.setBlueBotPosition(exit);
                        exitDisabled = true;
                    }
                }
                else if (exitInList && !exitDisabled)
                {
                    if (player.mapPosition.Equals(exit))
                    {
                        player.mapPosition = entrance;
                        entranceDisabled = true;
                    }
                    else
                    {
                        player.setBlueBotPosition(entrance);
                        entranceDisabled = true;
                    }
                }
            }
            else
            {
                if (!entranceInList && entranceDisabled)
                {
                    entranceDisabled = false;
                }
                if (!exitInList == exitDisabled)
                {
                    exitDisabled = false;
                }
            }
        }

        public override void Draw(RenderTexture win, View view, Vector2f relViewDis)
        {
            entranceSprite.Position = entranceExactPosition + relViewDis;
            win.Draw(entranceSprite);

            exitSprite.Position = exitExactPosition + relViewDis;
            win.Draw(exitSprite);
        }
    }
}
