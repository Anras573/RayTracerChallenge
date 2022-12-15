using RayTracerChallenge.Core.Shapes;
using Xunit;

namespace RayTracerChallenge.Core.Test.Shapes;

[Trait("Category", nameof(Cylinder))]
public class CylinderTest
{
    [Trait("Category", nameof(Ray))]
    [Theory]
    [InlineData(1.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f)]
    [InlineData(0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f)]
    [InlineData(0.0f, 0.0f, -5.0f, 1.0f, 1.0f, 1.0f)]
    public void ARayMissesACylinder(float p1, float p2, float p3, float v1, float v2, float v3)
    {
        var cylinder = new Cylinder();
        var direction = new Vector(v1, v2, v3).Normalize();
        var ray = new Ray(new Point(p1, p2, p3), direction);

        var intersections = cylinder.LocalIntersects(ray);
        
        Assert.Equal(0, intersections.Length);
    }
    
    [Trait("Category", nameof(Ray))]
    [Theory]
    [InlineData(1.0f, 0.0f, -5.0f, 0.0f, 0.0f, 1.0f, 5.0f, 5.0f)]
    [InlineData(0.0f, 0.0f, -5.0f, 0.0f, 0.0f, 1.0f, 4.0f, 6.0f)]
    [InlineData(0.5f, 0.0f, -5.0f, 0.1f, 1.0f, 1.0f, 6.80798f, 7.08872f)]
    public void ARayStrikesACylinder(float p1, float p2, float p3, float v1, float v2, float v3, float t0, float t1)
    {
        var cylinder = new Cylinder();
        var direction = new Vector(v1, v2, v3).Normalize();
        var ray = new Ray(new Point(p1, p2, p3), direction);

        var intersections = cylinder.LocalIntersects(ray);
        
        Assert.Equal(2, intersections.Length);
        Assert.Equal(t0, intersections[0].TimeValue, Utilities.Epsilon);
        Assert.Equal(t1, intersections[1].TimeValue, Utilities.Epsilon);
    }
    
    [Trait("Category", nameof(Ray))]
    [Theory]
    [InlineData(1.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f)]
    [InlineData(0.0f, -5.0f, -1.0f, 0.0f, 0.0f, -1.0f)]
    [InlineData(0.0f, -2.0f, 1.0f, 0.0f, 0.0f, 1.0f)]
    [InlineData(-1.0f, 1.0f, 0.0f, -1.0f, 0.0f, 0.0f)]
    public void NormalVectorOnACylinder(float p1, float p2, float p3, float v1, float v2, float v3)
    {
        var cylinder = new Cylinder();
        
        var normal = cylinder.LocalNormalAt(new Point(p1, p2, p3));

        var expectedNormal = new Vector(v1, v2, v3);
        Assert.Equal(expectedNormal, normal);
    }

    [Fact]
    public void TheDefaultMinimumAndMaximumForACylinder()
    {
        var cylinder = new Cylinder();
        
        Assert.Equal(float.NegativeInfinity, cylinder.Minimum);
        Assert.Equal(float.PositiveInfinity, cylinder.Maximum);
    }
    
    [Trait("Category", nameof(Ray))]
    [Theory]
    [InlineData(0.0f, 1.5f, 0.0f, 0.1f, 1.0f, 0.0f, 0)]
    [InlineData(0.0f, 3.0f, -5.0f, 0.0f, 0.0f, 1.0f, 0)]
    [InlineData(0.0f, 0.0f, -5.0f, 0.0f, 0.0f, 1.0f, 0)]
    [InlineData(0.0f, 2.0f, -5.0f, 0.0f, 0.0f, 1.0f, 0)]
    [InlineData(0.0f, 1.0f, -5.0f, 0.0f, 0.0f, 1.0f, 0)]
    [InlineData(0.0f, 1.5f, -2.0f, 0.0f, 0.0f, 1.0f, 2)]
    public void IntersectingAConstrainedCylinder(float p1, float p2, float p3, float v1, float v2, float v3, int count)
    {
        var cylinder = new Cylinder
        {
            Minimum = 1.0f,
            Maximum = 2.0f
        };

        var direction = new Vector(v1, v2, v3).Normalize();
        var ray = new Ray(new Point(p1, p2 ,p3), direction);

        var intersections = cylinder.LocalIntersects(ray);
        
        Assert.Equal(count, intersections.Length);
    }
    
    [Fact]
    public void TheDefaultClosedValueForACylinder()
    {
        var cylinder = new Cylinder();
        
        Assert.False(cylinder.Closed);
    }
    
    [Theory]
    [InlineData(0.0f, 3.0f, 0.0f, 0.0f, -1.0f, 0.0f, 2)]
    [InlineData(0.0f, 3.0f, -2.0f, 0.0f, -1.0f, 2.0f, 2)]
    [InlineData(0.0f, 4.0f, -2.0f, 0.0f, -1.0f, 1.0f, 2)]
    [InlineData(0.0f, 0.0f, -2.0f, 0.0f, 1.0f, 2.0f, 2)]
    [InlineData(0.0f, -1.0f, -2.0f, 0.0f, 1.0f, 1.0f, 2)]
    public void IntersectingTheCapsOfAClosedCylinder(float p1, float p2, float p3, float v1, float v2, float v3, int count)
    {
        var cylinder = new Cylinder
        {
            Closed = true,
            Minimum = 1.0f,
            Maximum = 2.0f
        };

        var direction = new Vector(v1, v2, v3).Normalize();
        var ray = new Ray(new Point(p1, p2, p3), direction);

        var intersections = cylinder.LocalIntersects(ray);
        
        Assert.Equal(count, intersections.Length);
    }

    [Theory]
    [InlineData(0.0f, 1.0f, 0.0f, 0.0f, -1.0f, 0.0f)]
    [InlineData(0.5f, 1.0f, 0.0f, 0.0f, -1.0f, 0.0f)]
    [InlineData(0.0f, 1.0f, 0.5f, 0.0f, -1.0f, 0.0f)]
    [InlineData(0.0f, 2.0f, 0.0f, 0.0f, 1.0f, 0.0f)]
    [InlineData(0.5f, 2.0f, 0.0f, 0.0f, 1.0f, 0.0f)]
    [InlineData(0.0f, 2.0f, 0.5f, 0.0f, 1.0f, 0.0f)]
    public void TheNormalVectorOnACylindersEndCaps(float p1, float p2, float p3, float v1, float v2, float v3)
    {
        var cylinder = new Cylinder
        {
            Closed = true,
            Minimum = 1.0f,
            Maximum = 2.0f
        };

        var normal = cylinder.LocalNormalAt(new Point(p1, p2, p3));

        var expectedNormal = new Vector(v1, v2, v3);
        Assert.Equal(expectedNormal, normal);
    }
}