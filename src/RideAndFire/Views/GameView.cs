using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using RideAndFire.Models;

namespace RideAndFire.Views;

public class GameView : View
{
    private readonly GameModel _model;

    private PlayerView _playerView;
    private MapView _mapView;
    private List<TurretView> _turretViews;
    private List<BulletView> _bulletViews;

    public GameView(GameModel model, SpriteBatch spriteBatch) : base(spriteBatch)
    {
        _model = model;
    }

    public override void Initialize()
    {
        _mapView = new MapView(_model.Map, SpriteBatch);

#if DEBUG
        _playerView = new DebugPlayerView(_model.Player, SpriteBatch);
        _turretViews = _model.Turrets
            .Select(TurretView (x) => new DebugTurretView(x, SpriteBatch))
            .ToList();
#else
            _playerView = new PlayerView(_model.Player, SpriteBatch);
            _turretViews = _model.Turrets
                .Select(x => new TurretView(x, SpriteBatch))
                .ToList();
#endif

        _bulletViews = [];
    }

    public override void Draw()
    {
        _mapView.Draw();
        _playerView.Draw();
        _turretViews.ForEach(x => x.Draw());
        _bulletViews.ForEach(x => x.Draw());
    }

    public void AddBullet(BulletModel bullet)
    {
        var bulletView = new BulletView(bullet, SpriteBatch);
        _bulletViews.Add(bulletView);
    }

    public void RemoveBullet(BulletModel bullet)
    {
        var bulletView = _bulletViews.FirstOrDefault(x => x.Bullet == bullet);
        if (bulletView is not null)
        {
            _bulletViews.Remove(bulletView);
        }
    }
}