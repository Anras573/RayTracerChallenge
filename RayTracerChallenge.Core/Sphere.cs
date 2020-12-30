using System;

namespace RayTracerChallenge.Core
{
    public class Sphere
    {
        public Point Origin;
        public Guid Id { get; }
        public Matrix Transform;

        public Sphere()
        {
            Origin = new Point(0f, 0f, 0f);
            Id = Guid.NewGuid();
            Transform = Matrix.IdentityMatrix();
        }

        public Intersections Intersects(Ray ray)
        {
            var ray2 = ray.Transform(Transform.Inverse());

            var sphereToRay = ray2.Origin - Origin;
            var a = ray2.Direction.Dot(ray2.Direction);
            var b = 2 * ray2.Direction.Dot(sphereToRay);
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
    }
}
