using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RideAndFire;

public static class ViewResources
{
    public static Texture2D DirtTile { get; private set; } = null!;
    public static Texture2D SandTile { get; private set; } = null!;
    public static Texture2D WallTile { get; private set; } = null!;
    public static Texture2D TurretTile { get; private set; } = null!;
    public static SpriteFont BasicFont { get; private set; } = null!;
    public static SpriteFont ComicSansFont { get; private set; } = null!;
    public static Texture2D Tank { get; private set; } = null!;
    public static Texture2D TankMuzzle { get; private set; } = null!;
    public static Vector2 TankMuzzleOffset => new(TankMuzzle.Height / 2f);
    public static Vector2 TankMuzzleEndOffset => new(TankMuzzle.Height + 7f);
    public static Texture2D Bullet { get; private set; } = null!;
    public static Texture2D TurretBase { get; private set; } = null!;
    public static Texture2D Turret { get; private set; } = null!;
    public static Vector2 TurretOffset => new(Turret.Height / 6f);
    public static Vector2 TurretMuzzleEndOffset => new(2 * Turret.Height / 3f);
    public static Texture2D Button { get; private set; } = null!;
    public static Texture2D ButtonPressed { get; private set; } = null!;
    
    public static void LoadContent(ContentManager contentManager)
    {
        DirtTile = contentManager.Load<Texture2D>("Sprites/Tiles/dirt");
        SandTile = contentManager.Load<Texture2D>("Sprites/Tiles/sand");
        WallTile = contentManager.Load<Texture2D>("Sprites/Tiles/wall");
        TurretTile = contentManager.Load<Texture2D>("Sprites/Tiles/turretTile");
        BasicFont = contentManager.Load<SpriteFont>("Fonts/basic");
        ComicSansFont = contentManager.Load<SpriteFont>("Fonts/comicSans");
        Tank = contentManager.Load<Texture2D>("Sprites/Tanks/tank");
        TankMuzzle = contentManager.Load<Texture2D>("Sprites/Tanks/tankMuzzle");
        Bullet = contentManager.Load<Texture2D>("Sprites/Projectiles/bullet");
        TurretBase = contentManager.Load<Texture2D>("Sprites/Turrets/turretBase");
        Turret = contentManager.Load<Texture2D>("Sprites/Turrets/turret");
        Button = contentManager.Load<Texture2D>("UI/Buttons/button");
        ButtonPressed = contentManager.Load<Texture2D>("UI/Buttons/buttonPressed");
    }
}