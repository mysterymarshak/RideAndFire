using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RideAndFire.Models;
using RideAndFire.Views.Game.Overlays;

namespace RideAndFire.Views.Game;

public class GameView : View
{
    private readonly GameModel _model;

    private PlayerView _playerView;
    private MapView _mapView;
    private List<TurretView> _turretViews;
    private List<BulletView> _bulletViews;
    private GameOverOverlayView _gameOverOverlayView;
    private ScoreOverlayView _scoreOverlayView;
    private StartOverlayView _startOverlayView;

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
        _gameOverOverlayView = new GameOverOverlayView(_model, SpriteBatch);
        _scoreOverlayView = new ScoreOverlayView(_model, SpriteBatch);
        _startOverlayView = new StartOverlayView(_model, SpriteBatch);
    }

    public override void Draw()
    {
        SpriteBatch.GraphicsDevice.Clear(Color.BlanchedAlmond);

        _mapView.Draw();
        _playerView.Draw();
        _turretViews.ForEach(x => x.Draw());
        _bulletViews.ForEach(x => x.Draw());

        if (_model.Score.IsGameOver)
        {
            _gameOverOverlayView.Draw();
        }
        else
        {
            _scoreOverlayView.Draw();
        }

        _startOverlayView.Draw();
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