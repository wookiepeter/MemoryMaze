using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

namespace MemoryMaze
{
    public abstract class Transporter
    {
        public Vector2i entrance;
        public bool drawEntrance;
        public Vector2i exit;
        public bool drawExit;

        protected AnimatedSprite entranceSprite;
        protected AnimatedSprite exitSprite;
        protected AnimatedSprite entranceParticleSprite;
        protected AnimatedSprite exitParticleSprite;
        protected float AnimationSecondsPerFrame = 0.1F;
        protected float particleAnimationSecondsPerFrame = 0.12F;

        protected Vector2f entranceExactPosition;
        protected Vector2f exitExactPosition;

        private Transporter(Transporter _transporter) { }

        public Transporter() { }

        abstract public Transporter Copy();

        abstract public void Update(Player player, float deltaTime);

        abstract public void Draw(RenderTexture win, View view, Vector2f relViewDis);
    }
}
