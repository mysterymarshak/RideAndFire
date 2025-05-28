using Microsoft.Xna.Framework.Graphics;

namespace RideAndFire.Views;

public abstract class View
{
    protected SpriteBatch SpriteBatch { get; }

    public View(SpriteBatch spriteBatch)
    {
        SpriteBatch = spriteBatch;
    }

    public abstract void Draw();

    public virtual void Initialize()
    {
    }
}