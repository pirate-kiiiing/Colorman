using System;
using UnityEngine;

public class SplashSquare : Splash
{
    private const float SPLASH_DELAY = 0.015f;

    public override void ApplyMove()
    {
        base.ApplyMove();

        float delay = 0f;

        for (int y = Math.Max(0, Point.y - 1); y <= Math.Min(Board.HEIGHT - 1, Point.y + 1); y++)
        {
            for (int x = Math.Max(0, Point.x - 1); x <= Math.Min(Board.WIDTH - 1, Point.x + 1); x++)
            {
                if (x == Point.x && y == Point.y) continue;

                base.ApplySplash(new Vector2Int(x, y), delay);

                delay += SPLASH_DELAY;
            }
        }
    }
}
