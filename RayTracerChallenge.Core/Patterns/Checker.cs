using System;

namespace RayTracerChallenge.Core.Patterns;

public class Checker : Pattern
{
    public Checker(Color first, Color second) : base(first, second)
    {
    }

    public override Color ColorAt(Point point)
    {
        return (MathF.Floor(point.X) + MathF.Floor(point.Y) + MathF.Floor(point.Z)) % 2 == 0 ? First : Second;
    }
}