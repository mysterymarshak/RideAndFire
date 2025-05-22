using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
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

    public GameView(GameModel model, SpriteBatch spriteBatch)
    {
        _model = model;
        _spriteBatch = spriteBatch;
    }

    public override void Draw()
    {
        DrawMap();
        DrawPlayer();
        DrawTurrets();
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

                _spriteBatch.Draw(GetTileTexture(tile.Type), bounds, Color.White);
                // _spriteBatch.DrawBox(bounds, Color.Red, thickness: 1f);
                // _spriteBatch.DrawString(Textures.BasicFont, $"[{x},{y}]", tile.Bounds.Center.ToVector2(), Color.Black);
            }
        }

        static Texture2D GetTileTexture(TileType type) => type switch
        {
            TileType.Dirt => ViewResources.DirtTile,
            TileType.Sand => ViewResources.SandTile,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, string.Empty)
        };
    }

    private void DrawPlayer()
    {
        var player = _model.Player;
        var angle = player.Rotation;

        _spriteBatch.Draw(ViewResources.Tank,
            new Rectangle(player.Position.ToPoint(), player.Size),
            null, Color.White, angle, ViewResources.Tank.Bounds.Center.ToVector2(),
            SpriteEffects.None, 0f);

        _spriteBatch.Draw(ViewResources.TankMuzzle,
            player.Position + new Vector2(ViewResources.TankMuzzleOffset.X * MathF.Sin(angle),
                -ViewResources.TankMuzzleOffset.Y * MathF.Cos(angle)), null, Color.White,
            angle, ViewResources.TankMuzzle.Bounds.Center.ToVector2(), Vector2.One, SpriteEffects.None, 0);

        _spriteBatch.DrawBox(player.Rectangle, Color.Blue);
        _spriteBatch.DrawRectangle(
            new Rectangle(player.Rectangle.Location + new Point(0, -40), new Size(player.Rectangle.Width, 10)),
            Color.Gray);
        _spriteBatch.FillRectangle(
            new RectangleF(player.Rectangle.Location.ToVector2() + new Vector2(1, -39),
                new SizeF((player.Rectangle.Width - 1) * (player.Health / player.MaxHealth), 9)), Color.Green);

        // todo: extract & improve
    }

    private void DrawTurrets()
    {
        foreach (var turret in _model.Turrets)
        {
            var angle = turret.Rotation;
            var maskColor = turret.IsDead ? Color.LightSlateGray : Color.White;

            _spriteBatch.Draw(ViewResources.TurretBase, new Rectangle(turret.Position.ToPoint(), turret.Size),
                null, maskColor, 0f, ViewResources.TurretBase.Bounds.Center.ToVector2(),
                SpriteEffects.None, 0f);

            _spriteBatch.Draw(ViewResources.Turret, new Rectangle(turret.Position.ToPoint() + new Vector2(
                    ViewResources.TurretOffset.X * MathF.Sin(angle),
                    -ViewResources.TurretOffset.Y * MathF.Cos(angle)).ToPoint(), turret.Size), null,
                maskColor, angle,
                ViewResources.Turret.Bounds.Center.ToVector2(), SpriteEffects.None, 0f);

            if (turret is { IsActive: true, IsDead: false })
            {
                _spriteBatch.DrawBox(turret.Rectangle, Color.Blue);
                SpriteBatchExtensions.DrawLine(_spriteBatch, turret.Position,
                    turret.Position +
                    Vector2.TransformNormal(new Vector2(0, -1000), Matrix.CreateRotationZ(turret.Rotation)),
                    Color.Red);
                _spriteBatch.DrawBox(
                    new Rectangle(
                        (turret.Position + new Vector2(ViewResources.TurretMuzzleEndOffset.X * MathF.Sin(angle),
                            -ViewResources.TurretMuzzleEndOffset.Y * MathF.Cos(angle))).ToPoint(), new Point(8, 8)),
                    Color.Brown);
            }

            // todo: extract & improve
        }
    }

    private void DrawBullets()
    {
        foreach (var bullet in _model.Bullets)
        {
            _spriteBatch.Draw(ViewResources.Bullet, new Rectangle(bullet.Position.ToPoint(), bullet.Size),
                null, Color.White, bullet.Rotation,
                ViewResources.Bullet.Bounds.Center.ToVector2(), SpriteEffects.None, 0f);

            _spriteBatch.DrawBox(bullet.Rectangle, Color.Green);
        }
    }
}