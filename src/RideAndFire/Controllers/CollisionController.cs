using Microsoft.Xna.Framework;
using RideAndFire.Models;

namespace RideAndFire.Controllers;

public class CollisionController
{
    public void CheckScreenBoundsCollision(PlayerModel player)
    {
        var newPosition = player.Position + player.Velocity;
        
        if (newPosition.X is < Constants.TileSize or > Constants.ScreenWidth - Constants.TileSize)
        {
            player.Velocity = new Vector2(0, player.Velocity.Y);
        }

        if (newPosition.Y is < Constants.TileSize or > Constants.ScreenHeight - Constants.TileSize)
        {
            player.Velocity = new Vector2(player.Velocity.X, 0);
        }
    }
}