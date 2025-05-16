using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RideAndFire.Models;
using RideAndFire.Views;

namespace RideAndFire.Controllers;

public abstract class Controller
{
    public abstract void OnUpdate(GameTime gameTime);
    public abstract void OnDraw();
}

public class GameController : Controller
{
    private readonly GameView _view;
    private readonly GameModel _model;
    private readonly InputController _inputController;
    private readonly ShootingController _shootingController;
    private readonly CollisionController _collisionController;

    public GameController(GameView view, GameModel model)
    {
        _view = view;
        _model = model;
        InitializeModels();
        
        _inputController = new InputController(_model.Player);
        _shootingController = new ShootingController();
        _collisionController = new CollisionController();
    }

    public override void OnUpdate(GameTime gameTime)
    {
        _inputController.OnUpdate(gameTime);
        
        if (_model.Player.IsShooting)
        {
            var bulletModel = _shootingController.Shoot(_model.Player, ViewResources.TankMuzzleEndOffset);
            _model.AddBullet(bulletModel);
        }
        
        _collisionController.CheckScreenBoundsCollision(_model.Player);
        
        _model.Update();
    }

    public override void OnDraw()
    {
        _view.Draw();
    }

    private void InitializeModels()
    {
        _model.Map = new TileModel[Constants.MapWidth, Constants.MapHeight];

        for (var x = 0; x < Constants.MapWidth; x++)
        {
            for (var y = 0; y < Constants.MapHeight; y++)
            {
                _model.Map[x, y] = new TileModel { Type = TileType.Sand, X = x, Y = y };
            }
        }

        var mapCenter = new Vector2(Constants.MapWidth / 2f * Constants.TileSize,
            Constants.MapHeight / 2f * Constants.TileSize);
        
        _model.Player = new PlayerModel(mapCenter);
    }
}