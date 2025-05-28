using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RideAndFire.Models;

namespace RideAndFire.Views;

public class PlayerView : View
{
    protected PlayerModel Player { get; }

    public PlayerView(PlayerModel player, SpriteBatch spriteBatch) : base(spriteBatch)
    {
        Player = player;
    }

    public override void Draw()
    {
        var angle = Player.Rotation;

        SpriteBatch.Draw(ViewResources.Tank, new Rectangle(Player.Position.ToPoint(), Player.Size), null,
            Color.White, angle, ViewResources.Tank.Bounds.Center.ToVector2(), SpriteEffects.None, 0f);

        SpriteBatch.Draw(ViewResources.TankMuzzle,
            Player.Position + new Vector2(ViewResources.TankMuzzleOffset.X * MathF.Sin(angle),
                -ViewResources.TankMuzzleOffset.Y * MathF.Cos(angle)), null, Color.White, angle,
            ViewResources.TankMuzzle.Bounds.Center.ToVector2(), Vector2.One, SpriteEffects.None, 0);
    }
}