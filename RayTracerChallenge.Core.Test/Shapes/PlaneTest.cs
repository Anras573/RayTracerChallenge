using RayTracerChallenge.Core.Shapes;
using Xunit;

namespace RayTracerChallenge.Core.Test.Shapes;

[Trait("Category", nameof(Plane))]
public class PlaneTest
{
    [Theory]
    [InlineData(0f, 0f, 0f)]
    [InlineData(10f, 0f, -10f)]
    [InlineData(-5f, 0f, 150f)]
    public void TheNormalOfAPlaneIsConstantEverywhere(float f1, float f2, float f3)
    {
        var plane = new Plane();

        var normalAt = plane.LocalNormalAt(new Point(f1, f2, f3));

        var expectedNormalAt = new Vector(0f, 1f, 0f);
        Assert.Equal(expectedNormalAt, normalAt);
    }

    [Fact]
    [Trait("Category", nameof(Ray))]
    public void IntersectWithARayParallelToThePlane()
    {
        var plane = new Plane();
        var ray = new Ray(new Point(0f, 10f, 0f), new Vector(0f, 0f, 1f));

        var intersections = plane.LocalIntersects(ray);

        Assert.Equal(0, intersections.Length);
    }

    [Fact]
    [Trait("Category", nameof(Ray))]
    public void IntersectWithACoplanarRay()
    {
        var plane = new Plane();
        var ray = new Ray(new Point(0f, 0f, 0f), new Vector(0f, 0f, 1f));

        var intersections = plane.LocalIntersects(ray);

        Assert.Equal(0, intersections.Length);
    }

    [Fact]
    [Trait("Category", nameof(Ray))]
    public void ARayIntersectingAPlaneFromAbove()
    {
        var plane = new Plane();
        var ray = new Ray(new Point(0f, 1f, 0f), new Vector(0f, -1f, 0f));

        var intersections = plane.LocalIntersects(ray);

        Assert.Equal(1, intersections.Length);
        Assert.Equal(1, intersections[0].TimeValue);
        Assert.Equal(plane, intersections[0].Object);
    }

    [Fact]
    [Trait("Category", nameof(Ray))]
    public void ARayIntersectingAPlaneFromBelow()
    {
        var plane = new Plane();
        var ray = new Ray(new Point(0f, -1f, 0f), new Vector(0f, 1f, 0f));

        var intersections = plane.LocalIntersects(ray);

        Assert.Equal(1, intersections.Length);
        Assert.Equal(1, intersections[0].TimeValue);
        Assert.Equal(plane, intersections[0].Object);
    }
}