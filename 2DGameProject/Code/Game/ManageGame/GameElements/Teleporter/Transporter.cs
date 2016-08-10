﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

namespace MemoryMaze
{
    abstract class Transporter
    {
        protected Vector2i entrance;
        protected Vector2i exit;

        protected Sprite entranceSprite;
        protected Sprite exitSprite;

        protected Vector2f entranceExactPosition;
        protected Vector2f exitExactPosition;

        private Transporter(Transporter _transporter) { }

        public Transporter() { }

        abstract public Transporter Copy();

        abstract public void Update(Player player, float deltaTime);

        abstract public void Draw(RenderTexture win, View view, Vector2f relViewDis);
    }
}