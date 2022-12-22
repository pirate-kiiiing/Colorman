using System;
using UnityEngine;

[Serializable]
public enum Color
{
    White,

    Red,

    Yellow,

    Green,

    Blue,
}

public static class ColorExtension
{
    public static TileType GetTileType(this Color color)
    {
        switch (color)
        {
            case Color.White: return TileType.TileWhite;
            case Color.Red: return TileType.TileRed;
            case Color.Yellow: return TileType.TileYellow;
            case Color.Green: return TileType.TileGreen;
            case Color.Blue: return TileType.TileBlue;

            default: throw new ArgumentException($"Invalid {nameof(Color)} {color}");
        }
    }

    public static UnityEngine.Color ToUnityColor(this Color color)
    {
        switch (color)
        {
            //case Color.Gray: return new Color32(170, 170, 170, 255);
            case Color.White: return UnityEngine.Color.white;
            case Color.Red: return new Color32(239, 83, 80, 255);
            case Color.Yellow: return new Color32(251, 192, 45, 255);
            case Color.Green: return new Color32(98, 211, 26, 255);
            case Color.Blue: return new Color32(131, 197, 234, 255);
        }

        throw new ArgumentException($"Invalid {nameof(Color)} {color}");
    }

    public static Color LevelAdjustedColor(this Color color)
    {
        if (color == Color.White) return Color.White;

        int level = GameManager.Instance.Level;
        int totalLevels = GameManager.GetTotalLevelCount();

        int offset = ((level - 1) / totalLevels) % ((int)Color.Blue);

        if (offset == 0) return color;
        else if (offset == 1)
        {
            if (color == Color.Red) return Color.Yellow;
            if (color == Color.Yellow) return Color.Green;
            if (color == Color.Green) return Color.Blue;
            if (color == Color.Blue) return Color.Red;
        }
        else if (offset == 2)
        {
            if (color == Color.Red) return Color.Green;
            if (color == Color.Yellow) return Color.Blue;
            if (color == Color.Green) return Color.Red;
            if (color == Color.Blue) return Color.Yellow;
        }
        else if (offset == 3)
        {
            if (color == Color.Red) return Color.Blue;
            if (color == Color.Yellow) return Color.Red;
            if (color == Color.Green) return Color.Yellow;
            if (color == Color.Blue) return Color.Green;
        }

        throw new ArgumentException($"No such adjusted color");
    }
}