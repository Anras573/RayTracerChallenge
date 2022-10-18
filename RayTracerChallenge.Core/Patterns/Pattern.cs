using RayTracerChallenge.Core.Shapes;

namespace RayTracerChallenge.Core.Patterns;

public abstract class Pattern
{
    public Matrix Transform = Matrix.IdentityMatrix();

    public Color First;
    public Color Second;

    public Pattern(Color first, Color second)
    {
        First = first;
        Second = second;
    }
    
    public abstract Color ColorAt(Point point);
    
    public Color ColorAtShape(Shape shape, Point point)
    {
        var objectPoint = shape.Transform.Inverse() * point;
        var patternPoint = Transform.Inverse() * objectPoint;
        
        return ColorAt(patternPoint);
    }
}