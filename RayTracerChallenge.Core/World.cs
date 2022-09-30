using System.Collections.Generic;
using System.Linq;

namespace RayTracerChallenge.Core
{
    public class World
    {
        public List<Light> Lights = new();
        public List<Sphere> Objects = new();

        public static World Default()
        {
            var light = new Light(new Point(-10f, 10f, -10f), Color.White);
            
            var sphere1 = new Sphere
            {
                Material =
                {
                    Color = new Color(0.8f, 1.0f, 0.6f),
                    Diffuse = 0.7f,
                    Specular = 0.2f
                }
            };

            var sphere2 = new Sphere
            {
                Transform = Matrix.Scale(0.5f, 0.5f, 0.5f)
            };

            return new World()
            {
                Lights = new() { light },
                Objects = new()
                {
                    sphere1,
                    sphere2
                }
            };
        }

        public Intersections Intersect(Ray ray)
        {
            var intersections = new List<Intersection>();

            foreach (var worldObject in Objects)
            {
                var temp = worldObject.Intersects(ray);
                
                if (temp.Length > 0)
                {
                    intersections.Add(temp[0]);
                    intersections.Add(temp[1]);
                }
            }

            return new Intersections(intersections.ToArray());
        }

        public Color ShadeHit(Computation computation)
        {
            var color = Color.Black;

            foreach (var light in Lights)
            {
                var isShadowed = IsShadowed(computation.OverPoint, light);
                color += computation.Object.Material.Lightning(light, computation.OverPoint, computation.EyeVector, computation.NormalVector, isShadowed);
            }

            return color;
        }

        public Color ColorAt(Ray ray)
        {
            var intersections = Intersect(ray);

            var hit = intersections.Hit();

            if (hit == null)
            {
                return Color.Black;
            }

            var computation = new Computation(hit, ray);

            return ShadeHit(computation);
        }

        public bool IsShadowed(Point point)
        {
            return IsShadowed(point, Lights.First());
        }

        public bool IsShadowed(Point point, Light light)
        {
            var v = light.Position - point;
            var distance = v.Magnitude();
            var direction = v.Normalize();

            var ray = new Ray(point, direction);
            var intersections = Intersect(ray);

            var hit = intersections.Hit();

            return hit is { } && hit.TimeValue < distance;
        }
    }
}
