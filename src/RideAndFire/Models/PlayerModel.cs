using System;
using Microsoft.Xna.Framework;

namespace RideAndFire.Models;

public class PlayerModel : ShooterModel
{
    public override bool IsActive { get; set; }
    public override Point Size => new(48, 48);
    public override float MaxHealth { get; protected set; } = Constants.PlayerHealth;
    public override float Health { get; protected set; } = Constants.PlayerHealth;
    public float AngularVelocity { get; set; }

    protected override TimeSpan ShootingCooldown => TimeSpan.FromMilliseconds(1500);

    public PlayerModel(Vector2 initialPosition)
    {
        Position = initialPosition;
    }

    public override void Update(GameTime gameTime)
    {
        Position += Velocity;
        Rotation += AngularVelocity;

        Velocity = Vector2.Zero;
        AngularVelocity = 0f;

        base.Update(gameTime);
    }
}