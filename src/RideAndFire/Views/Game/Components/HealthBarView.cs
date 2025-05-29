using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using RideAndFire.Extensions;
using RideAndFire.Models;

namespace RideAndFire.Views.Game.Components;

public class HealthBarView : View
{
    private static readonly SizeF HealthBarSize = new(60, 10);
    private static readonly int HealthBarBorderThickness = 1;
    private static readonly Color HealthBarColor = Color.ForestGreen;
    private static readonly Color HealthBarBorderColor = Color.Black;

    private readonly EntityModel _entity;
    private readonly Vector2 _offset;

    public HealthBarView(EntityModel entity, Point offset, SpriteBatch spriteBatch) : base(spriteBatch)
    {
        _entity = entity;
        _offset = offset.ToVector2();
    }

    public override void Draw()
    {
        var rectangleF = _entity.Rectangle.ToRectangleF();
        var healthPercentage = _entity.Health / _entity.MaxHealth;

        var center = rectangleF.Center + new Vector2(0, -rectangleF.Height / 2) + _offset;
        var borderRectangle = new RectangleF(Vector2.Zero, HealthBarSize).WithCenter(center);
        SpriteBatch.DrawRectangle(borderRectangle, HealthBarBorderColor, HealthBarBorderThickness);

        var healthBarRectangle = borderRectangle
            .GetRelativeRectangle(HealthBarBorderThickness, HealthBarBorderThickness,
                (borderRectangle.Width - HealthBarBorderThickness * 2) * healthPercentage,
                borderRectangle.Height - HealthBarBorderThickness * 2);
        SpriteBatch.FillRectangle(healthBarRectangle, HealthBarColor);
    }
}