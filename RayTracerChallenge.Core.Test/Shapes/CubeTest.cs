using RayTracerChallenge.Core.Shapes;
using Xunit;

namespace RayTracerChallenge.Core.Test.Shapes;

[Trait("Category", nameof(Cube))]
public class CubeTest
{
    [Theory]
    [Trait("Category", nameof(Ray))]
    [InlineData(5.0f, 0.5f, 0.0f, -1.0f, 0.0f, 0.0f, 4.0f, 6.0f)]
    [InlineData(-5.0f, 0.5f, 0.0f, 1.0f, 0.0f, 0.0f, 4.0f, 6.0f)]
    [InlineData(0.5f, 5.0f, 0.0f, 0.0f, -1.0f, 0.0f, 4.0f, 6.0f)]
    [InlineData(0.5f, -5.0f, 0.0f, 0.0f, 1.0f, 0.0f, 4.0f, 6.0f)]
    [InlineData(0.5f, 0.0f, 5.0f, 0.0f, 0.0f, -1.0f, 4.0f, 6.0f)]
    [InlineData(0.5f, 0.0f, -5.0f, 0.0f, 0.0f, 1.0f, 4.0f, 6.0f)]
    [InlineData(0.0f, 0.5f, 0.0f, 0.0f, 0.0f, 1.0f, -1.0f, 1.0f)]
    public void ARayIntersectsACube(float p1, float p2, float p3, float v1, float v2, float v3, float t1, float t2)
    {
        var cube = new Cube();
        var ray = new Ray(new Point(p1, p2, p3), new Vector(v1, v2, v3));

        var xs = cube.LocalIntersects(ray);
        
        Assert.Equal(2, xs.Length);
        Assert.Equal(t1, xs[0].TimeValue);
        Assert.Equal(t2, xs[1].TimeValue);
    }
    
    [Theory]
    [Trait("Category", nameof(Ray))]
    [InlineData(-2.0f, 0.0f, 0.0f, 0.2673f, 0.5345f, 0.8018f)]
    [InlineData(0.0f, -2.0f, 0.0f, 0.8018f, 0.2673f, 0.5345f)]
    [InlineData(0.0f, 0.0f, -2.0f, 0.5345f, 0.8018f, 0.2673f)]
    [InlineData(2.0f, 0.0f, 2.0f, 0.0f, 0.0f, -1.0f)]
    [InlineData(0.0f, 2.0f, 2.0f, 0.0f, -1.0f, 0.0f)]
    [InlineData(2.0f, 2.0f, 0.0f, -1.0f, 0.0f, 0.0f)]
    public void ARayMissesACube(float p1, float p2, float p3, float v1, float v2, float v3)
    {
        var cube = new Cube();
        var ray = new Ray(new Point(p1, p2, p3), new Vector(v1, v2, v3));

        var xs = cube.LocalIntersects(ray);
        
        Assert.Equal(0, xs.Length);
    }

    [Theory]
    [InlineData(1.0f, 0.5f, -0.8f, 1.0f, 0.0f, 0.0f)]
    [InlineData(-1.0f, -0.2f, 0.9f, -1.0f, 0.0f, 0.0f)]
    [InlineData(-0.4f, 1.0f, -0.1f, 0.0f, 1.0f, 0.0f)]
    [InlineData(0.3f, -1.0f, -0.7f, 0.0f, -1.0f, 0.0f)]
    [InlineData(-0.6f, 0.3f, 1.0f, 0.0f, 0.0f, 1.0f)]
    [InlineData(0.4f, 0.4f, -1.0f, 0.0f, 0.0f, -1.0f)]
    [InlineData(1.0f, 1.0f, 1.0f, 1.0f, 0.0f, 0.0f)]
    [InlineData(-1.0f, -1.0f, -1.0f, -1.0f, 0.0f, 0.0f)]
    public void TheNormalOnTheSurfaceOfACube(float p1, float p2, float p3, float n1, float n2, float n3)
    {
        var cube = new Cube();
        var point = new Point(p1, p2, p3);

        var normal = cube.LocalNormalAt(point);

        var expectedNormal = new Vector(n1, n2, n3);
        Assert.Equal(expectedNormal, normal);
    }
}