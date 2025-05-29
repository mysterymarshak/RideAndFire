using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using RideAndFire.Models;

namespace RideAndFire.Controllers;

public class TurretsController
{
    public event Action? AllTurretsDead;

    private readonly GameModel _model;
    private readonly ShootingController _shootingController;

    public TurretsController(GameModel model, ShootingController shootingController)
    {
        _model = model;
        _shootingController = shootingController;
    }

    public void Initialize()
    {
        var turretLocationTiles = new List<TileModel>
        {
            _model.Map[2, 2],
            _model.Map[Constants.MapWidth - 4, 2],
            _model.Map[Constants.MapWidth - 4, Constants.MapHeight - 4],
            _model.Map[2, Constants.MapHeight - 4]
        };

        foreach (var tile in turretLocationTiles)
        {
            var turretModel = new TurretModel(tile.Bounds.Location.ToVector2() + new Vector2(Constants.TileSize) +
                                              Constants.ScreenOffset);
            turretModel.Dead += OnTurretDead;

            _model.AddTurret(turretModel);
        }
    }

    public void HandleTurretsBehaviour()
    {
        HandleRotation();
        HandleShooting();
    }

    public void SetTurretsState(bool isActive)
    {
        foreach (var turret in _model.Turrets)
        {
            turret.IsActive = isActive;
        }
    }

    public void Dispose()
    {
        foreach (var turret in _model.Turrets)
        {
            turret.Dead -= OnTurretDead;
        }
    }

    private void HandleRotation()
    {
        foreach (var turret in _model.Turrets)
        {
            if (!turret.IsActive)
                continue;

            var player = _model.Player;
            var direction = player.Position - turret.Position;
            var targetAngle = MathHelper.PiOver2 + MathF.Atan2(direction.Y, direction.X);

            var rayToPlayer = new Ray2(turret.Position, Vector2.Rotate(new Vector2(0, -1), turret.Rotation));
            turret.AngularVelocity = MathHelper.WrapAngle(targetAngle - turret.Rotation);
            turret.IsFocusedOnTarget = rayToPlayer.Intersects(player.Rectangle, out _, out _);
        }
    }

    private void HandleShooting()
    {
        var turrets = _model.Turrets;
        foreach (var turret in turrets.Where(x => x.CanShoot))
        {
            _shootingController.Shoot(turret, ViewResources.TurretMuzzleEndOffset);
        }
    }

    private void OnTurretDead(EntityModel entity)
    {
        entity.Dead -= OnTurretDead;

        if (_model.Turrets.All(x => x.IsDead))
        {
            AllTurretsDead?.Invoke();
        }
    }
}