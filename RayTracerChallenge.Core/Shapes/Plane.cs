using System;

namespace RayTracerChallenge.Core.Shapes;

public class Plane : Shape
{
    public override Intersections LocalIntersects(Ray localRay)
    {
        if (MathF.Abs(localRay.Direction.Y) < Utilities.Epsilon)
        {
            return new Intersections();
        }

        var time = -localRay.Origin.Y / localRay.Direction.Y;

        return new Intersections(new Intersection(time, this));
    }

    public override Vector LocalNormalAt(Point localPoint)
    {
        return new Vector(0.0f, 1.0f, 0.0f);
    }

    public override Bounds GetBounds()
    {
        return new Bounds(new Point(float.NegativeInfinity, 0.0f, float.NegativeInfinity),
            new Point(float.PositiveInfinity, 0.0f, float.PositiveInfinity));
    }
}