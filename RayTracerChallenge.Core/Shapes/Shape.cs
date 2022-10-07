using System;

namespace RayTracerChallenge.Core.Shapes
{
    public abstract class Shape
    {
        public Point Origin = new(0f, 0f, 0f);
        public Matrix Transform = Matrix.IdentityMatrix();
        public Material Material = Material.Default;
        public Guid Id { get; } = Guid.NewGuid();

        public abstract Intersections LocalIntersects(Ray localRay);
        public abstract Vector LocalNormalAt(Point localPoint);

        public Intersections Intersects(Ray ray)
        {
            var localRay = ray.Transform(Transform.Inverse());
            return LocalIntersects(localRay);
        }

        public Vector NormalAt(Point worldPoint)
        {
            var localPoint = Transform.Inverse() * worldPoint;
            var localNormal = LocalNormalAt(localPoint);
            var worldNormal = Transform.Transpose().Inverse() * localNormal;

            return worldNormal.Normalize();
        }
    }
}
