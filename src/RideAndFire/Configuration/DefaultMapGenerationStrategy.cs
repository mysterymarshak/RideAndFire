using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using RideAndFire.Models;

namespace RideAndFire.Configuration;

public interface IMapGenerationStrategy
{
    TileModel[,] GenerateMap();
}

public class DefaultMapGenerationStrategy : IMapGenerationStrategy
{
    public TileModel[,] GenerateMap()
    {
        var map = new TileModel[Constants.MapWidth, Constants.MapHeight];

        for (var x = 0; x < Constants.MapWidth; x++)
        {
            for (var y = 0; y < Constants.MapHeight; y++)
            {
                var point = new Point(x, y);
                var tileType = TileType.Dirt;

                var dirtAreas = new List<Rectangle>
                {
                    new(2, 2, 2, 2),
                    new(Constants.MapWidth - 4, 2, 2, 2),
                    new(Constants.MapWidth - 4, Constants.MapHeight - 4, 2, 2),
                    new(2, Constants.MapHeight - 4, 2, 2),
                };
                // turret locations actually; todo improve 

                if (dirtAreas.Any(z => z.Contains(point)))
                {
                    tileType = TileType.Turret;
                }

                var wallAreas = new List<Rectangle>
                {
                    new(0, 0, Constants.MapWidth, 1),
                    new(0, Constants.MapHeight - 1, Constants.MapWidth, 1),
                    new(0, 0, 1, Constants.MapHeight),
                    new(Constants.MapWidth - 1, 0, 1, Constants.MapHeight)
                };

                if (wallAreas.Any(z => z.Contains(point)))
                {
                    tileType = TileType.Wall;
                }

                map[x, y] = new TileModel { Type = tileType, X = x, Y = y };
            }
        }

        return map;
    }
}