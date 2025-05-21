using Microsoft.Xna.Framework;
using RideAndFire.Models;
using RideAndFire.Views;

namespace RideAndFire.Controllers;

public class GameController
{
    private readonly GameView _view;
    private readonly GameModel _model;
    private readonly ShootingController _shootingController;
    private readonly CollisionController _collisionController;

    private InputController _inputController;

    public GameController(GameView view, GameModel model)
    {
        _view = view;
        _model = model;
        _shootingController = new ShootingController();
        _collisionController = new CollisionController();
    }

    public void Initialize()
    {
        InitializeMap();
        InitializePlayer();
    }

    public void OnUpdate(GameTime gameTime)
    {
        _inputController.OnUpdate(gameTime);

        if (_model.Player.IsShooting)
        {
            var bulletModel = _shootingController.Shoot(_model.Player, ViewResources.TankMuzzleEndOffset);
            _model.AddBullet(bulletModel);
        }

        _collisionController.CheckScreenBoundsCollision(_model.Player);

        _model.Update(gameTime);
    }

    public void OnDraw()
    {
        _view.Draw();
    }

    private void InitializeMap()
    {
        _model.Map = new TileModel[Constants.MapWidth, Constants.MapHeight];

        for (var x = 0; x < Constants.MapWidth; x++)
        {
            for (var y = 0; y < Constants.MapHeight; y++)
            {
                _model.Map[x, y] = new TileModel { Type = TileType.Sand, X = x, Y = y };
            }
        }
    }

    private void InitializePlayer()
    {
        var mapCenter = Constants.MapBounds.Center - ViewResources.Tank.Bounds.Center;
        _model.Player = new PlayerModel(mapCenter.ToVector2());

        _inputController = new InputController(_model.Player);
    }
}