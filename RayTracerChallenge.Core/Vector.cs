using System;

namespace RayTracerChallenge.Core;

public class Vector : Tuple, IEquatable<Vector>
{
    public Vector(float x, float y, float z) : base(x, y, z, VectorIndicator)
    {
    }

    public float Dot(Vector other)
        => X * other.X + Y * other.Y + Z * other.Z;

    public Vector Cross(Vector other)
        => new(x: Y * other.Z - Z * other.Y,
            y: Z * other.X - X * other.Z,
            z: X * other.Y - Y * other.X);

    public Vector Normalize()
    {
        var magnitude = Magnitude();

        return new Vector(X / magnitude, Y / magnitude, Z / magnitude);
    }

    public float Magnitude()
        => MathF.Sqrt(MathF.Pow(X, 2) + MathF.Pow(Y, 2) + MathF.Pow(Z, 2));

    public static Vector operator +(Vector left, Vector right)
        => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

    public static Vector operator -(Vector left, Vector right)
        => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

    public static Vector operator *(Vector vector, float scalar)
        => new(vector.X * scalar, vector.Y * scalar, vector.Z * scalar);

    public static Vector operator -(Vector vector)
        => new(-vector.X, -vector.Y, -vector.Z);

    public Vector Reflect(Vector surface)
    {
        return this - surface * 2 * Dot(surface);
    }

    public bool Equals(Vector other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;

        return MathF.Abs(X - other.X) < Utilities.Epsilon
               && MathF.Abs(Y - other.Y) < Utilities.Epsilon
               && MathF.Abs(Z - other.Z) < Utilities.Epsilon;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((Vector)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z, W);
    }
}