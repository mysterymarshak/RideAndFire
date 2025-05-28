using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RideAndFire.Extensions;
using RideAndFire.Models;

namespace RideAndFire.Views;

public class MapView : View
{
    private readonly TileModel[,] _map;

    public MapView(TileModel[,] map, SpriteBatch spriteBatch) : base(spriteBatch)
    {
        _map = map;
    }

    public override void Draw()
    {
        for (var x = 0; x < Constants.MapWidth; x++)
        {
            for (var y = 0; y < Constants.MapHeight; y++)
            {
                var tile = _map[x, y];
                var bounds = tile.Bounds;
                bounds.Offset(Constants.ScreenOffset);

                SpriteBatch.Draw(GetTileTexture(tile.Type), bounds, Color.White);
            }
        }
    }

    private static Texture2D GetTileTexture(TileType type) => type switch
    {
        TileType.Dirt => ViewResources.DirtTile,
        TileType.Sand => ViewResources.SandTile,
        TileType.Wall => ViewResources.WallTile,
        TileType.Turret => ViewResources.TurretTile,
        _ => throw new ArgumentOutOfRangeException(nameof(type), type, string.Empty)
    };
}