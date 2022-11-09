using System;

namespace RayTracerChallenge.Core.Patterns;

public class RadialGradient : Pattern
{
    private readonly Pattern _firstPattern;
    private readonly Pattern _secondPattern;

    public RadialGradient(Pattern first, Pattern second) : base(first.First, second.First)
    {
        _firstPattern = first;
        _secondPattern = second;
    }

    public override Color ColorAt(Point point)
    {
        var distance = MathF.Sqrt( MathF.Pow(point.X, 2f) + MathF.Pow(point.Z, 2f));
        var fraction = distance - MathF.Floor(distance);
           
        return _firstPattern.ColorAt(point) + (_secondPattern.ColorAt(point) - _firstPattern.ColorAt(point)) * fraction;
    }
}