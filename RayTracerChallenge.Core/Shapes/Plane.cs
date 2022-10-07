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
        return new Vector(0f, 1f, 0f);
    }
}