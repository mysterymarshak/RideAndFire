using System;
using Microsoft.Xna.Framework;
using RideAndFire.Models;

namespace RideAndFire.Controllers;

public class ShootingController
{
    public BulletModel Shoot(IShooter shooter, Vector2 muzzleOffset)
    {
        var rotation = shooter.Rotation;
        var transformMatrix = Matrix.CreateRotationZ(rotation);
        var velocity = Vector2.Transform(new Vector2(0, -1) * Constants.BulletSpeed, transformMatrix);

        shooter.Shoot();
        return new BulletModel(
            shooter.Position + new Vector2(muzzleOffset.X * MathF.Sin(rotation),
                -muzzleOffset.Y * MathF.Cos(rotation)), rotation, velocity);
    }
}