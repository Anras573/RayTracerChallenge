using System;
using System.Collections.Generic;
using System.Linq;
using RayTracerChallenge.Core.Shapes;

namespace RayTracerChallenge.Core;

public class World
{
    public List<Light> Lights = new();
    public List<Shape> Objects = new();

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
            Lights = new List<Light> { light },
            Objects = new List<Shape>
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

            intersections.AddRange(temp);
        }

        return new Intersections(intersections.ToArray());
    }

    public Color ShadeHit(Computation computation, int remainingCalls = 5)
    {
        var surfaceColor = Color.Black;

        foreach (var light in Lights)
        {
            var isShadowed = IsShadowed(computation.OverPoint, light);
            surfaceColor += computation.Object.Material.Lightning(
                computation.Object,
                light,
                computation.OverPoint,
                computation.EyeVector,
                computation.NormalVector,
                isShadowed);
        }

        var reflectedColor = ReflectedColor(computation, remainingCalls);
        var refractedColor = RefractedColor(computation, remainingCalls);

        if (!(computation.Object.Material.Reflective > 0.0f) || !(computation.Object.Material.Transparency > 0.0f))
        {
            return surfaceColor + reflectedColor + refractedColor;
        }
        
        var reflectance = computation.Schlick();

        return surfaceColor + reflectedColor * reflectance + refractedColor * (1.0f - reflectance);

    }

    public Color ColorAt(Ray ray, int remainingCalls = 5)
    { 
        var intersections = Intersect(ray);

        var hit = intersections.Hit();

        if (hit == null)
        {
            return Color.Black;
        }

        var computation = new Computation(hit, ray);

        return ShadeHit(computation, remainingCalls);
    }

    public bool IsShadowed(Point point)
    {
        return IsShadowed(point, Lights.First());
    }

    private bool IsShadowed(Point point, Light light)
    {
        var v = light.Position - point;
        var distance = v.Magnitude();
        var direction = v.Normalize();

        var ray = new Ray(point, direction);
        var intersections = Intersect(ray);

        var hit = intersections.Hit();

        return hit is { } && hit.TimeValue < distance;
    }

    public Color ReflectedColor(Computation computations, int remainingCalls = 5)
    {
        if (computations.Object.Material.Reflective == 0 || remainingCalls == 0)
            return Color.Black;

        var reflectRay = new Ray(computations.OverPoint, computations.ReflectVector);
        var color = ColorAt(reflectRay, --remainingCalls);
            
        return color * computations.Object.Material.Reflective;
    }

    public Color RefractedColor(Computation computation, int remainingCalls = 5)
    {
        if (computation.Object.Material.Transparency == 0.0f || remainingCalls == 0)
        {
            return Color.Black;
        }

        var nRatio = computation.N1 / computation.N2;
        var cosI = computation.EyeVector.Dot(computation.NormalVector);
        var sin2T = MathF.Pow(nRatio, 2) * (1.0f - MathF.Pow(cosI, 2));

        if (sin2T > 1.0f)
        {
            return Color.Black;
        }

        var cosT = MathF.Sqrt(1.0f - sin2T);
        var direction = computation.NormalVector * (nRatio * cosI - cosT) - computation.EyeVector * nRatio;
        var refractRay = new Ray(computation.UnderPoint, direction);

        var color = ColorAt(refractRay, --remainingCalls) * computation.Object.Material.Transparency;
            
        return color;
    }
}