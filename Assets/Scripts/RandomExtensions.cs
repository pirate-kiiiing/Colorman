using System;

public static class RandomExtensions
{
    public static double NextDouble(this Random random, double min, double max)
    {
        return (float)random.NextDouble() * (max - min) + min;
    }
}
