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
            exit = _exit;

            entranceSprite = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.DasC));
            exitSprite = new Sprite(AssetManager.GetTexture(AssetManager.TextureName.DasF));

            entranceExactPosition = new Vector2f(entrance.X * map.sizePerCell + map.sizePerCell * 0.25f, entrance.Y * map.sizePerCell + map.sizePerCell * 0.25f);
            entranceSprite.Scale = new Vector2f((float)map.sizePerCell * 0.5f / (float)entranceSprite.Texture.Size.X, (float)map.sizePerCell * 0.5f / (float)entranceSprite.Texture.Size.Y);

            exitExactPosition = new Vector2f(exit.X * map.sizePerCell + map.sizePerCell * 0.25f, exit.Y * map.sizePerCell + map.sizePerCell * 0.25f);
            exitSprite.Scale = new Vector2f((float)map.sizePerCell * 0.5f / (float)exitSprite.Texture.Size.X, (float)map.sizePerCell * 0.5f / (float)exitSprite.Texture.Size.Y);


        }

        private Portal(Portal _portal)
        {
            entrance = _portal.entrance;
            exit = _portal.exit;

            entranceSprite = _portal.entranceSprite;
            exitSprite = _portal.exitSprite;

            entranceExactPosition = _portal.entranceExactPosition;
            entranceSprite.Scale = _portal.entranceSprite.Scale;

            exitExactPosition = _portal.exitExactPosition;
            exitSprite.Scale = _portal.exitSprite.Scale;
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
                    player.mapPosition = exit;
                }
                else
                {
                    player.setBlueBotPosition(exit);
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
