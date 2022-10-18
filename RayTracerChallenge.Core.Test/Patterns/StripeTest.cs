using RayTracerChallenge.Core.Patterns;
using RayTracerChallenge.Core.Shapes;
using Xunit;

namespace RayTracerChallenge.Core.Test.Patterns;

[Trait("Category", nameof(Stripe))]
public class StripeTest
{
    [Fact]
    public void CreatingAStripePattern()
    {
        var pattern = new Stripe(Color.White, Color.Black);
        
        Assert.Equal(Color.White, pattern.First);
        Assert.Equal(Color.Black, pattern.Second);
    }
    
    [Theory]
    [InlineData(0f)]
    [InlineData(1f)]
    [InlineData(2f)]
    public void AStripePatternIsConstantInY(float y)
    {
        var pattern = new Stripe(Color.White, Color.Black);
        
        Assert.Equal(Color.White, pattern.ColorAt(new Point(0f, y, 0f)));
    }
    
    [Theory]
    [InlineData(0f)]
    [InlineData(1f)]
    [InlineData(2f)]
    public void AStripePatternIsConstantInZ(float z)
    {
        var pattern = new Stripe(Color.White, Color.Black);
        
        Assert.Equal(Color.White, pattern.ColorAt(new Point(0f, 0f, z)));
    }
    
    [Fact]
    public void AStripePatternAlternatesInX()
    {
        var pattern = new Stripe(Color.White, Color.Black);
        
        Assert.Equal(Color.White, pattern.ColorAt(new Point(0f, 0f, 0f)));
        Assert.Equal(Color.White, pattern.ColorAt(new Point(0.9f, 0f, 0f)));
        Assert.Equal(Color.Black, pattern.ColorAt(new Point(1f, 0f, 0f)));
        Assert.Equal(Color.Black, pattern.ColorAt(new Point(-0.1f, 0f, 0f)));
        Assert.Equal(Color.Black, pattern.ColorAt(new Point(-1f, 0f, 0f)));
        Assert.Equal(Color.White, pattern.ColorAt(new Point(-1.1f, 0f, 0f)));
    }
}