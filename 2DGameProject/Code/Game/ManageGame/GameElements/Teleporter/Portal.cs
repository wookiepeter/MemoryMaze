using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

namespace MemoryMaze
{
    class Portal : Transporter
    {
        public Portal(Vector2i _entrance, Vector2i _exit, Map map)
        {
            entrance = _entrance;
            drawEntrance = true;
            exit = _exit;
            drawExit = true;

            entranceSprite = new AnimatedSprite(AssetManager.GetTexture(AssetManager.TextureName.Teleporter), AnimationSecondsPerFrame, 8);
            exitSprite = new AnimatedSprite(AssetManager.GetTexture(AssetManager.TextureName.TeleporterExitOnly), AnimationSecondsPerFrame, 8);
            entranceParticleSprite = new AnimatedSprite(AssetManager.GetTexture(AssetManager.TextureName.ParticlesAnimated), particleAnimationSecondsPerFrame, 13);
            exitParticleSprite = new AnimatedSprite(AssetManager.GetTexture(AssetManager.TextureName.ParticlesOutgoingAnimated), particleAnimationSecondsPerFrame, 13);

            entranceExactPosition = new Vector2f(entrance.X * map.sizePerCell, entrance.Y * map.sizePerCell);
            entranceSprite.Scale = new Vector2f((float)map.sizePerCell / (float)entranceSprite.spriteSize.X, (float)map.sizePerCell / (float)entranceSprite.spriteSize.Y);
            entranceParticleSprite.Scale = entranceSprite.Scale;

            exitExactPosition = new Vector2f(exit.X * map.sizePerCell, exit.Y * map.sizePerCell);
            exitSprite.Scale = new Vector2f((float)map.sizePerCell / (float)exitSprite.spriteSize.X, (float)map.sizePerCell / (float)exitSprite.spriteSize.Y);
            exitParticleSprite.Scale = exitSprite.Scale;

        }

        private Portal(Portal _portal)
        {
            entrance = _portal.entrance;
            drawEntrance = _portal.drawEntrance;
            exit = _portal.exit;
            drawExit = _portal.drawExit;

            entranceSprite = _portal.entranceSprite;
            exitSprite = _portal.exitSprite;
            entranceParticleSprite = _portal.entranceParticleSprite;
            exitParticleSprite = _portal.exitParticleSprite;

            entranceExactPosition = _portal.entranceExactPosition;
            entranceSprite.Scale = _portal.entranceSprite.Scale;
            entranceParticleSprite.Scale = _portal.entranceParticleSprite.Scale;

            exitExactPosition = _portal.exitExactPosition;
            exitSprite.Scale = _portal.exitSprite.Scale;
            exitParticleSprite.Scale = _portal.exitParticleSprite.Scale;
        }

        public override Transporter Copy()
        {
            return new Portal(this);
        }

        public override void Update(Player player, float deltaTime)
        {
            bool entranceInList = player.getListWithPlayerAndBlueBot().Contains(entrance);
            if (entranceInList)
            {
                if (player.mapPosition.Equals(entrance))
                {
                    player.InitializeTeleport(this, false, exit);
                    Console.WriteLine("i did get initialized");
                    player.mapPosition = exit;
                }
                else
                {
                    player.InitializeTeleport(this, true, exit);
                    player.setBlueBotPosition(exit);
                }
            }

            entranceSprite.UpdateFrame(deltaTime);
            exitSprite.UpdateFrame(deltaTime);
            entranceParticleSprite.UpdateFrame(deltaTime);
            exitParticleSprite.UpdateFrame(deltaTime);
        }

        public override void Draw(RenderTexture win, View view, Vector2f relViewDis)
        {
            if (drawEntrance)
            {
                entranceSprite.Position = entranceExactPosition + relViewDis;
                win.Draw(entranceSprite);
                entranceParticleSprite.Position = entranceExactPosition + relViewDis;
                win.Draw(entranceParticleSprite);
            }

            if (drawExit)
            {
                exitSprite.Position = exitExactPosition + relViewDis;
                win.Draw(exitSprite);
                exitParticleSprite.Position = exitExactPosition + relViewDis;
                win.Draw(exitParticleSprite);
            }
        }
    }
}
