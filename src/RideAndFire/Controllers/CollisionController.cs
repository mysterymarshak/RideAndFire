using System;
using Microsoft.Xna.Framework;
using RideAndFire.Models;

namespace RideAndFire.Controllers;

public class CollisionController
{
    public event Action<BulletModel, IDamageable>? BulletHit;

    private readonly GameModel _gameModel;

    public CollisionController(GameModel gameModel)
    {
        _gameModel = gameModel;
    }

    public void CheckCollisions()
    {
        var player = _gameModel.Player;
        var newPosition = player.Position + player.Velocity;

        var newPlayerRectangle = player.Rectangle;
        newPlayerRectangle.Offset(player.Velocity);

        CheckScreenBoundsCollision(player, newPosition);
        CheckTurretsCollision(player, newPlayerRectangle);
        CheckBulletsCollision(player, newPlayerRectangle);
    }

    private void CheckScreenBoundsCollision(PlayerModel player, Vector2 newPosition)
    {
        var (x, y, width, height) = Constants.MapBounds;
        var velocity = player.Velocity;

        if (newPosition.X - player.Size.X / 2f < x + Constants.TileSize || newPosition.X + player.Size.X / 2f > x + width - Constants.TileSize)
        {
            velocity.X = 0;
        }

        if (newPosition.Y - player.Size.Y / 2f < y + Constants.TileSize || newPosition.Y + player.Size.Y / 2f > y + height - Constants.TileSize)
        {
            velocity.Y = 0;
        }
        
        // todo: improve

        player.Velocity = velocity;
    }

    private void CheckTurretsCollision(PlayerModel player, Rectangle playerRectangle)
    {
        var velocity = player.Velocity;

        foreach (var turret in _gameModel.Turrets)
        {
            var collisionResult = Collide(playerRectangle, turret.Rectangle);
            if (collisionResult is CollisionResult.None)
                continue;

            if ((velocity.X > 0 && collisionResult is CollisionResult.Left) ||
                (velocity.X < 0 && collisionResult is CollisionResult.Right))
            {
                velocity.X = 0;
            }

            if ((velocity.Y > 0 && collisionResult is CollisionResult.Top) ||
                (velocity.Y < 0 && collisionResult is CollisionResult.Bottom))
            {
                velocity.Y = 0;
            }
        }

        player.Velocity = velocity;
    }

    private void CheckBulletsCollision(PlayerModel player, Rectangle playerRectangle)
    {
        foreach (var bullet in _gameModel.Bullets)
        {
            var bulletHandled = false;
            var bulletRectangle = bullet.Rectangle;
            bulletRectangle.Offset(bullet.Velocity);

            foreach (var turret in _gameModel.Turrets)
            {
                if (bullet.Shooter == turret)
                    continue;

                var collidedWithTurret = SimpleCollide(bulletRectangle, turret.Rectangle);
                if (!collidedWithTurret)
                    continue;

                BulletHit?.Invoke(bullet, turret);
                bulletHandled = true;
                break;
            }

            if (bulletHandled || bullet.Shooter == player)
                continue;

            var collidedWithPlayer = SimpleCollide(bulletRectangle, playerRectangle);
            if (!collidedWithPlayer)
                continue;

            BulletHit?.Invoke(bullet, player);
        }
    }

    private static bool SimpleCollide(Rectangle entity1Rectangle, Rectangle entity2Rectangle) =>
        entity1Rectangle.Intersects(entity2Rectangle);

    private static CollisionResult Collide(Rectangle entity1Rectangle, Rectangle entity2Rectangle)
    {
        var result = CollisionResult.None;
        entity1Rectangle.Inflate(1, 1);

        if (!entity1Rectangle.Intersects(entity2Rectangle))
        {
            return result;
        }

        var center1 = entity1Rectangle.Center.ToVector2();
        var center2 = entity2Rectangle.Center.ToVector2();
        var direction = center1 - center2;

        if (Math.Abs(direction.X) > Math.Abs(direction.Y))
        {
            result = direction.X > 0 ? CollisionResult.Right : CollisionResult.Left;
        }
        else
        {
            result = direction.Y > 0 ? CollisionResult.Bottom : CollisionResult.Top;
        }

        return result;
    }
}

public enum CollisionResult
{
    None,

    Right,

    Top,

    Left,

    Bottom
}