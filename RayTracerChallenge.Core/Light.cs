using System;

namespace RayTracerChallenge.Core
{
    public class Light : IEquatable<Light>
    {
        public Light(Point position, Color intensity)
        {
            Position = position;
            Intensity = intensity;
        }

        public Color Intensity { get; }
        public Point Position { get; }

        public bool Equals(Light other)
        {
            return Intensity.Equals(other.Intensity) && Position.Equals(other.Position);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Light);
        }

        public override int GetHashCode()
        {
            return Intensity.GetHashCode() * Position.GetHashCode();
        }
    }
}
