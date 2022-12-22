using UnityEngine;

public class SplashCross : Splash
{
    private const float SPLASH_DELAY = 0.035f;

    public override void ApplyMove()
    {
        base.ApplyMove();

        float delay = 0f;

        // left
        for (int x = Point.x - 1; x >= 0; x--)
        {
            if (IsSplashable(x, Point.y) == false) break;

            base.ApplySplash(new Vector2Int(x, Point.y), delay);

            delay += SPLASH_DELAY;
        }

        delay = 0f;
        // right
        for (int x = Point.x + 1; x < Board.WIDTH; x++)
        {
            if (IsSplashable(x, Point.y) == false) break;

            base.ApplySplash(new Vector2Int(x, Point.y), delay);

            delay += SPLASH_DELAY;
        }

        delay = 0f;
        // up
        for (int y = Point.y + 1; y < Board.HEIGHT; y++)
        {
            if (IsSplashable(Point.x, y) == false) break;

            base.ApplySplash(new Vector2Int(Point.x, y), delay);

            delay += SPLASH_DELAY;
        }

        delay = 0f;
        // down
        for (int y = Point.y - 1; y >= 0; y--)
        {
            if (IsSplashable(Point.x, y) == false) break;

            base.ApplySplash(new Vector2Int(Point.x, y), delay);

            delay += SPLASH_DELAY;
        }
    }
}
