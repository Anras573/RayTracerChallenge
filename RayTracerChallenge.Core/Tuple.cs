using System;

namespace RayTracerChallenge.Core
{
    public struct Tuple
    {
        public float X;
        public float Y;
        public float Z;
        public float W;

        public static float VectorIndicator = 0f;
        public static float PointIndicator = 1f;
        public static int MaximumColorValue = 255;

        public Tuple(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public static Tuple Point(float x, float y, float z)
        {
            return new Tuple(x, y, z, PointIndicator);
        }

        public static Tuple Vector(float x, float y, float z)
        {
            return new Tuple(x, y, z, VectorIndicator);
        }

        public static Tuple Color(float x, float y, float z)
        {
            return new Tuple(x, y, z, VectorIndicator);
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

        public static Tuple operator +(Tuple left, Tuple right)
        {
            if (left.W == PointIndicator && right.W == PointIndicator)
            {
                throw new ArithmeticException("You can't add two Points together!");
            }

            return new Tuple(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);
        }

        public static Tuple operator -(Tuple left, Tuple right)
        {
            if (left.W == VectorIndicator && right.W == PointIndicator)
            {
                throw new ArithmeticException("You can't substract a Vector from a Point!");
            }

            return new Tuple(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);
        }

        public static Tuple operator !(Tuple t)
        {
            return new Tuple(-t.X, -t.Y, -t.Z, -t.W);
        }

        public static Tuple operator *(Tuple t, float val)
        {
            return new Tuple(t.X * val, t.Y * val, t.Z * val, t.W * val);
        }

        public static Tuple operator *(Tuple left, Tuple right)
        {
            return HadamardProduct(left, right);
        }

        public static Tuple operator /(Tuple t, float val)
        {
            return new Tuple(t.X / val, t.Y / val, t.Z / val, t.W / val);
        }

        public float Magnitude()
        {
            if (W != VectorIndicator)
            {
                throw new ArithmeticException("This operation can only be used on a Vector!");
            }

            return MathF.Sqrt(X * X + Y * Y + Z * Z);
        }

        public Tuple Normalize()
        {
            if (W != VectorIndicator)
            {
                throw new ArithmeticException("This operation can only be used on a Vector!");
            }

            var magnitude = 1f / Magnitude();

            return new Tuple(X * magnitude, Y * magnitude, Z * magnitude, W * magnitude);
        }

        public static float Dot (Tuple left, Tuple right)
        {
            if (left.W != VectorIndicator || right.W != VectorIndicator)
            {
                throw new ArithmeticException("This operation can only be used on Vectors!");
            }

            return left.X * right.X +
                   left.Y * right.Y +
                   left.Z * right.Z +
                   left.W * right.W;
        }

        public static Tuple Cross(Tuple left, Tuple right)
        {
            if (left.W != VectorIndicator || right.W != VectorIndicator)
            {
                throw new ArithmeticException("This operation can only be used on Vectors!");
            }

            return Vector(left.Y * right.Z - left.Z * right.Y,
                          left.Z * right.X - left.X * right.Z,
                          left.X * right.Y - left.Y * right.X);
        }

        public static Tuple HadamardProduct(Tuple colorLeft, Tuple colorRight)
        {
            return Color(colorLeft.X * colorRight.X,
                         colorLeft.Y * colorRight.Y,
                         colorLeft.Z * colorRight.Z);
        }

        public static Tuple SchurProduct(Tuple colorLeft, Tuple colorRight)
        {
            return HadamardProduct(colorLeft, colorRight);
        }
    }
}
