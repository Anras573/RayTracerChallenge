using System;

namespace RayTracerChallenge.Core.Shapes;

public class Cube : Shape
{
    public override Intersections LocalIntersects(Ray localRay)
    {
        var (xTMin, xTMax) = CheckAxis(localRay.Origin.X, localRay.Direction.X);
        var (yTMin, yTMax) = CheckAxis(localRay.Origin.Y, localRay.Direction.Y);
        var (zTMin, zTMax) = CheckAxis(localRay.Origin.Z, localRay.Direction.Z);

        var tMin = MathF.Max(xTMin, MathF.Max(yTMin, zTMin));
        var tMax = MathF.Min(xTMax, MathF.Min(yTMax, zTMax));

        return tMin > tMax
            ? new Intersections()
            : new Intersections(new Intersection(tMin, this), new Intersection(tMax, this));
    }

    private (float tMin, float tMax) CheckAxis(float origin, float direction)
    {
        var tMin = 0.0f;
        var tMax = 0.0f;
        
        var tMinNumerator = -1 - origin;
        var tMaxNumerator = 1 - origin;

        if (MathF.Abs(direction) >= Utilities.Epsilon)
        {
            tMin = tMinNumerator / direction;
            tMax = tMaxNumerator / direction;
        }
        else
        {
            tMin = tMinNumerator * float.NegativeInfinity;
            tMax = tMaxNumerator * float.NegativeInfinity;
        }

        if (tMin > tMax)
        {
            (tMin, tMax) = (tMax, tMin);
        }

        return (tMin, tMax);
    }

    public override Vector LocalNormalAt(Point localPoint)
    {
        var max = MathF.Max(
            MathF.Abs(localPoint.X), 
            MathF.Max(
                MathF.Abs(localPoint.Y), 
                MathF.Abs(localPoint.Z)));

        if (max == MathF.Abs(localPoint.X))
        {
            return new Vector(localPoint.X, 0.0f, 0.0f);
        }
        else if (max == MathF.Abs(localPoint.Y))
        {
            return new Vector(0.0f, localPoint.Y, 0.0f);
        }

        return new Vector(0.0f, 0.0f, localPoint.Z);
    }
}