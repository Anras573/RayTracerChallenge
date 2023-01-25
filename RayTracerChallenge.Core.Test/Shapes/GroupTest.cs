using RayTracerChallenge.Core.Shapes;
using Xunit;

namespace RayTracerChallenge.Core.Test.Shapes;

[Trait("Category", nameof(Group))]
public class GroupTest
{
    [Fact]
    public void CreatingANewGroup()
    {
        var g = new Group();
        
        Assert.Equal(Matrix.IdentityMatrix(), g.Transform);
        Assert.Empty(g);
    }
    
    [Fact]
    public void AddingAChildToAGroup()
    {
        var group = new Group();
        var child = new TestShape();
        
        group.Add(child);
        
        Assert.NotEmpty(group);
        Assert.Contains(child, group);
        Assert.Equal(group, child.Parent);
    }
    
    [Fact]
    [Trait("Category", nameof(Ray))]
    [Trait("Category", nameof(Intersections))]
    public void IntersectingARayWithAnEmptyGroup()
    {
        var group = new Group();
        var ray = new Ray(new Point(0.0f, 0.0f, 0.0f), new Vector(0.0f, 0.0f, 1.0f));

        var intersections = group.LocalIntersects(ray);
        
        Assert.Empty(intersections);
    }
    
    [Fact]
    [Trait("Category", nameof(Ray))]
    [Trait("Category", nameof(Intersections))]
    [Trait("Category", nameof(Sphere))]
    public void IntersectingARayWithANonemptyGroup()
    {
        var group = new Group();
        var s1 = new Sphere();
        var s2 = new Sphere
        {
            Transform = Matrix.Translate(0.0f, 0.0f, -3.0f)
        };
        var s3 = new Sphere
        {
            Transform = Matrix.Translate(5.0f, 0.0f, 0.0f)
        };
        
        group.Add(s1);
        group.Add(s2);
        group.Add(s3);
        
        var ray = new Ray(new Point(0.0f, 0.0f, -5.0f), new Vector(0.0f, 0.0f, 1.0f));

        var intersections = group.LocalIntersects(ray);
        
        Assert.NotEmpty(intersections);
        Assert.Equal(4, intersections.Length);
        Assert.Equal(s2, intersections[0].Object);
        Assert.Equal(s2, intersections[1].Object);
        Assert.Equal(s1, intersections[2].Object);
        Assert.Equal(s1, intersections[3].Object);
    }
    
    [Fact]
    [Trait("Category", nameof(Ray))]
    [Trait("Category", nameof(Intersections))]
    [Trait("Category", nameof(Sphere))]
    public void IntersectingATransformedGroup()
    {
        var group = new Group
        {
            Transform = Matrix.Scale(2.0f, 2.0f, 2.0f)
        };
        var s = new Sphere
        {
            Transform = Matrix.Translate(5.0f, 0.0f, 0.0f)
        };

        group.Add(s);

        var ray = new Ray(new Point(10.0f, 0.0f, -10.0f), new Vector(0.0f, 0.0f, 1.0f));

        var intersections = group.Intersects(ray);
        
        Assert.NotEmpty(intersections);
        Assert.Equal(2, intersections.Length);
    }
}