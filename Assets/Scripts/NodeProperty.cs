using System;
using UnityEngine;

public abstract class NodeProperty
{
    public Vector2Int Point { get; }

    public abstract NodeType Type { get; }

    public NodeProperty(Vector2Int point)
    {
        Point = point;
    }

    public static NodeProperty GetProperty(Vector2Int point, TileType tileType)
    {
        switch (tileType)
        {
            case TileType.TileWhite: return new TileProperty(point, Color.White);
            case TileType.TileRed: return new TileProperty(point, Color.Red);
            case TileType.TileYellow: return new TileProperty(point, Color.Yellow);
            case TileType.TileGreen: return new TileProperty(point, Color.Green);
            case TileType.TileBlue: return new TileProperty(point, Color.Blue);

            case TileType.PaintWhite: return new PaintProperty(point, Color.White);
            case TileType.PaintRed: return new PaintProperty(point, Color.Red);
            case TileType.PaintYellow: return new PaintProperty(point, Color.Yellow);
            case TileType.PaintGreen: return new PaintProperty(point, Color.Green);
            case TileType.PaintBlue: return new PaintProperty(point, Color.Blue);

            case TileType.Wall: return new WallProperty(point);
            case TileType.Hole: return new HoleProperty(point);
            case TileType.Ice1: return new IceProperty(point, health: 1);
            case TileType.Ice2: return new IceProperty(point, health: 2);
            case TileType.Ice3: return new IceProperty(point, health: 3);

            case TileType.WarpGray: return new WarpProperty(point, Color.White, key: 1);
            case TileType.WarpRed: return new WarpProperty(point, Color.Red, key: 2);
            case TileType.WarpYellow: return new WarpProperty(point, Color.Yellow, key: 3);
            case TileType.WarpGreen: return new WarpProperty(point, Color.Green, key: 4);
            case TileType.WarpBlue: return new WarpProperty(point, Color.Blue, key: 5);

            case TileType.FinishOpened:
            case TileType.Finish: return new FinishProperty(point);

            default: throw new ArgumentException($"Invalid {nameof(TileType)} {tileType}");
        }
    }
}
