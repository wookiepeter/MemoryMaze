using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;

public class AnimatedSprite : Sprite
{
    public float secondsPerFrame { get; private set; }
    public Vector2i spriteSize { get; private set; }
    public int frameCount { get; private set; }
    Vector2i upperLeftCorner;

    float? animationTime;

    public AnimatedSprite(Texture spriteSheet, float secondsPerFrame, int frameCount, Vector2i spriteSize)
        : this(spriteSheet, secondsPerFrame, frameCount, spriteSize, new Vector2i(0, 0))
    {
    }

    public AnimatedSprite(Texture spriteSheet, float secondsPerFrame, int frameCount, Vector2i spriteSize, Vector2i upperLeftCorner)
        : base(spriteSheet)
    {
        this.secondsPerFrame = secondsPerFrame;
        this.frameCount = frameCount;
        this.spriteSize = spriteSize;
        this.upperLeftCorner = upperLeftCorner;
        animationTime = 0F;
    }

    /// <summary>start or restart the animation</summary>
    public void RestartAnimation(GameTime currentTime)
    {
        animationTime = 0F;
    }

    /// <summary>start or restart the animation</summary>
    public void StopAnimation()
    {
        animationTime = null;
    }

    public Sprite UpdateFrame(float deltaTime)
    {
        int currentFrame = 0;

        if (animationTime.HasValue)
        {
            animationTime += deltaTime;
            animationTime = (animationTime > (secondsPerFrame * frameCount)) ? animationTime - (secondsPerFrame * frameCount) : animationTime;

            currentFrame = (int)(animationTime / secondsPerFrame);
        }

        TextureRect = new IntRect(upperLeftCorner.X + (currentFrame * spriteSize.X), upperLeftCorner.Y, spriteSize.X, spriteSize.Y);
        return this;

    }
}