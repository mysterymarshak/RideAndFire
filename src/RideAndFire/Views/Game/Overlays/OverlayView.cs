using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace RideAndFire.Views.Game.Overlays;

public class OverlayView : View
{
    public OverlayView(SpriteBatch spriteBatch) : base(spriteBatch)
    {
    }

    protected void DrawFade()
    {
        SpriteBatch.FillRectangle(
            new RectangleF(Vector2.Zero, new SizeF(Constants.ScreenWidth, Constants.ScreenHeight)),
            new Color(Color.Black, 100));
    }
}