using Microsoft.Xna.Framework;

namespace RideAndFire;

public static class Constants
{
    public static readonly Vector2 ScreenOffset = new((ScreenWidth - MapWidth * TileSize) / 2f,
        (ScreenHeight - (MapHeight * TileSize)) / 2f);

    public static readonly Rectangle MapBounds =
        new(ScreenOffset.ToPoint(), (new Vector2(MapWidth, MapHeight) * TileSize).ToPoint());
    
    public const int ScreenWidth = 1920;
    public const int ScreenHeight = 1080;
    public const int TileSize = 64;
    public const int MapWidth = ScreenWidth / TileSize;
    public const int MapHeight = ScreenHeight / TileSize;

    public const float PlayerSpeed = 500f;
    public const float BulletSpeed = 1000f;
    public const float ReversePlayerSpeed = 400f;
    public const float RotationSpeedInRadians = 3f;
}