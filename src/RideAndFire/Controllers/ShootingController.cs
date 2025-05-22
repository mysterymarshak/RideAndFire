using System;
using Microsoft.Xna.Framework;
using RideAndFire.Extensions;
using RideAndFire.Models;

namespace RideAndFire.Controllers;

public class ShootingController
{
    private readonly GameModel _gameModel;

    public ShootingController(GameModel gameModel)
    {
        _gameModel = gameModel;
    }

    public void HandleBulletsMovement(GameTime gameTime)
    {
        var bullets = _gameModel.Bullets;

        foreach (var bullet in bullets)
        {
            if (bullet.IsOutOfScreenBounds())
            {
                bullet.MarkForRemoval();
                continue;
            }

            bullet.Velocity = bullet.Direction * Constants.BulletSpeed * gameTime.AsDeltaTime();
        }
    }

    public void Shoot(IShooter shooter, Vector2 muzzleOffset)
    {
        var rotation = shooter.Rotation;
        var transformMatrix = Matrix.CreateRotationZ(rotation);
        var velocity = Vector2.Transform(new Vector2(0, -1), transformMatrix);

        shooter.Shoot();

        var bullet = new BulletModel(shooter,
            shooter.Position + new Vector2(muzzleOffset.X * MathF.Sin(rotation), -muzzleOffset.Y * MathF.Cos(rotation)),
            rotation, velocity);
        _gameModel.AddBullet(bullet);
    }

    public void OnBulletHit(BulletModel bullet, IDamageable target)
    {
        bullet.MarkForRemoval();

        if (target.GetType() == bullet.Shooter.GetType())
            return;

        target.OnDamage(Constants.BulletDamage);
    }
}