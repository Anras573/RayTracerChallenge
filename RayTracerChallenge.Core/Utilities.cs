namespace RayTracerChallenge.Core;

public static class Utilities
{
    public const float Epsilon = 0.0025f;

    public static class RefractiveIndex
    {
        public const float Vacuum = 1.0f;
        public const float Air = 1.00029f;
        public const float Water = 1.333f;
        public const float Glass = 1.52f;
        public const float Diamond = 2.417f;
    }
}