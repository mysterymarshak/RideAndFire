using Microsoft.Xna.Framework;

namespace RideAndFire.Extensions;

public static class GameTimeExtensions
{
    public static float AsDeltaTime(this GameTime gameTime)
    {
        return (float)gameTime.ElapsedGameTime.TotalSeconds;
    }
}