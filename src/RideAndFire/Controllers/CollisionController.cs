using Microsoft.Xna.Framework;
using RideAndFire.Models;

namespace RideAndFire.Controllers;

public class CollisionController
{
    public void CheckScreenBoundsCollision(PlayerModel player)
    {
        var (x, y, width, height) = Constants.MapBounds;
        var newPosition = player.Position + player.Velocity;

        if (newPosition.X < x + Constants.TileSize || newPosition.X > x + width - Constants.TileSize)
        {
            player.Velocity = new Vector2(0, player.Velocity.Y);
        }

        if (newPosition.Y < y + Constants.TileSize || newPosition.Y > y + height - Constants.TileSize)
        {
            player.Velocity = new Vector2(player.Velocity.X, 0);
        }
    }
}