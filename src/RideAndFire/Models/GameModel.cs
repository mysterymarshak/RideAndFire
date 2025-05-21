using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace RideAndFire.Models;

public class GameModel : Model
{
    public TileModel[,] Map { get; set; }
    public PlayerModel Player { get; set; }
    public IEnumerable<BulletModel> Bullets => _bullets;

    private readonly List<BulletModel> _bullets = [];

    public override void Update(GameTime gameTime)
    {
        Player.Update(gameTime);
        UpdateBullets(gameTime);
    }

    public void AddBullet(BulletModel bullet)
    {
        _bullets.Add(bullet);
    }

    private void UpdateBullets(GameTime gameTime)
    {
        for (var i = _bullets.Count - 1; i >= 0; i--)
        {
            var bullet = _bullets[i];

            bullet.Update(gameTime);

            if (_bullets[i].IsOutOfScreenBounds())
            {
                _bullets.RemoveAt(i);
            }
        }
    }
}