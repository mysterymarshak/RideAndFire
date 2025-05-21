using Microsoft.Xna.Framework;

namespace RideAndFire;

public static class Constants
{
    public static Rectangle MapBounds => new(TileSize * 1, TileSize * 1, MapWidth * TileSize - TileSize * 1, MapHeight * TileSize - TileSize * 1);

    public const int ScreenWidth = 2560;
    public const int ScreenHeight = 1440;
    public const int MapWidth = ScreenWidth / 64;
    public const int TileSize = 64;
    public const int MapHeight = ScreenHeight / 64;
    
    public const float PlayerSpeed = 500f;
    public const float BulletSpeed = 1000f;
    public const float ReversePlayerSpeed = 400f;
    public const float RotationSpeedInRadians = 3f;
}