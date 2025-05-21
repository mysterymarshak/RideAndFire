using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RideAndFire.Extensions;

public static class SpriteBatchExtensions
{
    private static Texture2D? _lineTexture;

    public static void DrawBox(this SpriteBatch spriteBatch, Rectangle rectangle, Color color, float thickness = 1f)
    {
        var (x, y, width, height) = rectangle;

        spriteBatch.DrawLine(new Vector2(x, y), new Vector2(x + width, y), color, thickness);
        spriteBatch.DrawLine(new Vector2(x + width, y), new Vector2(x + width, y + height), color, thickness);
        spriteBatch.DrawLine(new Vector2(x + width, y + height), new Vector2(x, y + height), color, thickness);
        spriteBatch.DrawLine(new Vector2(x, y + height), new Vector2(x, y), color, thickness);
    }

    public static void DrawLine(this SpriteBatch spriteBatch, Vector2 point1, Vector2 point2, Color color,
        float thickness = 1f)
    {
        var origin = new Vector2(0f, 0.5f);
        var scale = new Vector2(Vector2.Distance(point1, point2), thickness);
        var angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);

        spriteBatch.Draw(GetLineTexture(spriteBatch), point1, null, color, angle, origin, scale, SpriteEffects.None, 0);
    }

    private static Texture2D GetLineTexture(SpriteBatch spriteBatch)
    {
        if (_lineTexture is null)
        {
            _lineTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            _lineTexture.SetData([Color.White]);
        }

        return _lineTexture;
    }
}