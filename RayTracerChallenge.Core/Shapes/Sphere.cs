using System;

namespace RayTracerChallenge.Core.Shapes;

public class Sphere : Shape
{
    public override Intersections LocalIntersects(Ray localRay)
    {
        var sphereToRay = localRay.Origin - Origin;
        var a = localRay.Direction.Dot(localRay.Direction);
        var b = 2 * localRay.Direction.Dot(sphereToRay);
        var c = sphereToRay.Dot(sphereToRay) - 1;

        var discriminant = MathF.Pow(b, 2) - 4 * a * c;

        if (discriminant < 0)
            return new Intersections();

        var t1 = (-b - MathF.Sqrt(discriminant)) / (2 * a);
        var t2 = (-b + MathF.Sqrt(discriminant)) / (2 * a);

        return new Intersections(
            new Intersection(MathF.Min(t1, t2), this),
            new Intersection(MathF.Max(t1, t2), this));
    }

    public override Vector LocalNormalAt(Point localPoint)
    {
        return localPoint - Origin;
    }
}