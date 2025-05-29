using Microsoft.Xna.Framework;

namespace RideAndFire.Controllers;

public abstract class GameStateController
{
    public abstract void Initialize();
    public abstract void OnUpdate(GameTime gameTime);
    public abstract void OnDraw();
    public abstract void Dispose();
}