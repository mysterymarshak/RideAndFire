using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using RideAndFire.Extensions;
using RideAndFire.Models;
using RideAndFire.Views.Components;

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
    }
}