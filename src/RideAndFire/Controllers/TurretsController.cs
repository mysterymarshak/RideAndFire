using System;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using RideAndFire.Helpers;
using RideAndFire.Models;

namespace RideAndFire.Controllers;

public class TurretsController
{
    private readonly GameModel _gameModel;
    private readonly ShootingController _shootingController;
    private readonly Timer _turretsStartTimer;

    public TurretsController(GameModel gameModel, ShootingController shootingController)
    {
        _gameModel = gameModel;
        _shootingController = shootingController;
        _turretsStartTimer = new Timer();
    }

    public void Initialize()
    {
        _turretsStartTimer.Start(Constants.TurretsStartDelay, ActivateTurrets);
    }

    public void HandleTurretsBehaviour(GameTime gameTime)
    {
        _turretsStartTimer.Update(gameTime);

        if (_turretsStartTimer.IsRunning)
            return;

        HandleRotation();
        HandleShooting();
    }

    private void ActivateTurrets()
    {
        foreach (var turret in _gameModel.Turrets)
        {
            turret.IsActive = true;
        }
    }

    private void HandleRotation()
    {
        foreach (var turret in _gameModel.Turrets)
        {
            if (turret.IsDead)
                continue;

            var player = _gameModel.Player;
            var direction = player.Position - turret.Position;
            var targetAngle = MathHelper.PiOver2 + MathF.Atan2(direction.Y, direction.X);

            var rayToPlayer = new Ray2(turret.Position, Vector2.Rotate(new Vector2(0, -1), turret.Rotation));
            turret.AngularVelocity = MathHelper.WrapAngle(targetAngle - turret.Rotation);
            turret.IsFocusedOnTarget = rayToPlayer.Intersects(player.Rectangle, out _, out _);
        }
    }

    private void HandleShooting()
    {
        var turrets = _gameModel.Turrets;
        foreach (var turret in turrets.Where(x => x.CanShoot))
        {
            _shootingController.Shoot(turret, ViewResources.TurretMuzzleEndOffset);
        }
    }
}