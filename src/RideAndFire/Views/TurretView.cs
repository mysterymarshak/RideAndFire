using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RideAndFire.Models;

namespace RideAndFire.Views;

public class TurretView : View
{
    protected TurretModel Turret { get; }

    public TurretView(TurretModel turret, SpriteBatch spriteBatch) : base(spriteBatch)
    {
        Turret = turret;
    }

    public override void Draw()
    {
        var angle = Turret.Rotation;
        var maskColor = Turret.IsDead ? Color.LightSlateGray : Color.White;

        SpriteBatch.Draw(ViewResources.TurretBase, new Rectangle(Turret.Position.ToPoint(), Turret.Size), null,
            maskColor, 0f, ViewResources.TurretBase.Bounds.Center.ToVector2(), SpriteEffects.None, 0f);

        SpriteBatch.Draw(ViewResources.Turret,
            new Rectangle(
                Turret.Position.ToPoint() + new Vector2(ViewResources.TurretOffset.X * MathF.Sin(angle),
                    -ViewResources.TurretOffset.Y * MathF.Cos(angle)).ToPoint(), Turret.Size), null, maskColor,
            angle, ViewResources.Turret.Bounds.Center.ToVector2(), SpriteEffects.None, 0f);
    }
}