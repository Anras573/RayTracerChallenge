namespace RayTracerChallenge.Core;

public static class Utilities
{
    public const float Epsilon = 0.0025f;
    
    /// <summary>
    /// Makes sure the value is not greater than 1, or less than 0.
    /// </summary>
    /// <param name="color">The R, G, or B value of a color</param>
    /// <returns>
    /// 1.0f if <paramref name="color"/> is greater than 1.0f.
    /// 0.0f if <paramref name="color"/> is less than 0.0f,
    /// else returns the value of <paramref name="color"/>
    /// </returns>
    public static float ClampColor(float color)
    {
        return color switch
        {
            > 1.0f => 1.0f,
            < 0.0f => 0.0f,
            _ => color
        };
    }

    public static class RefractiveIndex
    {
        public const float Vacuum = 1.0f;
        public const float Air = 1.00029f;
        public const float Water = 1.333f;
        public const float Glass = 1.52f;
        public const float Diamond = 2.417f;
    }
}