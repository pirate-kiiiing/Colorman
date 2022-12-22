using System;
using UnityEngine;

public static class VectorExtension
{
    public static Vector2 ToVector2(this Vector3 vector)
    {
        return new Vector2(vector.x, vector.y);
    }

    public static Vector2Int ToVector2Int(this Vector3 vector)
    {
        return new Vector2Int((int) vector.x, (int) vector.y);
    }

    public static Vector2Int ToVector2(this Vector3Int vector)
    {
        return new Vector2Int(vector.x, vector.y);
    }

    public static Vector2Int ToUnitVector(this Vector2Int v)
    {
        if ((v.x == 0 && v.y == 0) || (v.x != 0 && v.y != 0))
        {
            throw new ArgumentException($"Cannot normalize {nameof(Vector2Int)} {v}");
        }

        return
            (v.x > 0) ? Vector2Int.right :
            (v.x < 0) ? Vector2Int.left :
            (v.y > 0) ? Vector2Int.up : Vector2Int.down;
    }

    public static Vector3 ToVector3(this Vector2 vector)
    {
        return new Vector3(vector.x, vector.y, 0);
    }

    public static Vector3Int ToVector3(this Vector2Int vector)
    {
        return new Vector3Int(vector.x, vector.y, 0);
    }
}
