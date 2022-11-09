using System;
using System.Diagnostics.CodeAnalysis;

namespace RayTracerChallenge.Core;

public class Color : Tuple, IEquatable<Color>
{
    public static Color Black => new(0f, 0f, 0f);
    public static Color White => new(1f, 1f, 1f);
    public static Color Red => new (1f, 0f, 0f);
    public static Color Green => new (0f, 1f, 0f);
    public static Color Blue => new (0f, 0f, 1f);

    public float R => X;
    public float G => Y;
    public float B => Z;

    public const int MaximumColorValue = 255;

    public Color(float r, float g, float b) : base(r, g, b, VectorIndicator) { }

    public static Color operator +(Color left, Color right)
        => new (left.X + right.X, left.Y + right.Y, left.Z + right.Z);

    public static Color operator -(Color left, Color right)
        => new (left.X - right.X, left.Y - right.Y, left.Z - right.Z);

    public static Color operator *(Color left, Color right)
        => new (left.X * right.X, left.Y * right.Y, left.Z * right.Z);

    public static Color operator *(Color c, float val)
        => new (c.X * val, c.Y * val, c.Z * val);
        
    public static Color operator /(Color c, float val)
        => new (c.X / val, c.Y / val, c.Z / val);

    public Color HadamardProduct(Color other) => new (R * other.R, G * other.G, B * other.B);

    public Color SchurProduct(Color other) => HadamardProduct(other);

    public bool Equals([AllowNull] Color other)
    {
        if (other == null) return false;

        return MathF.Abs(R - other.R) < Utilities.Epsilon
               && MathF.Abs(G - other.G) < Utilities.Epsilon
               && MathF.Abs(B - other.B) < Utilities.Epsilon;
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as Color);
    }

    public override int GetHashCode()
    {
        return R.GetHashCode() ^ G.GetHashCode() ^ B.GetHashCode();
    }
}