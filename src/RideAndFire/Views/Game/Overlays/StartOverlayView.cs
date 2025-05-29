using System;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RideAndFire.Models;

namespace RideAndFire.Views.Game.Overlays;

public class StartOverlayView : OverlayView
{
    private readonly GameModel _model;
    private readonly StringBuilder _startOverlayStringBuilder;

    public StartOverlayView(GameModel model, SpriteBatch spriteBatch) : base(spriteBatch)
    {
        _model = model;
        _startOverlayStringBuilder = new StringBuilder();
    }

    public override void Draw()
    {
        var startDelayLeft = _model.StartDelayLeft;
        if (startDelayLeft > TimeSpan.Zero)
        {
            DrawFade();

            _startOverlayStringBuilder.Append(startDelayLeft.Seconds + 1);
            SpriteBatch.DrawString(ViewResources.ComicSansFont, _startOverlayStringBuilder,
                Constants.MapBounds.Center.ToVector2() -
                ViewResources.ComicSansFont.MeasureString(_startOverlayStringBuilder) / 2 +
                new Vector2(0, -Constants.ScreenHeight / 4f), Color.White);

            _startOverlayStringBuilder.Clear();
        }
    }
}