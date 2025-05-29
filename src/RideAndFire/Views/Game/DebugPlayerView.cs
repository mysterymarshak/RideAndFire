using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RideAndFire.Extensions;
using RideAndFire.Models;

namespace RideAndFire.Views.Game;

public class DebugPlayerView : PlayerView
{
    public DebugPlayerView(PlayerModel player, SpriteBatch spriteBatch) : base(player, spriteBatch)
    {
    }

    public override void Draw()
    {
        base.Draw();
 
        SpriteBatch.DrawBox(Player.Rectangle, Color.Blue);
    }
}