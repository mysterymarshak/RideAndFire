using Microsoft.Xna.Framework;

namespace RideAndFire.Models;

public class BulletModel : EntityModel
{
    public override Point Size => new(16, 16);
    public IShooter Shooter { get; }
    public Vector2 Direction { get; }
    public bool MarkedForRemoval { get; private set; }

    public BulletModel(IShooter shooter, Vector2 initialPosition, float rotation, Vector2 direction)
    {
        Shooter = shooter;
        Position = initialPosition;
        Rotation = rotation;
        Direction = direction;
    }

    public override void Update(GameTime gameTime)
    {
        Position += Velocity;

        Velocity = Vector2.Zero;
    }

    public bool IsOutOfScreenBounds() => !Constants.MapBounds.Contains(Position);

    public void MarkForRemoval()
    {
        MarkedForRemoval = true;
    }
}