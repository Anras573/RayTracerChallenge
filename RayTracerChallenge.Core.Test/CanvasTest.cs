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
            var defaultColor = Tupple.Color(0f, 0f, 0f);

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
            var redColor = Tupple.Color(1f, 0f, 0f);
            var canvas = new Canvas(width, height);

            // Act
            canvas.WritePixel(x, y, redColor);

            // Assert
            var index = 32;
            Assert.Equal(redColor, canvas.Pixels[index]);
        }

        [Fact]
        [Trait("Category", "Color")]
        [Trait("Category", "PPM Image")]
        public void GivenCanvas_WhenConvertingToPpmImageString_ThenHeaderIsCorrectlyCreated()
        {
            // Arrange
            var width = 10;
            var height = 20;
            var canvas = new Canvas(width, height);
            var ppmIdentifier = "P3";
            var newLine = Environment.NewLine;

            // Act
            var ppmString = canvas.ToPpm();

            // Assert
            /*
             *  P3
             *  10 20
             *  255
             */
            var expectedHeader = $"{ppmIdentifier}{newLine}{width} {height}{newLine}{Tupple.MaximumColorValue}";
            Assert.StartsWith(expectedHeader, ppmString);
        }

        [Fact]
        [Trait("Category", "Color")]
        [Trait("Category", "PPM Image")]
        public void GivenCanvasWithColors_WhenConvertingToPpmImageString_ThenPpmImageStringIsCorrectlyCreated()
        {
            // Arrange
            var width = 5;
            var height = 3;
            var canvas = new Canvas(width, height);
            var color1 = Tupple.Color(1.5f, 0f, 0f);
            var color2 = Tupple.Color(0f, .5f, 0f);
            var color3 = Tupple.Color(-.5f, 0f, 1f);

            // Act
            canvas.WritePixel(0, 0, color1);
            canvas.WritePixel(2, 1, color2);
            canvas.WritePixel(4, 2, color3);
            var ppmString = canvas.ToPpm();

            // Assert
            var line4 = "255 0 0 0 0 0 0 0 0 0 0 0 0 0 0 ";
            var line5 = "0 0 0 0 0 0 0 128 0 0 0 0 0 0 0 ";
            var line6 = "0 0 0 0 0 0 0 0 0 0 0 0 0 0 255 ";
            var lines = ppmString.Split(Environment.NewLine);
            Assert.Equal(line4, lines[3]);
            Assert.Equal(line5, lines[4]);
            Assert.Equal(line6, lines[5]);
        }

        [Fact]
        [Trait("Category", "PPM Image")]
        public void GivenCanvas_WhenConvertingToPpmImageString_ThenStringEndsWithANewLineCharacter()
        {
            // Arrange
            var width = 10;
            var height = 20;
            var canvas = new Canvas(width, height);

            // Act
            var ppmString = canvas.ToPpm();

            // Assert
            Assert.EndsWith(Environment.NewLine, ppmString);
        }

        [Fact]
        [Trait("Category", "Color")]
        [Trait("Category", "PPM Image")]
        public void GivenLargeCanvasWithColors_WhenConvertingToPpmImageString_ThenPpmImageStringIsCorrectlyCreated()
        {
            // Arrange
            var width = 10;
            var height = 2;
            var canvas = new Canvas(width, height);
            var color = Tupple.Color(1f, .8f, .6f);

            // Act
            for (var x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    canvas.WritePixel(x, y, color);
                }
            }

            var ppmString = canvas.ToPpm();

            // Assert
            var line4 = "255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204 ";
            var line5 = "153 255 204 153 255 204 153 255 204 153 255 204 153 ";
            var line6 = "255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204 ";
            var line7 = "153 255 204 153 255 204 153 255 204 153 255 204 153 ";
            var lines = ppmString.Split(Environment.NewLine);
            Assert.Equal(line4, lines[3]);
            Assert.Equal(line5, lines[4]);
            Assert.Equal(line6, lines[5]);
            Assert.Equal(line7, lines[6]);
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
                canvas.WritePixel(width + 1, height - 1, Tupple.Color(1f, 1f, 1f));
            });

            // Assert
            Assert.Equal($"x can't be greater than {width - 1}\r\nParameter name: x", ex.Message);
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
                canvas.WritePixel(width - 1, height + 1, Tupple.Color(1f, 1f, 1f));
            });

            // Assert
            Assert.Equal($"y can't be greater than {height - 1}\r\nParameter name: y", ex.Message);
        }
    }
}
