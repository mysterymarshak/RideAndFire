using Microsoft.Xna.Framework;
using RideAndFire.Extensions;

namespace RideAndFire.Models;

public class BulletModel : EntityModel
{
    public override Vector2 Size => new(16, 16);
    public sealed override Vector2 Position { get; protected set; }
    public sealed override float Rotation { get; protected set; }

    private readonly Vector2 _constantVelocity;

    public BulletModel(Vector2 initialPosition, float rotation, Vector2 constantVelocity)
    {
        Position = initialPosition;
        Rotation = rotation;
        _constantVelocity = constantVelocity;
    }

    public override void Update(GameTime gameTime)
    {
        Position += _constantVelocity * gameTime.AsDeltaTime();
    }

    public bool IsOutOfScreenBounds() =>
        Position.X is < 0 or > Constants.ScreenWidth || Position.Y is < 0 or > Constants.ScreenHeight;
}