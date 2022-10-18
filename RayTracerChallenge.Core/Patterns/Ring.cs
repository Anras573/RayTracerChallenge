using System;

namespace RayTracerChallenge.Core.Patterns;

public class Ring : Pattern
{
    public Ring(Color first, Color second) : base(first, second)
    {
    }

    public override Color ColorAt(Point point)
    {
        return MathF.Floor(MathF.Sqrt(MathF.Pow(point.X, 2f) + MathF.Pow(point.Z, 2f))) % 2 == 0 ? First : Second;
    }
}