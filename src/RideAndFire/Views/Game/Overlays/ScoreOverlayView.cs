using System;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RideAndFire.Models;

namespace RideAndFire.Views.Game.Overlays;

public class ScoreOverlayView : View
{
    private readonly GameModel _model;
    private readonly StringBuilder _stringBuilder;

    public ScoreOverlayView(GameModel model, SpriteBatch spriteBatch) : base(spriteBatch)
    {
        _model = model;
        _stringBuilder = new StringBuilder();
    }

    public override void Draw()
    {
        _stringBuilder.Append("Score: ");
        AppendScoreFormatted();

        SpriteBatch.DrawString(ViewResources.BasicFont, _stringBuilder,
            new Vector2(Constants.ScreenWidth / 2f - ViewResources.BasicFont.MeasureString(_stringBuilder).X / 2, 0),
            Color.Black);

        _stringBuilder.Clear();
    }

    private void AppendScoreFormatted()
    {
        var value = _model.Score.TimePlayed.TotalSeconds;

        var number = (int)Math.Round(value * 10, MidpointRounding.AwayFromZero);
        var integerPart = number / 10;
        var fractionalPart = number % 10;

        _stringBuilder.Append(integerPart);
        _stringBuilder.Append('.');
        _stringBuilder.Append(fractionalPart);
    }
}