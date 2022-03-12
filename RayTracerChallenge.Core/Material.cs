using System;

namespace RayTracerChallenge.Core
{
    public class Material
    {
        public static Material Default => new(new Color(1f, 1f, 1f), 0.1f, 0.9f, 0.9f, 200.0f);

        public Material(Color color, float ambient, float diffuse, float specular, float shininess)
        {
            Color = color;
            Ambient = ambient;
            Diffuse = diffuse;
            Specular = specular;
            Shininess = shininess;
        }

        public Color Color;
        public float Ambient;
        public float Diffuse;
        public float Specular;
        public float Shininess;

        public Color Lightning(Light light, Point position, Vector eyeV, Vector normalV)
        {
            var effectiveColor = Color * light.Intensity;
            var lightV = (light.Position - position).Normalize();
            var ambient = effectiveColor * Ambient;
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
    }
}
