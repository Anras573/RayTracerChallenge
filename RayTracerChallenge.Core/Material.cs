using System;
using RayTracerChallenge.Core.Patterns;
using RayTracerChallenge.Core.Shapes;

namespace RayTracerChallenge.Core
{
    public class Material :IEquatable<Material>
    {
        public static Material Default => new(Color.White, 0.1f, 0.9f, 0.9f, 200.0f);

        private Material(float ambient, float diffuse, float specular, float shininess)
        {
            Ambient = ambient;
            Diffuse = diffuse;
            Specular = specular;
            Shininess = shininess;
        }
        
        public Material(Color color, float ambient = 0.1f, float diffuse = 0.9f, float specular = 0.9f, float shininess = 200f)
            : this(ambient, diffuse, specular, shininess)
        {
            Color = color;
        }
        
        public Material(Pattern pattern, float ambient = 0.1f, float diffuse = 0.9f, float specular = 0.9f, float shininess = 200f)
            : this(ambient, diffuse, specular, shininess)
        {
            Pattern = pattern;
        }

        public Pattern Pattern;
        public Color Color;
        public float Ambient;
        public float Diffuse;
        public float Specular;
        public float Shininess;

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
            return Ambient.GetHashCode() ^ Color.GetHashCode() ^ Diffuse.GetHashCode() ^ Shininess.GetHashCode() ^ Specular.GetHashCode();
        }
    }
}
