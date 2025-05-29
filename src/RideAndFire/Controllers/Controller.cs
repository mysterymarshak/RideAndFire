using Microsoft.Xna.Framework;

namespace RideAndFire.Controllers;

public abstract class Controller
{
    public abstract void Initialize();
    public abstract void OnUpdate(GameTime gameTime);
    public abstract void OnDraw();
}