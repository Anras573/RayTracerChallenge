using RayTracerChallenge.Core.Test.Comparers;
using System;
using Xunit;

namespace RayTracerChallenge.Core.Test
{
    [Trait("Category", "Material")]
    public class MaterialTest
    {
        [Fact]
        public void GivenDefaultMaterial_WhenCreatingMaterial_ThenReturnDefaultMaterial()
        {
            // Arrange
            var expectedMaterial = new Material(Color.White, 0.1f, 0.9f, 0.9f, 200.0f);

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
        [Trait("Category", "Lights")]
        public void GivenEyesBetweenTheLightningAndTheSurface_WhenCalculatingColor_ThenReturnColor()
        {
            // Arrange
            var material = Material.Default;
            var position = new Point(0f, 0f, 0f);
            var eyeV = new Vector(0f, 0f, -1f);
            var normalV = new Vector(0f, 0f, -1f);
            var light = new Light(new Point(0f, 0f, -10f), Color.White);

            // Act
            var result = material.Lightning(light, position, eyeV, normalV);

            // Assert
            Assert.Equal(result, new Color(1.9f, 1.9f, 1.9f));
        }

        [Fact]
        [Trait("Category", "Lights")]
        public void GivenEyesBetweenTheLightningAndTheSurfaceWithEyesOffsetBy45Degrees_WhenCalculatingColor_ThenReturnColor()
        {
            // Arrange
            var material = Material.Default;
            var position = new Point(0f, 0f, 0f);
            var eyeV = new Vector(0f, MathF.Sqrt(2f) / 2, -MathF.Sqrt(2f) / 2);
            var normalV = new Vector(0f, 0f, -1f);
            var light = new Light(new Point(0f, 0f, -10f), Color.White);

            // Act
            var result = material.Lightning(light, position, eyeV, normalV);

            // Assert
            Assert.Equal(result, Color.White);
        }

        [Fact]
        [Trait("Category", "Lights")]
        public void GivenEyesBetweenTheLightningAndTheSurfaceWithLightOffsetBy45Degrees_WhenCalculatingColor_ThenReturnColor()
        {
            // Arrange
            var material = Material.Default;
            var position = new Point(0f, 0f, 0f);
            var eyeV = new Vector(0f, 0f, -1f);
            var normalV = new Vector(0f, 0f, -1f);
            var light = new Light(new Point(0f, 10f, -10f), Color.White);

            // Act
            var result = material.Lightning(light, position, eyeV, normalV);

            // Assert
            Assert.Equal(0.7364f, result.R, ApproximateComparer.Default);
            Assert.Equal(0.7364f, result.G, ApproximateComparer.Default);
            Assert.Equal(0.7364f, result.B, ApproximateComparer.Default);
        }

        [Fact]
        [Trait("Category", "Lights")]
        public void GivenEyesInThePathOfTheReflection_WhenCalculatingColor_ThenReturnColor()
        {
            // Arrange
            var material = Material.Default;
            var position = new Point(0f, 0f, 0f);
            var eyeV = new Vector(0f, -MathF.Sqrt(2f) / 2, -MathF.Sqrt(2f) / 2);
            var normalV = new Vector(0f, 0f, -1f);
            var light = new Light(new Point(0f, 10f, -10f), Color.White);

            // Act
            var result = material.Lightning(light, position, eyeV, normalV);

            // Assert
            Assert.Equal(1.6364f, result.R, ApproximateComparer.Default);
            Assert.Equal(1.6364f, result.G, ApproximateComparer.Default);
            Assert.Equal(1.6364f, result.B, ApproximateComparer.Default);
        }

        [Fact]
        [Trait("Category", "Lights")]
        public void GivenEyesBehindTheSurface_WhenCalculatingColor_ThenReturnColor()
        {
            // Arrange
            var material = Material.Default;
            var position = new Point(0f, 0f, 0f);
            var eyeV = new Vector(0f, 0f, -1f);
            var normalV = new Vector(0f, 0f, -1f);
            var light = new Light(new Point(0f, 0f, 10f), Color.White);

            // Act
            var result = material.Lightning(light, position, eyeV, normalV);

            // Assert
            Assert.Equal(result, new Color(0.1f, 0.1f, 0.1f));
        }
    }
}
