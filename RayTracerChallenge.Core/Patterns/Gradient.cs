using System;

namespace RayTracerChallenge.Core.Patterns;

public class Gradient : Pattern
{
    public Gradient(Color first, Color second) : base(first, second)
    {
    }

    public override Color ColorAt(Point point)
    {
        var distance = Second - First;
        var fraction = point.X - MathF.Floor(point.X);

        return First + distance * fraction;
    }
}