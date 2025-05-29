using System;
using Microsoft.Xna.Framework;
using RideAndFire.Helpers;

namespace RideAndFire.Models;

public abstract class ShooterModel : EntityModel, IShooter
{
    public virtual bool CanShoot => !IsTimerRunning;

    protected abstract TimeSpan ShootingCooldown { get; }
    protected bool IsTimerRunning => _cooldownTimer.IsRunning;

    private readonly Timer _cooldownTimer = new();

    public override void Update(GameTime gameTime)
    {
        _cooldownTimer.Update(gameTime);
    }

    public void Shoot()
    {
        if (!CanShoot)
        {
            throw new InvalidOperationException("you cant shoot");
        }

        _cooldownTimer.Start(ShootingCooldown);
    }
}