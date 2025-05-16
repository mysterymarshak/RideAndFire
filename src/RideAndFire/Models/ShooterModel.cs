using System;
using Microsoft.Xna.Framework;
using RideAndFire.Helpers;

namespace RideAndFire.Models;

public abstract class ShooterModel : EntityModel, IShooter
{
    public abstract bool IsShooting { get; set; }

    public bool CanShoot => !_cooldownTimer.IsRunning;

    protected abstract TimeSpan ShootingDelay { get; }

    private readonly Timer _cooldownTimer = new();

    public void Shoot()
    {
        _cooldownTimer.Start(ShootingDelay);
    }
}