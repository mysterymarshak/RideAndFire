using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using RideAndFire.Helpers;
using RideAndFire.Models;
using RideAndFire.Views;
using RideAndFire.Views.Game;

namespace RideAndFire.Controllers;

public class GameController : Controller
{
    private readonly GameView _view;
    private readonly GameModel _model;
    private readonly Timer _startDelayTimer;
    private readonly ShootingController _shootingController;
    private readonly CollisionController _collisionController;
    private readonly TurretsController _turretsController;

    private InputController _inputController;

    public GameController(GameView view, GameModel model)
    {
        _view = view;
        _model = model;
        _startDelayTimer = new Timer();
        _shootingController = new ShootingController(_model);
        _collisionController = new CollisionController(_model);
        _turretsController = new TurretsController(_model, _shootingController);
    }

    public override void Initialize()
    {
        InitializeMap();
        InitializePlayer();
        InitializeTurrets();

        _collisionController.BulletHit += _shootingController.OnBulletHit;
        _shootingController.BulletCreated += _view.AddBullet;
        _shootingController.BulletRemoved += _view.RemoveBullet;
        
        _startDelayTimer.Start(Constants.StartDelay, OnStartDelayPassed);
    }
    
    public override void OnUpdate(GameTime gameTime)
    {
        _startDelayTimer.Update(gameTime);
        _inputController.OnUpdate(gameTime);

        HandlePlayerShooting();
        _turretsController.HandleTurretsBehaviour();
        _shootingController.HandleBulletsMovement(gameTime);

        _collisionController.CheckCollisions();

        _model.Update(gameTime);
    }

    public override void OnDraw()
    {
        _view.Draw();
    }

    private void OnStartDelayPassed()
    {
        _model.Player.IsActive = true;
        _turretsController.ActivateTurrets();
    }
    
    private void InitializeMap()
    {
        _model.Map = new TileModel[Constants.MapWidth, Constants.MapHeight];

        for (var x = 0; x < Constants.MapWidth; x++)
        {
            for (var y = 0; y < Constants.MapHeight; y++)
            {
                var point = new Point(x, y);
                var tileType = TileType.Dirt;

                var dirtAreas = new List<Rectangle>
                {
                    new(2, 2, 2, 2),
                    new(Constants.MapWidth - 4, 2, 2, 2),
                    new(Constants.MapWidth - 4, Constants.MapHeight - 4, 2, 2),
                    new(2, Constants.MapHeight - 4, 2, 2),
                };

                if (dirtAreas.Any(z => z.Contains(point)))
                {
                    tileType = TileType.Turret;
                }
                
                var wallAreas = new List<Rectangle>
                {
                    new(0, 0, Constants.MapWidth, 1),
                    new(0, Constants.MapHeight - 1, Constants.MapWidth, 1),
                    new(0, 0, 1, Constants.MapHeight),
                    new(Constants.MapWidth - 1, 0, 1, Constants.MapHeight)
                };
                
                if (wallAreas.Any(z => z.Contains(point)))
                {
                    tileType = TileType.Wall;
                }

                _model.Map[x, y] = new TileModel { Type = tileType, X = x, Y = y };
            }
        }

        // todo: extract & improve
    }

    private void InitializePlayer()
    {
        var mapCenter = Constants.MapBounds.Center - ViewResources.Tank.Bounds.Center;
        _model.Player = new PlayerModel(mapCenter.ToVector2());

        _inputController = new InputController(_model.Player);
    }

    private void InitializeTurrets()
    {
        var tiles = new List<TileModel>
        {
            _model.Map[2, 2],
            _model.Map[Constants.MapWidth - 4, 2],
            _model.Map[Constants.MapWidth - 4, Constants.MapHeight - 4],
            _model.Map[2, Constants.MapHeight - 4]
        };

        foreach (var tile in tiles)
        {
            var turretModel = new TurretModel(tile.Bounds.Location.ToVector2() + new Vector2(Constants.TileSize) +
                                              Constants.ScreenOffset);
            _model.AddTurret(turretModel);
        }
        
        // todo: extract & improve
    }

    private void HandlePlayerShooting()
    {
        if (_model.Player.IsShooting)
        {
            _shootingController.Shoot(_model.Player, ViewResources.TankMuzzleEndOffset);
        }
    }
}