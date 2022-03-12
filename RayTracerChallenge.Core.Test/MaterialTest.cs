using RayTracerChallenge.Core.Test.Comparers;
using System;
using Xunit;

namespace RayTracerChallenge.Core.Test
{
    [Trait("Category", "Material")]
    public class MaterialTest
    {
        private static ApproximateComparer DefaultComparer => new ApproximateComparer(.0001f);

        [Fact]
        public void GivenDefaultMaterial_WhenCreatingMaterial_ThenReturnDefaultMaterial()
        {
            // Arrange
            var expectedMaterial = new Material(new Color(1f, 1f, 1f), 0.1f, 0.9f, 0.9f, 200.0f);

            // Act
            var sut = Material.Default;

            // Assert
            Assert.Equal(expectedMaterial.Color, sut.Color);
            Assert.Equal(expectedMaterial.Ambient, sut.Ambient);
            Assert.Equal(expectedMaterial.Diffuse, sut.Diffuse);
            Assert.Equal(expectedMaterial.Specular, sut.Specular);
            Assert.Equal(expectedMaterial.Shininess, sut.Shininess);
        }

        [Fact]
        [Trait("Category", "Light")]
        public void GivenEyesBetweenTheLightningAndTheSurface_WhenCalculatingColor_ThenReturnColor()
        {
            // Arrange
            var material = Material.Default;
            var position = new Point(0f, 0f, 0f);
            var eyeV = new Vector(0f, 0f, -1f);
            var normalV = new Vector(0f, 0f, -1f);
            var light = new Light(new Point(0f, 0f, -10f), new Color(1f, 1f, 1f));

            // Act
            var result = material.Lightning(light, position, eyeV, normalV);

            // Assert
            Assert.Equal(result, new Color(1.9f, 1.9f, 1.9f));
        }

        [Fact]
        [Trait("Category", "Light")]
        public void GivenEyesBetweenTheLightningAndTheSurfaceWithEyesOffsetBy45Degrees_WhenCalculatingColor_ThenReturnColor()
        {
            // Arrange
            var material = Material.Default;
            var position = new Point(0f, 0f, 0f);
            var eyeV = new Vector(0f, MathF.Sqrt(2f) / 2, -MathF.Sqrt(2f) / 2);
            var normalV = new Vector(0f, 0f, -1f);
            var light = new Light(new Point(0f, 0f, -10f), new Color(1f, 1f, 1f));

            // Act
            var result = material.Lightning(light, position, eyeV, normalV);

            // Assert
            Assert.Equal(result, new Color(1f, 1f, 1f));
        }

        [Fact]
        [Trait("Category", "Light")]
        public void GivenEyesBetweenTheLightningAndTheSurfaceWithLightOffsetBy45Degrees_WhenCalculatingColor_ThenReturnColor()
        {
            // Arrange
            var material = Material.Default;
            var position = new Point(0f, 0f, 0f);
            var eyeV = new Vector(0f, 0f, -1f);
            var normalV = new Vector(0f, 0f, -1f);
            var light = new Light(new Point(0f, 10f, -10f), new Color(1f, 1f, 1f));

            // Act
            var result = material.Lightning(light, position, eyeV, normalV);

            // Assert
            Assert.Equal(0.7364f, result.R, DefaultComparer);
            Assert.Equal(0.7364f, result.G, DefaultComparer);
            Assert.Equal(0.7364f, result.B, DefaultComparer);
        }

        [Fact]
        [Trait("Category", "Light")]
        public void GivenEyesInThePathOfTheReflection_WhenCalculatingColor_ThenReturnColor()
        {
            // Arrange
            var material = Material.Default;
            var position = new Point(0f, 0f, 0f);
            var eyeV = new Vector(0f, -MathF.Sqrt(2f) / 2, -MathF.Sqrt(2f) / 2);
            var normalV = new Vector(0f, 0f, -1f);
            var light = new Light(new Point(0f, 10f, -10f), new Color(1f, 1f, 1f));

            // Act
            var result = material.Lightning(light, position, eyeV, normalV);

            // Assert
            Assert.Equal(1.6364f, result.R, DefaultComparer);
            Assert.Equal(1.6364f, result.G, DefaultComparer);
            Assert.Equal(1.6364f, result.B, DefaultComparer);
        }

        [Fact]
        [Trait("Category", "Light")]
        public void GivenEyesBehindTheSurface_WhenCalculatingColor_ThenReturnColor()
        {
            // Arrange
            var material = Material.Default;
            var position = new Point(0f, 0f, 0f);
            var eyeV = new Vector(0f, 0f, -1f);
            var normalV = new Vector(0f, 0f, -1f);
            var light = new Light(new Point(0f, 0f, 10f), new Color(1f, 1f, 1f));

            // Act
            var result = material.Lightning(light, position, eyeV, normalV);

            // Assert
            Assert.Equal(result, new Color(0.1f, 0.1f, 0.1f));
        }
    }
}
