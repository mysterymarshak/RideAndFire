using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using RideAndFire.Extensions;
using RideAndFire.Models;

namespace RideAndFire.Views;

public class DebugPlayerView : PlayerView
{
    public DebugPlayerView(PlayerModel player, SpriteBatch spriteBatch) : base(player, spriteBatch)
    {
    }

    public override void Draw()
    {
        base.Draw();

        SpriteBatch.DrawBox(Player.Rectangle, Color.Blue);

        SpriteBatch.DrawRectangle(
            new Rectangle(Player.Rectangle.Location + new Point(0, -40), new Size(Player.Rectangle.Width, 10)),
            Color.Gray);

        SpriteBatch.FillRectangle(
            new RectangleF(Player.Rectangle.Location.ToVector2() + new Vector2(1, -39),
                new SizeF((Player.Rectangle.Width - 1) * (Player.Health / Player.MaxHealth), 9)), Color.Green);
    }
}