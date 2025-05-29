using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RideAndFire.Models;

namespace RideAndFire.Views.Game.Overlays;

public class GameOverOverlayView : OverlayView
{
    private readonly GameModel _model;

    public GameOverOverlayView(GameModel model, SpriteBatch spriteBatch) : base(spriteBatch)
    {
        _model = model;
    }

    public override void Draw()
    {
        DrawFade();

        var screenQuarter = new Vector2(0, Constants.ScreenHeight / 4f);
        var scoreModel = _model.Score;
        var bestScore = scoreModel.BestScore;
        var isPlayerDead = scoreModel.IsPlayerDead;
        var isRecordBroken = scoreModel.IsRecordBroken;

        var gameOverMessage = isPlayerDead ? "You lost!" : "You won!";
        var gameOverMessageSize = ViewResources.ComicSansFont.MeasureString(gameOverMessage);
        SpriteBatch.DrawString(ViewResources.ComicSansFont, gameOverMessage,
            Constants.ScreenCenter - (gameOverMessageSize / 2) - screenQuarter, Color.White);

        var scoreMessagePrefix = isRecordBroken ? "New record! " : string.Empty;
        var scoreMessage = string.Format(CultureInfo.InvariantCulture, scoreMessagePrefix + "Your score: {0:0.0}",
            scoreModel.GetScore());
        var scoreMessageSize = ViewResources.ComicSansFont.MeasureString(scoreMessage);
        SpriteBatch.DrawString(ViewResources.ComicSansFont, scoreMessage,
            Constants.ScreenCenter - (scoreMessageSize / 2) - screenQuarter + new Vector2(0, gameOverMessageSize.Y),
            Color.White);

        if (!isRecordBroken)
        {
            var bestScoreMessage = string.Format(CultureInfo.InvariantCulture, "Your best score: {0:0.0}",
                double.IsNaN(bestScore) ? 0d : bestScore);
            var bestScoreMessageSize = ViewResources.ComicSansFont.MeasureString(bestScoreMessage);
            SpriteBatch.DrawString(ViewResources.ComicSansFont, bestScoreMessage,
                Constants.ScreenCenter - (bestScoreMessageSize / 2) - screenQuarter +
                new Vector2(0, gameOverMessageSize.Y + scoreMessageSize.Y), Color.White);
        }

        var restartMessage = "Press ESC to exit to the main menu";
        SpriteBatch.DrawString(ViewResources.ComicSansFont, restartMessage,
            Constants.ScreenCenter - ViewResources.ComicSansFont.MeasureString(restartMessage) / 2 +
            new Vector2(0, Constants.ScreenHeight / 4f), Color.White);
    }
}