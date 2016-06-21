using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML;
using SFML.Graphics;
using SFML.Window;

namespace MemoryMaze
{

    public class Enemy
    {
        RectangleShape sprite;
        Vector2i mapPosition;
        Vector2f size { get { return sprite.Size; } set { sprite.Size = value; } }
        public enum EnemyKind
        {
            FIERWALL,
            ANTIVIRUS
        }
        private Texture enemyTexture;
        private void getTexture()
        {
            switch (enemyType)
            {
                case EnemyKind.ANTIVIRUS:
                    enemyTexture = new Texture("Assets/Textures/placeholder/antivirus.png");
                    break;

                case EnemyKind.FIERWALL:
                    enemyTexture = new Texture("Assets/Textures/placeholder/fIERWALL.png");
                    break;

            }
        }
        private EnemyKind enemyType;
        public EnemyKind getType()
        {
            return enemyType;
        }
        public Enemy(Vector2i position, Map map, EnemyKind enemyType)
        {
            this.sprite = new RectangleShape(new Vector2f(1F, 1F));
            this.sprite.Size = new Vector2f(map.sizePerCell * 0.8F, map.sizePerCell* 0.8F);
            this.mapPosition = position;
            this.enemyType = enemyType;
            getTexture();
            this.sprite.Texture = enemyTexture;
            updateSpritePosition(map);
        }
        public void draw(RenderWindow win, View view)
        {
            win.Draw(sprite);
        }

        void updateSpritePosition(Map map)
        {
            this.sprite.Position = new Vector2f(mapPosition.X * map.sizePerCell + map.sizePerCell * 0.1F, mapPosition.Y * map.sizePerCell + map.sizePerCell * 0.1F);
        }

    }
}