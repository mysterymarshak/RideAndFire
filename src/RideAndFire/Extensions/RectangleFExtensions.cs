using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace RideAndFire.Extensions;

public static class RectangleFExtensions
{
    public static RectangleF WithCenter(this RectangleF rectangle, Vector2 newCenter)
    {
        var offset = newCenter - rectangle.Center;
        rectangle.Offset(offset.X, offset.Y);

        return rectangle;
    }
}