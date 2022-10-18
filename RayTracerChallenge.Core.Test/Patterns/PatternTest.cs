using RayTracerChallenge.Core.Patterns;
using RayTracerChallenge.Core.Shapes;
using Xunit;

namespace RayTracerChallenge.Core.Test.Patterns;

[Trait("Category", nameof(Pattern))]
public class PatternTest
{
    [Fact]
    public void TheDefaultPatternTransformation()
    {
        var pattern = new TestPattern();
        
        Assert.Equal(Matrix.IdentityMatrix(), pattern.Transform);
    }
    
    [Fact]
    public void  AssigningATransformation()
    {
        var pattern = new TestPattern()
        {
            Transform = Matrix.Translate(1f, 2f, 3f)
        };
        
        Assert.Equal(Matrix.Translate(1f, 2f, 3f), pattern.Transform);
    }
    
    [Fact]
    [Trait("Category", nameof(Shape))]
    public void StripesWithAnObjectTransformation()
    {
        var obj = new Sphere
        {
            Transform = Matrix.Scale(2f, 2f, 2f)
        };
        var pattern = new TestPattern();

        var color = pattern.ColorAtShape(obj, new Point(2f, 3f, 4f));

        Assert.Equal(new Color(1f, 1.5f, 2f), color);
    }
    
    [Fact]
    [Trait("Category", nameof(Shape))]
    public void StripesWithAPatternTransformation()
    {
        var obj = new Sphere();
        var pattern = new TestPattern()
        {
            Transform = Matrix.Scale(2f, 2f, 2f)
        };

        var color = pattern.ColorAtShape(obj, new Point(2f, 3f, 4f));

        Assert.Equal(new Color(1f, 1.5f, 2f), color);
    }
    
    [Fact]
    [Trait("Category", nameof(Shape))]
    public void StripesWithBothAnObjectAndAPatternTransformation()
    {
        var obj = new Sphere
        {
            Transform = Matrix.Scale(2f, 2f, 2f)
        };
        var pattern = new TestPattern()
        {
            Transform = Matrix.Translate(0.5f, 1f, 1.5f)
        };

        var color = pattern.ColorAtShape(obj, new Point(2.5f, 3f, 3.5f));

        Assert.Equal(new Color(0.75f, 0.5f, 0.25f), color);
    }
}

public class TestPattern : Pattern
{
    public override Color ColorAt(Point point) => new (point.X, point.Y, point.Z);

    public TestPattern() : base(Color.White, Color.Black)
    {
    }
}