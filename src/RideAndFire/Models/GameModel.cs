using System.Collections.Generic;

namespace RideAndFire.Models;

public class GameModel : Model
{
    public TileModel[,] Map { get; set; }
    public PlayerModel Player { get; set; }
    public IEnumerable<BulletModel> Bullets => _bullets;
    
    private readonly List<BulletModel> _bullets = [];
    
    public override void Update()
    {
        Player.Update();
        UpdateBullets();
    }

    public void AddBullet(BulletModel bullet)
    {
        _bullets.Add(bullet);
    }
    
    private void UpdateBullets()
    {
        _bullets.ForEach(x => x.Update());

        for (var i = _bullets.Count - 1; i >= 0; i--)
        {
            if (_bullets[i].IsOutOfScreenBounds())
            {
                _bullets.RemoveAt(i);
            }
        }
    }
}