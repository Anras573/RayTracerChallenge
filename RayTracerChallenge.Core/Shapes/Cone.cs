using System;

namespace RayTracerChallenge.Core.Shapes;

public class Cone : Shape
{
    public float Minimum = float.NegativeInfinity;
    public float Maximum = float.PositiveInfinity;
    public bool Closed;
 
    public override Intersections LocalIntersects(Ray localRay)
    {
        var intersections = new Intersections();
        
        var a = MathF.Pow(localRay.Direction.X, 2.0f)
                - MathF.Pow(localRay.Direction.Y, 2.0f)
                + MathF.Pow(localRay.Direction.Z, 2.0f);
        var b = 2.0f * localRay.Origin.X * localRay.Direction.X
                - 2.0f * localRay.Origin.Y * localRay.Direction.Y
                + 2.0f * localRay.Origin.Z * localRay.Direction.Z;
        var c = MathF.Pow(localRay.Origin.X, 2.0f)
                - MathF.Pow(localRay.Origin.Y, 2.0f)
                + MathF.Pow(localRay.Origin.Z, 2.0f);
        
        if (MathF.Abs(a) <= Utilities.Epsilon)
        {
            if (MathF.Abs(b) <= Utilities.Epsilon)
            {
                return intersections;
            }
        
            var t = -c / (2.0f * b);
            intersections.Add(new Intersection(t, this));
        }
        
        var discriminant = MathF.Pow(b, 2.0f) - 4.0f * a * c;

        if (MathF.Abs(discriminant) < Utilities.Epsilon)
        {
            discriminant = 0.0f;
        }
        
        if (discriminant < 0.0f)
            return intersections;

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
    
    private static bool CheckCap(Ray ray, float t, float radius)
    {
        var x = ray.Origin.X + t * ray.Direction.X;
        var z = ray.Origin.Z + t * ray.Direction.Z;

        return MathF.Pow(x, 2.0f) + MathF.Pow(z, 2.0f) - radius <= Utilities.Epsilon;
        //return MathF.Round(MathF.Pow(x, 2.0f) + MathF.Pow(z, 2.0f), 4) <= radius;
    }

    private void IntersectCaps(Ray ray, Intersections intersections)
    {
        if (!Closed || MathF.Abs(ray.Direction.Y) < Utilities.Epsilon)
        {
            return;
        }

        var t = (Minimum - ray.Origin.Y) / ray.Direction.Y;
        if (CheckCap(ray, t, MathF.Abs(Minimum)))
        {
            intersections.Add(new Intersection(t, this));
        }

        t = (Maximum - ray.Origin.Y) / ray.Direction.Y;
        if (CheckCap(ray, t, MathF.Abs(Maximum)))
        {
            intersections.Add(new Intersection(t, this));
        }
    }

    public override Vector LocalNormalAt(Point localPoint)
    {
        var distance = MathF.Pow(localPoint.X, 2.0f) + MathF.Pow(localPoint.Z, 2.0f) + Utilities.Epsilon;

        var top = MathF.Abs(Maximum);
        var bottom = MathF.Abs(Minimum);

        if (distance < top && localPoint.Y >= Maximum - Utilities.Epsilon)
        {
            return new Vector(0, 1, 0);
        }
        
        if (distance < bottom && localPoint.Y <= Minimum + Utilities.Epsilon)
        {
            return new Vector(0, -1, 0);
        }
        
        var y = MathF.Sqrt(MathF.Pow(localPoint.X, 2.0f) + MathF.Pow(localPoint.Z, 2.0f));
        
        if (localPoint.Y > 0.0f)
        {
            y *= -1.0f;
        }
        
        return new Vector(localPoint.X, y, localPoint.Z);
    }
}