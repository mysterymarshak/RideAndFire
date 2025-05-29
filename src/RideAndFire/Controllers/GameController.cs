using Microsoft.Xna.Framework;
using RideAndFire.Configuration;
using RideAndFire.Helpers;
using RideAndFire.Models;
using RideAndFire.Views.Game;

namespace RideAndFire.Controllers;

public class GameController : GameStateController
{
    private readonly GameView _view;
    private readonly GameModel _model;
    private readonly IMapGenerationStrategy _mapGenerationStrategy;
    private readonly ConfigurationController _configurationController;
    private readonly Timer _startDelayTimer;
    private readonly ShootingController _shootingController;
    private readonly CollisionController _collisionController;
    private readonly TurretsController _turretsController;

    private InputController _inputController;

    public GameController(GameView view, GameModel model, IMapGenerationStrategy mapGenerationStrategy,
        ConfigurationController configurationController)
    {
        _view = view;
        _model = model;
        _mapGenerationStrategy = mapGenerationStrategy;
        _configurationController = configurationController;
        _startDelayTimer = new Timer();
        _shootingController = new ShootingController(_model);
        _collisionController = new CollisionController(_model);
        _turretsController = new TurretsController(_model, _shootingController);
    }

    public override void Initialize()
    {
        ImportConfiguration();
        InitializeMap();
        InitializePlayer();
        InitializeTurrets();

        _collisionController.BulletHit += _shootingController.OnBulletHit;
        _shootingController.BulletCreate += _view.AddBullet;
        _shootingController.BulletRemove += _view.RemoveBullet;
        _model.Player.Dead += OnPlayerDead;
        _model.Score.MaxScoreUpdate += OnMaxScoreUpdated;
        _turretsController.AllTurretsDead += OnGameOver;

        _startDelayTimer.Start(Constants.StartDelay, OnStartDelayPassed);
    }

    public override void OnUpdate(GameTime gameTime)
    {
        _startDelayTimer.Update(gameTime);
        _inputController.OnUpdate(gameTime);

        _turretsController.HandleTurretsBehaviour();
        _shootingController.HandleBulletsMovement(gameTime);

        _collisionController.CheckCollisions();

        _model.Update(gameTime);
    }

    public override void OnDraw()
    {
        _view.Draw();
    }

    public override void Dispose()
    {
        _collisionController.BulletHit -= _shootingController.OnBulletHit;
        _shootingController.BulletCreate -= _view.AddBullet;
        _shootingController.BulletRemove -= _view.RemoveBullet;
        _turretsController.AllTurretsDead -= OnGameOver;
        _model.Score.MaxScoreUpdate -= OnMaxScoreUpdated;
        _model.Player.Dead -= OnPlayerDead;
        _turretsController.Dispose();
    }

    private void OnStartDelayPassed()
    {
        _model.Player.IsActive = true;
        _turretsController.SetTurretsState(true);
    }

    private void OnPlayerDead(EntityModel entity)
    {
        OnGameOver();
    }

    private void OnGameOver()
    {
        var scoreModel = _model.Score;
        if (scoreModel.IsGameOver)
            return;

        var player = _model.Player;
        player.IsActive = false;

        scoreModel.SetGameOver(player.IsDead);
        _turretsController.SetTurretsState(false);
    }

    private void OnMaxScoreUpdated(double bestScore)
    {
        var configuration = _configurationController.Configuration;
        var updatedConfiguration = configuration with { BestScore = bestScore };
        _configurationController.UpdateConfiguration(updatedConfiguration);
    }

    private void ImportConfiguration()
    {
        var configuration = _configurationController.Configuration;
        var bestScore = configuration.BestScore;
        _model.Score = new ScoreModel(bestScore);
    }

    private void InitializeMap()
    {
        _model.Map = _mapGenerationStrategy.GenerateMap();
    }

    private void InitializePlayer()
    {
        var mapCenter = Constants.MapBounds.Center.ToVector2();
        var tankOffset = ViewResources.Tank.Bounds.Location.ToVector2() / 2f;
        _model.Player = new PlayerModel(mapCenter - tankOffset);

        _inputController = new InputController(_model.Player, _shootingController);
    }

    private void InitializeTurrets()
    {
        _turretsController.Initialize();
    }
}