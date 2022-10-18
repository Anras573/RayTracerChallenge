namespace RayTracerChallenge.Core.Patterns;

public class Solid : Pattern
{
    public Solid(Color color) : base(color, color)
    {
    }

    public override Color ColorAt(Point point) => First;
}