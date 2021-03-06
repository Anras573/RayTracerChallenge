﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace RayTracerChallenge.Core
{
    public class Color : Tuple, IEquatable<Color>
    {
        public float R => X;
        public float G => Y;
        public float B => Z;

        public static int MaximumColorValue = 255;

        public Color(float r, float g, float b) : base(r, g, b, VectorIndicator)
        {
        }

        public static Color operator +(Color left, Color right)
            => new Color(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

        public static Color operator -(Color left, Color right)
            => new Color(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

        public Color HadamardProduct(Color other) => new Color(R * other.R, G * other.G, B * other.B);

        public Color SchurProduct(Color other) => HadamardProduct(other);

        public bool Equals([AllowNull] Color other)
        {
            if (other == null) return false;

            return R == other.R && G == other.G && B == other.B;
        }
    }
}
