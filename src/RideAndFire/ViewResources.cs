using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RideAndFire;

public static class ViewResources
{
    public static Texture2D SandTile { get; private set; } = null!;
    public static SpriteFont BasicFont { get; private set; } = null!;
    public static Texture2D Tank { get; private set; } = null!;
    public static Texture2D TankMuzzle { get; private set; } = null!;
    public static Vector2 TankMuzzleOffset => new(TankMuzzle.Height / 2f);
    public static Vector2 TankMuzzleEndOffset => new(TankMuzzle.Height + 7f);
    public static Texture2D Bullet { get; private set; } = null!;

    public static void LoadContent(ContentManager contentManager)
    {
        SandTile = contentManager.Load<Texture2D>("Sprites/Tiles/sand");
        BasicFont = contentManager.Load<SpriteFont>("Fonts/basicFont");
        Tank = contentManager.Load<Texture2D>("Sprites/Tanks/tank");
        TankMuzzle = contentManager.Load<Texture2D>("Sprites/Tanks/tankMuzzle");
        Bullet = contentManager.Load<Texture2D>("Sprites/Projectiles/bullet");
    }
}