using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RideAndFire.Models;

namespace RideAndFire.Views;

public class BulletView : View
{
    public BulletModel Bullet { get; }

    public BulletView(BulletModel bullet, SpriteBatch spriteBatch) : base(spriteBatch)
    {
        Bullet = bullet;
    }

    public override void Draw()
    {
        SpriteBatch.Draw(ViewResources.Bullet, new Rectangle(Bullet.Position.ToPoint(), Bullet.Size), null,
            Color.White, Bullet.Rotation, ViewResources.Bullet.Bounds.Center.ToVector2(), SpriteEffects.None, 0f);
    }
}