using System;

namespace RayTracerChallenge.Core
{
    public struct Tupple
    {
        public float X;
        public float Y;
        public float Z;
        public float W;

        public static float VectorIndicator = 0f;
        public static float PointIndicator = 1f;
        public static int MaximumColorValue = 255;

        public Tupple(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public static Tupple Point(float x, float y, float z)
        {
            return new Tupple(x, y, z, PointIndicator);
        }

        public static Tupple Vector(float x, float y, float z)
        {
            return new Tupple(x, y, z, VectorIndicator);
        }

        public static Tupple Color(float x, float y, float z)
        {
            return new Tupple(x, y, z, VectorIndicator);
        }

        public bool Equals(Tupple tupple)
        {
            return MathF.Abs(X - tupple.X) < float.Epsilon
                && MathF.Abs(Y - tupple.Y) < float.Epsilon
                && MathF.Abs(Z - tupple.Z) < float.Epsilon
                && MathF.Abs(W - tupple.W) < float.Epsilon;
        }

        public static Tupple operator +(Tupple left, Tupple right)
        {
            if (left.W == PointIndicator && right.W == PointIndicator)
            {
                throw new ArithmeticException("You can't add two Points together!");
            }

            return new Tupple(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);
        }

        public static Tupple operator -(Tupple left, Tupple right)
        {
            if (left.W == VectorIndicator && right.W == PointIndicator)
            {
                throw new ArithmeticException("You can't substract a Vector from a Point!");
            }

            return new Tupple(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);
        }

        public static Tupple operator !(Tupple t)
        {
            return new Tupple(-t.X, -t.Y, -t.Z, -t.W);
        }

        public static Tupple operator *(Tupple t, float val)
        {
            return new Tupple(t.X * val, t.Y * val, t.Z * val, t.W * val);
        }

        public static Tupple operator *(Tupple left, Tupple right)
        {
            return HadamardProduct(left, right);
        }

        public static Tupple operator /(Tupple t, float val)
        {
            return new Tupple(t.X / val, t.Y / val, t.Z / val, t.W / val);
        }

        public float Magnitude()
        {
            if (W != VectorIndicator)
            {
                throw new ArithmeticException("This operation can only be used on a Vector!");
            }

            return MathF.Sqrt(X * X + Y * Y + Z * Z);
        }

        public Tupple Normalize()
        {
            if (W != VectorIndicator)
            {
                throw new ArithmeticException("This operation can only be used on a Vector!");
            }

            var magnitude = 1f / Magnitude();

            return new Tupple(X * magnitude, Y * magnitude, Z * magnitude, W * magnitude);
        }

        public static float Dot (Tupple left, Tupple right)
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

        public static Tupple Cross(Tupple left, Tupple right)
        {
            if (left.W != VectorIndicator || right.W != VectorIndicator)
            {
                throw new ArithmeticException("This operation can only be used on Vectors!");
            }

            return Vector(left.Y * right.Z - left.Z * right.Y,
                          left.Z * right.X - left.X * right.Z,
                          left.X * right.Y - left.Y * right.X);
        }

        public static Tupple HadamardProduct(Tupple colorLeft, Tupple colorRight)
        {
            return Color(colorLeft.X * colorRight.X,
                         colorLeft.Y * colorRight.Y,
                         colorLeft.Z * colorRight.Z);
        }

        public static Tupple SchurProduct(Tupple colorLeft, Tupple colorRight)
        {
            return HadamardProduct(colorLeft, colorRight);
        }
    }
}
