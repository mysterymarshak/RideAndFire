using System;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using RideAndFire.Models;

namespace RideAndFire.Views.Game;

public class StartOverlayView : View
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
            _startOverlayStringBuilder.Append(startDelayLeft.Seconds + 1);

            SpriteBatch.FillRectangle(
                new RectangleF(Vector2.Zero, new SizeF(Constants.ScreenWidth, Constants.ScreenHeight)),
                new Color(Color.Black, 100));
        
            SpriteBatch.DrawString(ViewResources.ComicSansFont, _startOverlayStringBuilder,
                Constants.MapBounds.Center.ToVector2() - ViewResources.ComicSansFont.MeasureString(_startOverlayStringBuilder) +
                new Vector2(0, -Constants.ScreenHeight / 4f), Color.White);

            _startOverlayStringBuilder.Clear();
        }
    }
}