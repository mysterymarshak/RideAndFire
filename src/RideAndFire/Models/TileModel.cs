using Microsoft.Xna.Framework;

namespace RideAndFire.Models;

public class TileModel : Model
{
    public Rectangle Bounds => new(X * Constants.TileSize, Y * Constants.TileSize, Constants.TileSize,
        Constants.TileSize);

    public required TileType Type { get; init; }
    public required int X { get; init; }
    public required int Y { get; init; }
}

public enum TileType
{
    None,

    Grass,

    Sand
}