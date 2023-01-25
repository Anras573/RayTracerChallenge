using System;

namespace RayTracerChallenge.Core.Shapes;

public abstract class Shape
{
    public readonly Point Origin = new(0f, 0f, 0f);
    public Matrix Transform = Matrix.IdentityMatrix();
    public Material Material = Material.Default;
    public Guid Id { get; } = Guid.NewGuid();
    public Shape Parent = null;

    public abstract Intersections LocalIntersects(Ray localRay);
    public abstract Vector LocalNormalAt(Point localPoint);
    public abstract Bounds GetBounds();
    public Bounds GetParentSpaceBounds => GetBounds().Transform(Transform);

    public Intersections Intersects(Ray ray)
    {
        var localRay = ray.Transform(Transform.Inverse());
        return LocalIntersects(localRay);
    }

    public Vector NormalAt(Point worldPoint)
    {
        var localPoint = WorldToObjectSpace(worldPoint);
        var localNormal = LocalNormalAt(localPoint);

        return NormalToWorldSpace(localNormal);
    }

    public Point WorldToObjectSpace(Point worldPoint)
    {
        if (Parent is not null)
            worldPoint = Parent.WorldToObjectSpace(worldPoint);

        return Transform.Inverse() * worldPoint;
    }

    public Vector NormalToWorldSpace(Vector normal)
    {
        normal = Transform.Inverse().Transpose() * normal;
        normal = new Vector(normal.X, normal.Y, normal.Z);
        normal = normal.Normalize();

        if (Parent is not null)
            normal = Parent.NormalToWorldSpace(normal);

        return normal;
    }
}