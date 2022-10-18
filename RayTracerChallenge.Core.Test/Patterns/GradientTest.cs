using RayTracerChallenge.Core.Patterns;
using Xunit;

namespace RayTracerChallenge.Core.Test.Patterns;

[Trait("Category", nameof(Gradient))]
public class GradientTest
{
    [Fact]
    public void AGradientLinearlyInterpolatesBetweenColors()
    {
        var pattern = new Gradient(Color.White, Color.Black);

        var color1 = pattern.ColorAt(new Point(0f, 0f, 0f));
        var color2 = pattern.ColorAt(new Point(0.25f, 0f, 0f));
        var color3 = pattern.ColorAt(new Point(0.5f, 0f, 0f));
        var color4 = pattern.ColorAt(new Point(0.75f, 0f, 0f));
        
        Assert.Equal(Color.White, color1);
        Assert.Equal(new Color(0.75f, 0.75f, 0.75f), color2);
        Assert.Equal(new Color(0.5f, 0.5f, 0.5f), color3);
        Assert.Equal(new Color(0.25f, 0.25f, 0.25f), color4);
    }
}