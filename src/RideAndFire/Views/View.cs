﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RideAndFire.Views;

public abstract class View
{
    protected SpriteBatch SpriteBatch { get; }

    public View(SpriteBatch spriteBatch)
    {
        SpriteBatch = spriteBatch;
    }

    public virtual void Draw()
    {
    }

    public virtual void DrawTimed(GameTime gameTime)
    {
    }

    public virtual void Initialize()
    {
    }

    public virtual void Dispose()
    {
    }
}