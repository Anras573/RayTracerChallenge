using RayTracerChallenge.Core.Patterns;
using Xunit;

namespace RayTracerChallenge.Core.Test.Patterns;

[Trait("Category", nameof(Checker))]
public class CheckerTest
{
    [Fact]
    public void CheckerShouldRepeatInX()
    {
        var pattern = new Checker(Color.White, Color.Black);

        var color1 = pattern.ColorAt(new Point(0f, 0f, 0f));
        var color2 = pattern.ColorAt(new Point(0.99f, 0f, 0f));
        var color3 = pattern.ColorAt(new Point(1.01f, 0f, 0f));
        
        Assert.Equal(Color.White, color1);
        Assert.Equal(Color.White, color2);
        Assert.Equal(Color.Black, color3);
    }
}