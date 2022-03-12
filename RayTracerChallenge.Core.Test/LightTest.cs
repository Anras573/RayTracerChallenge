using Xunit;

namespace RayTracerChallenge.Core.Test
{
    [Trait("Category", "Light")]
    public class LightTest
    {
        [Fact]
        public void GivenPositionAndIntensity_WhenCreatingLight_ThenReturnLightWithPositionAndIntensity()
        {
            // Arrange
            var position = new Point(0f, 0f, 0f);
            var intensity = new Color(1f, 1f, 1f);

            // Act
            var light = new Light(position, intensity);

            // Assert
            Assert.Equal(intensity, light.Intensity);
            Assert.Equal(position, light.Position);
        }
    }
}
