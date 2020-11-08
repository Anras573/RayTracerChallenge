﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace RayTracerChallenge.Core
{
    public class Point : Tuple, IEquatable<Point>
    {
        public Point(float x, float y, float z) : base(x, y, z, PointIndicator)
        {
        }

        public bool Equals([AllowNull] Point other)
        {
            if (other == null) return false;

            return X == other.X && Y == other.Y && Z == other.Z;
        }

        public static Point operator +(Vector left, Point right)
          => new Point(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

        public static Point operator +(Point left, Vector right)
            => new Point(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

        public static Point operator -(Point left, Vector right)
            => new Point(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

        public static Vector operator -(Point left, Point right)
            => new Vector(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
    }
}
