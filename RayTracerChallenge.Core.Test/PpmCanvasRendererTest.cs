using System;
using System.IO;
using Xunit;

namespace RayTracerChallenge.Core.Test
{
    [Trait("Category", "Canvas")]
    [Trait("Category", "PPM Image")]
    public class PpmCanvasRendererTest
    {
        [Fact]
        [Trait("Category", "Color")]
        public void GivenCanvas_WhenConvertingToPpmImageString_ThenHeaderIsCorrectlyCreated()
        {
            // Arrange
            var width = 10;
            var height = 20;
            var canvas = new Canvas(width, height);
            var ppmIdentifier = "P3";
            var newLine = Environment.NewLine;
            var sut = new PpmCanvasRenderer();
            var tempPath = Path.GetTempFileName();

            // Act
            sut.Render(canvas, tempPath);
            var ppmString = File.ReadAllText(tempPath);

            // Assert
            /*
             *  P3
             *  10 20
             *  255
             */
            var expectedHeader = $"{ppmIdentifier}{newLine}{width} {height}{newLine}{Color.MaximumColorValue}";
            Assert.StartsWith(expectedHeader, ppmString);
        }

        [Fact]
        [Trait("Category", "Color")]
        public void GivenCanvasWithColors_WhenConvertingToPpmImageString_ThenPpmImageStringIsCorrectlyCreated()
        {
            // Arrange
            var width = 5;
            var height = 3;
            var canvas = new Canvas(width, height);
            var color1 = new Color(1.5f, 0f, 0f);
            var color2 = new Color(0f, .5f, 0f);
            var color3 = new Color(-.5f, 0f, 1f);
            var sut = new PpmCanvasRenderer();
            var tempPath = Path.GetTempFileName();

            // Act
            canvas.WritePixel(0, 0, color1);
            canvas.WritePixel(2, 1, color2);
            canvas.WritePixel(4, 2, color3);
            sut.Render(canvas, tempPath);
            var ppmString = File.ReadAllText(tempPath);

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
        public void GivenCanvas_WhenConvertingToPpmImageString_ThenStringEndsWithANewLineCharacter()
        {
            // Arrange
            var width = 10;
            var height = 20;
            var canvas = new Canvas(width, height);
            var sut = new PpmCanvasRenderer();
            var tempPath = Path.GetTempFileName();

            // Act
            sut.Render(canvas, tempPath);
            var ppmString = File.ReadAllText(tempPath);

            // Assert
            Assert.EndsWith(Environment.NewLine, ppmString);
        }

        [Fact]
        [Trait("Category", "Color")]
        public void GivenLargeCanvasWithColors_WhenConvertingToPpmImageString_ThenPpmImageStringIsCorrectlyCreated()
        {
            // Arrange
            var width = 10;
            var height = 2;
            var canvas = new Canvas(width, height);
            var color = new Color(1f, .8f, .6f);
            var sut = new PpmCanvasRenderer();
            var tempPath = Path.GetTempFileName();

            // Act
            for (var x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    canvas.WritePixel(x, y, color);
                }
            }

            sut.Render(canvas, tempPath);
            var ppmString = File.ReadAllText(tempPath);

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
    }
}
