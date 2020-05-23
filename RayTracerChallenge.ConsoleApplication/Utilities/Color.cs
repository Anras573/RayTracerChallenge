using Tuple = RayTracerChallenge.Core.Tuple;

namespace RayTracerChallenge.ConsoleApplication.Utilities
{
    public static class Color
    {
        public static Tuple White => Tuple.Color(255f, 255f, 255f);
        public static Tuple Black => Tuple.Color(0f, 0f, 0f);
        public static Tuple Red => Tuple.Color(255f, 0f, 0f);
        public static Tuple Green => Tuple.Color(0f, 255f, 0f);
        public static Tuple Blue => Tuple.Color(0f, 0f, 255f);
    }
}
