using Microsoft.Xna.Framework;

namespace RideAndFire.Models;

public interface IShooter
{
    Vector2 Position { get; }
    float Rotation { get; }
    bool CanShoot { get; }
    void Shoot();
}