using System;
using Microsoft.Xna.Framework;

namespace RideAndFire.Models;

public abstract class EntityModel : Model, IDamageable
{
    public Rectangle Rectangle =>
        new(new Point((int)(Position.X - Size.X / 2f), (int)(Position.Y - Size.Y / 2f)), Size);

    public abstract Point Size { get; }
    public bool IsActive { get; set; }
    public bool IsDead { get; protected set; }

    public virtual float MaxHealth { get; protected set; }

    public virtual float Health
    {
        get => _health;
        protected set
        {
            if (_health > MaxHealth)
            {
                throw new InvalidOperationException("Health cannot exceed MaxHealth.");
            }

            _health = value;
        }
    }

    public float Rotation
    {
        get => _rotation;
        protected set => _rotation = MathHelper.WrapAngle(value);
    }

    public Vector2 Position { get; protected set; }
    public Vector2 Velocity { get; set; }

    private float _health;
    private float _rotation;

    public virtual void OnDamage(float damage)
    {
        if (IsDead || !IsActive)
            return;

        Health = Math.Clamp(Health - damage, 0, Health);

        if (Health == 0)
        {
            IsDead = true;
        }
    }
}