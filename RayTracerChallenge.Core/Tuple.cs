using System;

namespace RayTracerChallenge.Core;

public class Tuple : IEquatable<Tuple>
{
    public readonly float X;
    public readonly float Y;
    public readonly float Z;
    public readonly float W;

    public const float VectorIndicator = 0f;
    public const float PointIndicator = 1f;

    public Tuple(float x, float y, float z, float w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static Tuple operator !(Tuple t)
        => new(-t.X, -t.Y, -t.Z, -t.W);

    public static Tuple operator *(Tuple t, float val)
        => new(t.X * val, t.Y * val, t.Z * val, t.W * val);

    public static Tuple operator *(Tuple left, Tuple right)
        => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z, VectorIndicator);

    public static Tuple operator /(Tuple t, float val)
        => new(t.X / val, t.Y / val, t.Z / val, t.W / val);

    public bool Equals(Tuple other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z) && W.Equals(other.W);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((Tuple)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z, W);
    }
}