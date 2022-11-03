using System;
using RayTracerChallenge.Core.Patterns;
using RayTracerChallenge.Core.Shapes;

namespace RayTracerChallenge.Core;

public class Material :IEquatable<Material>
{
    public static Material Default => new(Color.White);

    public static Material Glass => new(Color.White)
    {
        Transparency = 1.0f,
        RefractiveIndex = Utilities.RefractiveIndex.Glass
    };

    public Material(Color color)
    {
        Color = color;
    }
        
    public Material(Pattern pattern)
    {
        Pattern = pattern;
    }

    public Pattern Pattern;
    public Color Color;
    public float Ambient = 0.1f;
    public float Diffuse = 0.9f;
    public float Specular = 0.9f;
    public float Shininess = 200f;
    public float Reflective = 0.0f;
    public float Transparency = 0.0f;
    public float RefractiveIndex = Utilities.RefractiveIndex.Vacuum;

    public Color Lightning(Shape shape, Light light, Point position, Vector eyeV, Vector normalV, bool inShadow = false)
    {
        Color = Pattern != null ? Pattern.ColorAtShape(shape, position) : Color;

        var effectiveColor = Color * light.Intensity;
        var ambient = effectiveColor * Ambient;

        if (inShadow)
        {
            return ambient;
        }

        var lightV = (light.Position - position).Normalize();
        var diffuse = Color.Black;
        var specular = Color.Black;

        var lightDotNormal = lightV.Dot(normalV);

        if (lightDotNormal >= 0f)
        {
            diffuse = effectiveColor * Diffuse * lightDotNormal;

            var reflectV = -lightV.Reflect(normalV);
            var reflectDotEye = reflectV.Dot(eyeV);

            if (reflectDotEye > 0f)
            {
                var factor = MathF.Pow(reflectDotEye, Shininess);
                specular = light.Intensity * Specular * factor;
            }
        }

        return ambient + diffuse + specular;
    }

    public bool Equals(Material other)
    {
        return other.Ambient.Equals(Ambient)
               && other.Color.Equals(Color)
               && other.Diffuse.Equals(Diffuse)
               && other.Shininess.Equals(Shininess)
               && other.Specular.Equals(Specular);
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as Material);
    }

    public override int GetHashCode()
    {
        return Ambient.GetHashCode()
               ^ Color.GetHashCode()
               ^ Diffuse.GetHashCode()
               ^ Shininess.GetHashCode()
               ^ Specular.GetHashCode()
               ^ Reflective.GetHashCode();
    }
}