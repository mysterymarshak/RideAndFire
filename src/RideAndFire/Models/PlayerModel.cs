using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace RideAndFire.Models;

public class PlayerModel : ShooterModel
{
    public override Vector2 Size => new(64, 64);
    public sealed override Vector2 Position { get; protected set; }
    public override float Rotation { get; protected set; }

    public Vector2 Velocity { get; set; }
    public float AngularVelocity { get; set; }
    public override bool IsShooting { get; set; }
    
    protected override TimeSpan ShootingDelay => TimeSpan.FromMilliseconds(1500); 

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
        IsShooting = false;
    }
}