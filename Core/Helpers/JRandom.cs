using System;

namespace Core.Helpers;

public class JRandom
{
    public static Random Instance { get; } = new Random();

    private static readonly object _lock = new();

    public static int Next(int maxExclusive)
    {
        lock (_lock)
        {
            return Instance.Next(maxExclusive);
        }
    }
}
