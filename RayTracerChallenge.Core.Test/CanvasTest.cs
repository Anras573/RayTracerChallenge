using System;
using Xunit;

namespace RayTracerChallenge.Core.Test
{
    [Trait("Category", "Canvas")]
    public class CanvasTest
    {
        [Fact]
        [Trait("Category", "Color")]
        public void GivenCanvasWithWithAndHeight_WhenCallingConstructor_ThenCanvasWithWidthAndHeightAndDefaultColorIsCreated()
        {
            // Arrange
            var width = 10;
            var height = 20;
            var defaultColor = new Color(0f, 0f, 0f);

            // Act
            var canvas = new Canvas(width, height);

            // Assert
            Assert.Equal(width, canvas.Width);
            Assert.Equal(height, canvas.Height);

            foreach (var pixel in canvas.Pixels)
            {
                Assert.Equal(defaultColor, pixel);
            }
        }

        [Fact]
        [Trait("Category", "Color")]
        public void GivenCanvasWithRedColor_WhenCallingConstructor_ThenCanvasWithRedColorIsCreated()
        {
            // Arrange
            var width = 10;
            var height = 20;
            var x = 2;
            var y = 3;
            var redColor = new Color(1f, 0f, 0f);
            var canvas = new Canvas(width, height);

            // Act
            canvas.WritePixel(x, y, redColor);

            // Assert
            var index = 32;
            Assert.Equal(redColor, canvas.Pixels[index]);
        }

        [Fact]
        [Trait("Category", "Exception")]
        public void GivenCanvas_WhenWritingPixelWithXOutOfBound_ThenArgumentOutOfBoundExceptionIsThrown()
        {
            // Arrange
            var width = 10;
            var height = 10;
            var canvas = new Canvas(width, height);

            // Act
            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                canvas.WritePixel(width + 1, height - 1, new Color(1f, 1f, 1f));
            });

            // Assert
            Assert.Equal("x", ex.ParamName);
            var expectedException = new ArgumentOutOfRangeException("x", $"x can't be greater than {width - 1}");
            Assert.Equal(expectedException.Message, ex.Message);
        }

        [Fact]
        [Trait("Category", "Exception")]
        public void GivenCanvas_WhenWritingPixelWithYOutOfBound_ThenArgumentOutOfBoundExceptionIsThrown()
        {
            // Arrange
            var width = 10;
            var height = 10;
            var canvas = new Canvas(width, height);

            // Act
            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                canvas.WritePixel(width - 1, height + 1, new Color(1f, 1f, 1f));
            });

            // Assert
            Assert.Equal("y", ex.ParamName);
            var expectedException = new ArgumentOutOfRangeException("y", $"y can't be greater than {height - 1}");
            Assert.Equal(expectedException.Message, ex.Message);
        }
    }
}
