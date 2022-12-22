using System;
using System.Collections.Generic;
using System.Linq;

public static class IEnumerableExtension
{
    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> enumerable)
    {
        IList<T> list = enumerable.ToList();
        Random r = new Random();

        for (int i = list.Count - 1; i > 0; i--)
        {
            int swap = r.Next(i + 1);
            Swap(list, swap, i);
        }

        return list;
    }

    private static void Swap<T>(IList<T> list, int a, int b)
    {
        T tmp = list[a];
        list[a] = list[b];
        list[b] = tmp;
    }
}
