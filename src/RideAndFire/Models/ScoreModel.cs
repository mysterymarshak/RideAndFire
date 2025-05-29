using System;
using Microsoft.Xna.Framework;

namespace RideAndFire.Models;

public class ScoreModel : Model
{
    public event Action<double>? MaxScoreUpdate;

    public double BestScore { get; private set; }
    public double Score => GetScore();
    public bool IsRecordBroken { get; private set; }
    public TimeSpan TimePlayed { get; private set; }
    public bool IsGameOver { get; private set; }
    public bool IsPlayerDead { get; private set; }

    public ScoreModel(double bestScore)
    {
        BestScore = bestScore;
    }

    public override void Update(GameTime gameTime)
    {
        if (IsGameOver)
            return;

        TimePlayed += gameTime.ElapsedGameTime;
    }

    public void SetGameOver(bool isPlayerDead)
    {
        IsGameOver = true;
        IsPlayerDead = isPlayerDead;

        if (isPlayerDead)
            return;

        if (Score < BestScore)
        {
            BestScore = Score;
            IsRecordBroken = true;
            MaxScoreUpdate?.Invoke(Score);
        }
    }

    public double GetScore()
    {
        return IsPlayerDead ? 0 : Math.Round(TimePlayed.TotalSeconds, 1);
    }
}