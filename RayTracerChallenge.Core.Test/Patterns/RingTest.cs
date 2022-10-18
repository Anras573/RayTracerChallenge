using RayTracerChallenge.Core.Patterns;
using Xunit;

namespace RayTracerChallenge.Core.Test.Patterns;

[Trait("Category", nameof(Ring))]
public class RingTest
{
    [Fact]
    public void ARingShouldExtendInBothXAndZ()
    {
        var pattern = new Ring(Color.White, Color.Black);

        var color1 = pattern.ColorAt(new Point(0f, 0f, 0f));
        var color2 = pattern.ColorAt(new Point(1f, 0f, 0f));
        var color3 = pattern.ColorAt(new Point(0f, 0f, 1f));
        var color4 = pattern.ColorAt(new Point(0.708f, 0f, 0.708f));
        
        Assert.Equal(Color.White, color1);
        Assert.Equal(Color.Black, color2);
        Assert.Equal(Color.Black, color3);
        Assert.Equal(Color.Black, color4);
    } 
}