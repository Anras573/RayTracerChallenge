using System;
using RayTracerChallenge.Core.Shapes;
using Xunit;

namespace RayTracerChallenge.Core.Test.Shapes;

[Trait("Category", nameof(Cone))]
public class ConeTest
{
    [Theory]
    [InlineData(0.0f, 0.0f, -5.0f, 0.0f, 0.0f, 1.0f, 5.0f, 5.0f)]
    [InlineData(0.0f, 0.0f, -5.0f, 1.0f, 1.0f, 1.0f, 8.66025f, 8.66025f)]
    [InlineData(1.0f, 1.0f, -5.0f, -0.5f, -1.0f, 1.0f, 4.55006f, 49.44994f)]
    public void IntersectingAConeWithARay(float p1, float p2, float p3, float v1, float v2, float v3, float t0, float t1)
    {
        var cone = new Cone();
        var direction = new Vector(v1, v2, v3).Normalize();
        var ray = new Ray(new Point(p1, p2, p3), direction);

        var intersections = cone.LocalIntersects(ray);
        
        Assert.Equal(2, intersections.Length);
        Assert.Equal(t0, intersections[0].TimeValue, Utilities.Epsilon);
        Assert.Equal(t1, intersections[1].TimeValue, Utilities.Epsilon);
    }

    [Fact]
    public void IntersectingAConeWithARayParallelToOneOfItsHalves()
    {
        var cone = new Cone();
        var direction = new Vector(0.0f, 1.0f, 1.0f).Normalize();
        var ray = new Ray(new Point(0.0f, 0.0f, -1.0f), direction);

        var intersections = cone.LocalIntersects(ray);
        
        Assert.Equal(1, intersections.Length);
        Assert.Equal(0.35355f, intersections[0].TimeValue, Utilities.Epsilon);
    }

    [Theory]
    [InlineData(0.0f, 0.0f, -5.0f, 0.0f, 1.0f, 0.0f, 0)]
    [InlineData(0.0f, 0.0f, -0.25f, 0.0f, 1.0f, 1.0f, 2)]
    [InlineData(0.0f, 0.0f, -0.25f, 0.0f, 1.0f, 0.0f, 4)]
    public void IntersectingAConesEndCaps(float p1, float p2, float p3, float v1, float v2, float v3, int count)
    {
        var cone = new Cone
        {
            Closed = true,
            Minimum = -0.5f, 
            Maximum = 0.5f
        };
        var direction = new Vector(v1, v2, v3).Normalize();
        var ray = new Ray(new Point(p1, p2, p3), direction);

        var intersections = cone.LocalIntersects(ray);
        
        Assert.Equal(count, intersections.Length);
    }

    [Theory]
    [InlineData(0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f)]
    [InlineData(1.0f, 1.0f, 1.0f, 1.0f, -1.41421f, 1.0f)]
    [InlineData(-1.0f, -1.0f, 0.0f, -1.0f, 1.0f, 0.0f)]
    public void ComputingTheNormalVectorOnACone(float p1, float p2, float p3, float v1, float v2, float v3)
    {
        var cone = new Cone();

        var normal = cone.LocalNormalAt(new Point(p1, p2, p3));

        var expectedNormal = new Vector(v1, v2, v3);
        Assert.Equal(expectedNormal, normal);
    }
}