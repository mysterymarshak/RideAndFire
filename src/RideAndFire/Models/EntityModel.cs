using Microsoft.Xna.Framework;

namespace RideAndFire.Models;

public abstract class EntityModel : Model
{
    public virtual Rectangle Rectangle => new(new Point((int)(Position.X - Size.X / 2), (int)(Position.Y - Size.Y / 2)),
        Size.ToPoint());

    public abstract Vector2 Size { get; }
    public abstract Vector2 Position { get; protected set; }
    public abstract float Rotation { get; protected set; }
}