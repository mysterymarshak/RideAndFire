﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace RideAndFire.Models;

public class GameModel : Model
{
    public TimeSpan StartDelayLeft { get; set; } = Constants.StartDelay;
    public TileModel[,] Map { get; set; }
    public PlayerModel Player { get; set; }
    public ScoreModel Score { get; set; }
    public IEnumerable<BulletModel> Bullets => _bullets;
    public IEnumerable<TurretModel> Turrets => _turrets;

    private readonly List<BulletModel> _bullets = [];
    private readonly List<TurretModel> _turrets = [];

    public override void Update(GameTime gameTime)
    {
        StartDelayLeft = TimeSpan.FromSeconds(Math.Clamp((StartDelayLeft - gameTime.ElapsedGameTime).TotalSeconds, 0,
            Constants.StartDelay.TotalSeconds));
        Player.Update(gameTime);
        UpdateBullets(gameTime);
        UpdateTurrets(gameTime);
        UpdateScore(gameTime);
    }

    public void AddBullet(BulletModel bullet)
    {
        _bullets.Add(bullet);
    }

    public void AddTurret(TurretModel turret)
    {
        _turrets.Add(turret);
    }

    private void UpdateBullets(GameTime gameTime)
    {
        for (var i = _bullets.Count - 1; i >= 0; i--)
        {
            var bullet = _bullets[i];

            if (bullet.MarkedForRemoval)
            {
                _bullets.RemoveAt(i);
                continue;
            }

            bullet.Update(gameTime);
        }
    }

    private void UpdateTurrets(GameTime gameTime)
    {
        foreach (var turret in _turrets)
        {
            turret.Update(gameTime);
        }
    }

    private void UpdateScore(GameTime gameTime)
    {
        if (StartDelayLeft != TimeSpan.Zero)
            return;

        Score.Update(gameTime);
    }
}