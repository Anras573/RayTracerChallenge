using RayTracerChallenge.Core.Shapes;
using Xunit;

namespace RayTracerChallenge.Core.Test;

[Trait("Category", nameof(Intersections))]
public class IntersectionsTest
{
    [Theory]
    [InlineData(0, 1.0f, 1.5f)]
    [InlineData(1, 1.5f, 2.0f)]
    [InlineData(2, 2.0f, 2.5f)]
    [InlineData(3, 2.5f, 2.5f)]
    [InlineData(4, 2.5f, 1.5f)]
    [InlineData(5, 1.5f, 1.0f)]
    [Trait("Category", nameof(Sphere))]
    [Trait("Category", nameof(Ray))]
    [Trait("Category", nameof(Intersection))]
    [Trait("Category", nameof(Material))]
    [Trait("Category", nameof(Computation))]
    public void FindingN1AndN2AtVariousIntersections(int index, float expectedN1, float expectedN2)
    {
        var a = new Sphere
        {
            Material = Material.Glass,
            Transform = Matrix.Scale(2.0f, 2.0f, 2.0f)
        };
        var b = new Sphere
        {
            Material = Material.Glass,
            Transform = Matrix.Translate(0.0f, 0.0f, -0.25f)
        };
        var c = new Sphere
        {
            Material = Material.Glass,
            Transform = Matrix.Translate(0.0f, 0.0f, 0.25f)
        };
        
        a.Material.RefractiveIndex = 1.5f;
        b.Material.RefractiveIndex = 2.0f;
        c.Material.RefractiveIndex = 2.5f;
        
        var ray = new Ray(new Point(0.0f, 0.0f, -4.0f), new Vector(0.0f, 0.0f, 1.0f));
        var intersections = new Intersections(
            new Intersection(2.0f, a),
            new Intersection(2.75f, b),
            new Intersection(3.25f, c),
            new Intersection(4.75f, b),
            new Intersection(5.25f, c),
            new Intersection(6.0f, a));

        var computations = new Computation(intersections[index], ray, intersections);
        
        Assert.Equal(expectedN1, computations.N1);
        Assert.Equal(expectedN2, computations.N2);
    }

    [Fact]
    [Trait("Category", nameof(Sphere))]
    [Trait("Category", nameof(Ray))]
    [Trait("Category", nameof(Intersection))]
    [Trait("Category", nameof(Material))]
    [Trait("Category", nameof(Computation))]
    public void TheUnderPointIsOffsetBelowTheSurface()
    {
        var ray = new Ray(new Point(0.0f, 0.0f, -5.0f), new Vector(0.0f, 0.0f, 1.0f));
        var shape = new Sphere
        {
            Material = Material.Glass,
            Transform = Matrix.Translate(0.0f, 0.0f, 1.0f)
        };
        var intersections = new Intersections(new Intersection(5.0f, shape));

        var computation = new Computation(intersections[0], ray, intersections);
        
        Assert.True(computation.UnderPoint.Z > Utilities.Epsilon / 2);
        Assert.True(computation.Point.Z < computation.UnderPoint.Z);
    }
}