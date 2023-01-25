using System;

namespace RayTracerChallenge.Core.Shapes;

public class Bounds
{
    public Point Minimum;
    public Point Maximum;

    public Bounds(Point minimum, Point maximum)
    {
        Minimum = minimum;
        Maximum = maximum;
    }
    
    public static Bounds Empty
        => new (
            new Point(float.PositiveInfinity,float.PositiveInfinity,float.PositiveInfinity),
            new Point(float.NegativeInfinity,float.NegativeInfinity,float.NegativeInfinity));
    
    public Bounds Transform(Matrix transform)
    {
        var bounds = Empty;
        
        foreach (var point in GetCorners())
            bounds += transform * point;

        return bounds;
    }

    public Point[] GetCorners()
    {
        return new[]
        {
            new Point(Minimum.X,Minimum.Y,Minimum.Z),
            new Point(Minimum.X,Minimum.Y,Maximum.Z),
            new Point(Minimum.X,Maximum.Y,Minimum.Z),
            new Point(Minimum.X,Maximum.Y,Maximum.Z),
            new Point(Maximum.X,Minimum.Y,Minimum.Z),
            new Point(Maximum.X,Minimum.Y,Maximum.Z),
            new Point(Maximum.X,Maximum.Y,Minimum.Z),
            new Point(Maximum.X,Maximum.Y,Maximum.Z)
        };
    }
    
    public bool Intersects(Ray ray)
    {
        var (xTMin, xTMax) = CheckAxis(ray.Origin.X, ray.Direction.X, Minimum.X, Maximum.X);
        var (yTMin, yTMax) = CheckAxis(ray.Origin.Y, ray.Direction.Y, Minimum.Y, Maximum.Y);
        var (zTMin, zTMax) = CheckAxis(ray.Origin.Z, ray.Direction.Z, Minimum.Z, Maximum.Z);

        var tMin = MathF.Max(xTMin, MathF.Max(yTMin, zTMin));
        var tMax = MathF.Min(xTMax, MathF.Min(yTMax, zTMax));

        return !(tMin > tMax);
    }

    private static (float, float) CheckAxis(float origin, float direction, float min, float max)
    {
        var tMinNumerator = min - origin;
        var tMaxNumerator = max - origin;

        float tMax, tMin;
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
        
        return tMin > tMax
            ? (tMax, tMin)
            : (tMin, tMax);
    }
    
    public static Bounds operator +(Bounds left, Bounds right)
    {
        var min = new Point(MathF.Min(left.Minimum.X, right.Minimum.X),
            MathF.Min(left.Minimum.Y, right.Minimum.Y),
            MathF.Min(left.Minimum.Z, right.Minimum.Z));

        var max = new Point(MathF.Max(left.Maximum.X, right.Maximum.X),
            MathF.Max(left.Maximum.Y, right.Maximum.Y),
            MathF.Max(left.Maximum.Z, right.Maximum.Z));

        return new Bounds(min, max);
    }
    
    public static Bounds operator +(Bounds a, Point p)
    {
        var min = new Point(MathF.Min(a.Minimum.X, p.X),
            MathF.Min(a.Minimum.Y, p.Y),
            MathF.Min(a.Minimum.Z, p.Z));

        var max = new Point(MathF.Max(a.Maximum.X, p.X),
            MathF.Max(a.Maximum.Y, p.Y),
            MathF.Max(a.Maximum.Z, p.Z));

        return new Bounds(min, max);
    }
}