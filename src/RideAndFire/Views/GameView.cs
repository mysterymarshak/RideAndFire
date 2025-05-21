using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RideAndFire.Extensions;
using RideAndFire.Models;

namespace RideAndFire.Views;

public abstract class View
{
    public abstract void Draw();
}

public class GameView : View
{
    private readonly GameModel _model;
    private readonly SpriteBatch _spriteBatch;
    private readonly GraphicsDeviceManager _graphics;

    public GameView(GameModel model, SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
    {
        _model = model;
        _spriteBatch = spriteBatch;
        _graphics = graphics;
    }

    public override void Draw()
    {
        DrawMap();
        DrawPlayer();
        DrawBullets();
    }

    private void DrawMap()
    {
        for (var x = 0; x < Constants.MapWidth; x++)
        {
            for (var y = 0; y < Constants.MapHeight; y++)
            {
                var tile = _model.Map[x, y];
                var bounds = tile.Bounds;
                bounds.Offset(Constants.ScreenOffset);
                
                _spriteBatch.Draw(ViewResources.SandTile, bounds, Color.White);
                //_spriteBatch.DrawBox(bounds, Color.Red, thickness: 1f);
                // _spriteBatch.DrawString(Textures.BasicFont, $"[{x},{y}]", tile.Bounds.Center.ToVector2(), Color.Black);
            }
        }
    }

    private void DrawPlayer()
    {
        var angle = _model.Player.Rotation;

        _spriteBatch.Draw(ViewResources.Tank,
            new Rectangle(_model.Player.Position.ToPoint(), _model.Player.Size.ToPoint()),
            null, Color.White, angle, ViewResources.Tank.Bounds.Center.ToVector2(),
            SpriteEffects.None, 0f);

        _spriteBatch.Draw(ViewResources.TankMuzzle,
            _model.Player.Position + new Vector2(ViewResources.TankMuzzleOffset.X * MathF.Sin(angle),
                -ViewResources.TankMuzzleOffset.Y * MathF.Cos(angle)), null, Color.White,
            angle, ViewResources.TankMuzzle.Bounds.Center.ToVector2(), Vector2.One, SpriteEffects.None, 0);

        _spriteBatch.DrawBox(_model.Player.Rectangle, Color.Blue);
    }

    private void DrawBullets()
    {
        foreach (var bullet in _model.Bullets)
        {
            _spriteBatch.Draw(ViewResources.Bullet, new Rectangle(bullet.Position.ToPoint(), bullet.Size.ToPoint()),
                null, Color.White, bullet.Rotation,
                ViewResources.Bullet.Bounds.Center.ToVector2(), SpriteEffects.None, 0f);

            // _spriteBatch.DrawBox(bullet.Rectangle, Color.Blue);
        }
    }
}