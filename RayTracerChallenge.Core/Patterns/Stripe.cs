using System;

namespace RayTracerChallenge.Core.Patterns;

public class Stripe : Pattern
{
    public Stripe(Color first, Color second) : base(first, second)
    {
    }
    
    public override Color ColorAt(Point point)
    {
        return MathF.Floor(point.X) % 2 == 0 ? First : Second;
    }

}