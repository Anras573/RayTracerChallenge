using System;

namespace RayTracerChallenge.Core
{
    public class Tuple
    {
        public float X;
        public float Y;
        public float Z;
        public float W;

        public static readonly float VectorIndicator = 0f;
        public static readonly float PointIndicator = 1f;

        public Tuple(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public bool Equals(Tuple tuple)
        {
            return MathF.Abs(X - tuple.X) < float.Epsilon
                && MathF.Abs(Y - tuple.Y) < float.Epsilon
                && MathF.Abs(Z - tuple.Z) < float.Epsilon
                && MathF.Abs(W - tuple.W) < float.Epsilon;
        }

        public bool Equals(Tuple tuple, int precision)
        {
            return Math.Round(X, precision) == Math.Round(tuple.X, precision) &&
                   Math.Round(Y, precision) == Math.Round(tuple.Y, precision) &&
                   Math.Round(Z, precision) == Math.Round(tuple.Z, precision) &&
                   Math.Round(W, precision) == Math.Round(tuple.W, precision);
        }

        public static Tuple operator !(Tuple t)
            => new Tuple(-t.X, -t.Y, -t.Z, -t.W);

        public static Tuple operator *(Tuple t, float val)
            => new Tuple(t.X * val, t.Y * val, t.Z * val, t.W * val);

        public static Tuple operator *(Tuple left, Tuple right)
            => new Tuple(left.X * right.X, left.Y * right.Y, left.Z * right.Z, VectorIndicator);

        public static Tuple operator /(Tuple t, float val)
            => new Tuple(t.X / val, t.Y / val, t.Z / val, t.W / val);
    }
}
