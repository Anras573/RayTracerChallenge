using System.Collections.Generic;
using System.Linq;

namespace RayTracerChallenge.ConsoleApplication.Utilities;

public static class Extensions
{
    public static IEnumerable<(int, TValue)> AsIndexable<TValue>(this List<TValue> list)
    {
        return list.Select((t, i) => (i: i, value: t));
    }

    public static float Clamp(this float value, float max, float min = 0f)
    {
        if (value > max) return max;
        if (value < min) return min;
        return value;
    }
}