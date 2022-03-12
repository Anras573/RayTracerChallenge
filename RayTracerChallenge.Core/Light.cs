namespace RayTracerChallenge.Core
{
    public class Light
    {
        public Light(Point position, Color intensity)
        {
            Position = position;
            Intensity = intensity;
        }

        public Color Intensity { get; }
        public Point Position { get; }
    }
}
