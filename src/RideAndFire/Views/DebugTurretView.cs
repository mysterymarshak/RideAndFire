using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RideAndFire.Extensions;
using RideAndFire.Models;
using RideAndFire.Views.Components;

namespace RideAndFire.Views;

public class DebugTurretView : TurretView
{
    public DebugTurretView(TurretModel turret, SpriteBatch spriteBatch) : base(turret, spriteBatch)
    {
    }

    public override void Draw()
    {
        base.Draw();

        if (!Turret.IsActive)
            return;

        var angle = Turret.Rotation;

        SpriteBatch.DrawBox(Turret.Rectangle, Color.Blue);

        SpriteBatch.DrawLine(Turret.Position,
            Turret.Position + Vector2.TransformNormal(new Vector2(0, -1000), Matrix.CreateRotationZ(angle)),
            Color.Red);

        SpriteBatch.DrawBox(
            new Rectangle(
                (Turret.Position + new Vector2(ViewResources.TurretMuzzleEndOffset.X * MathF.Sin(angle),
                    -ViewResources.TurretMuzzleEndOffset.Y * MathF.Cos(angle))).ToPoint(), new Point(8, 8)),
            Color.Brown);
    }
}