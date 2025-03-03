using System;
using System.Collections.Generic;

namespace Core.Helpers;

public static class ListExtensions
{
    public static T Random<T>(this List<T> list)
    {
        if (list == null || list.Count == 0)
        {
            throw new InvalidOperationException("Cannot get a random item from an empty list.");
        }

        return list[JRandom.Next(list.Count)];
    }
}
