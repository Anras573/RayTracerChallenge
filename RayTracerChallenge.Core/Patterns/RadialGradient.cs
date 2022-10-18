using System;

namespace RayTracerChallenge.Core.Patterns;

public class RadialGradient : Pattern
{
    public Pattern FirstPattern;
    public Pattern SecondPattern;

    public RadialGradient(Pattern first, Pattern second) : base(first.First, second.First)
    {
        FirstPattern = first;
        SecondPattern = second;
    }

    public override Color ColorAt(Point point)
    {
        var distance = MathF.Sqrt( MathF.Pow(point.X, 2f) + MathF.Pow(point.Z, 2f));
        var fraction = distance - MathF.Floor(distance);
           
        return FirstPattern.ColorAt(point) + (SecondPattern.ColorAt(point) - FirstPattern.ColorAt(point)) * fraction;
    }
}