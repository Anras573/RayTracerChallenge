using System;

namespace RayTracerChallenge.Core.Shapes;

public class Cylinder : Shape
{
    public float Minimum = float.NegativeInfinity;
    public float Maximum = float.PositiveInfinity;
    public bool Closed;

    public override Intersections LocalIntersects(Ray localRay)
    {
        var intersections = new Intersections();
        
        var a = MathF.Pow(localRay.Direction.X, 2.0f) + MathF.Pow(localRay.Direction.Z, 2.0f);

        if (MathF.Abs(a) < Utilities.Epsilon)
        {
            IntersectCaps(localRay, intersections);
            return intersections;
        }

        var b = 2 * localRay.Origin.X * localRay.Direction.X +
                2 * localRay.Origin.Z * localRay.Direction.Z;
        var c = MathF.Pow(localRay.Origin.X, 2.0f) + MathF.Pow(localRay.Origin.Z, 2.0f) - 1.0f;

        var discriminant = MathF.Pow(b, 2.0f) - 4.0f * a * c;

        if (discriminant < 0.0f)
        {
            return intersections;
        }

        var t0 = (-b - MathF.Sqrt(discriminant)) / (2.0f * a);
        var t1 = (-b + MathF.Sqrt(discriminant)) / (2.0f * a);
        
        if (t0 > t1)
        {
            (t0, t1) = (t1, t0);
        }
        
        var y0 = localRay.Origin.Y + t0 * localRay.Direction.Y;
        var y1 = localRay.Origin.Y + t1 * localRay.Direction.Y;

        if (Minimum < y0 && y0 < Maximum)
        {
            intersections.Add(new Intersection(t0, this));
        }

        if (Minimum < y1 && y1 < Maximum)
        {
            intersections.Add(new Intersection(t1, this));
        }
        
        IntersectCaps(localRay, intersections);
        
        return intersections;
    }

    private static bool CheckCap(Ray ray, float t)
    {
        var x = ray.Origin.X + t * ray.Direction.X;
        var z = ray.Origin.Z + t * ray.Direction.Z;

        return MathF.Pow(x, 2.0f) + MathF.Pow(z, 2.0f) - 1.0f <= Utilities.Epsilon;
    }

    private void IntersectCaps(Ray ray, Intersections intersections)
    {
        if (!Closed || MathF.Abs(ray.Direction.Y) < Utilities.Epsilon)
        {
            return;
        }

        var t = (Minimum - ray.Origin.Y) / ray.Direction.Y;
        if (CheckCap(ray, t))
        {
            intersections.Add(new Intersection(t, this));
        }

        t = (Maximum - ray.Origin.Y) / ray.Direction.Y;
        if (CheckCap(ray, t))
        {
            intersections.Add(new Intersection(t, this));
        }
    }

    public override Vector LocalNormalAt(Point localPoint)
    {
        var distance = MathF.Pow(localPoint.X, 2.0f) + MathF.Pow(localPoint.Z, 2.0f);

        return distance switch
        {
            < 1.0f when localPoint.Y >= Maximum - Utilities.Epsilon => new Vector(0.0f, 1.0f, 0.0f),
            < 1.0f when localPoint.Y <= Minimum + Utilities.Epsilon => new Vector(0.0f, -1.0f, 0.0f),
            _ => new Vector(localPoint.X, 0.0f, localPoint.Z)
        };
    }
}