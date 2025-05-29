using System;
using Microsoft.Xna.Framework;
using RideAndFire.Extensions;

namespace RideAndFire.Models;

public class TurretModel : ShooterModel
{
    public override Point Size => new(96, 96);
    public override bool CanShoot => IsActive && !IsTimerRunning && IsFocusedOnTarget;
    public override float MaxHealth { get; protected set; } = Constants.TurretHealth;
    public override float Health { get; protected set; } = Constants.TurretHealth;
    public float AngularVelocity { get; set; }
    public bool IsFocusedOnTarget { get; set; }

    protected override TimeSpan ShootingCooldown => GetRandomShootingCooldown();

    public TurretModel(Vector2 initialPosition)
    {
        Position = initialPosition;
    }

    public override void Update(GameTime gameTime)
    {
        var maxRotationVelocity = Constants.MaxTurretRotationSpeed * gameTime.AsDeltaTime();
        Rotation += Math.Clamp(AngularVelocity, -maxRotationVelocity, maxRotationVelocity);

        AngularVelocity = 0f;
        IsFocusedOnTarget = false;

        base.Update(gameTime);
    }

    private TimeSpan GetRandomShootingCooldown()
    {
        return Constants.TurretShootingCooldownMin + TimeSpan.FromSeconds(Random.Shared.NextDouble() *
                                                                          (Constants.TurretShootingCooldownMax -
                                                                           Constants.TurretShootingCooldownMin)
                                                                          .TotalSeconds);
    }
}