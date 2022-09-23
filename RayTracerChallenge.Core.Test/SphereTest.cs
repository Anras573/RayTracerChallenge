using RayTracerChallenge.Core.Test.Comparers;
using System;
using Xunit;

namespace RayTracerChallenge.Core.Test
{
    [Trait("Category", "Sphere")]
    public class SphereTest
    {

        [Fact]
        [Trait("Category", "Normal At")]
        public void GivenPointOnTheXAxis_WhenGettingTheNormalAt_ThenReturnThePointAsVector()
        {
            // Arrange
            var sut = new Sphere();
            var point = new Point(1, 0, 0);

            // Act
            var normal = sut.NormalAt(point);

            // Assert
            Assert.Equal(new Vector(1, 0, 0), normal);
        }

        [Fact]
        [Trait("Category", "Normal At")]
        public void GivenPointOnTheYAxis_WhenGettingTheNormalAt_ThenReturnThePointAsVector()
        {
            // Arrange
            var sut = new Sphere();
            var point = new Point(0, 1, 0);

            // Act
            var normal = sut.NormalAt(point);

            // Assert
            Assert.Equal(new Vector(0, 1, 0), normal);
        }

        [Fact]
        [Trait("Category", "Normal At")]
        public void GivenPointOnTheZAxis_WhenGettingTheNormalAt_ThenReturnThePointAsVector()
        {
            // Arrange
            var sut = new Sphere();
            var point = new Point(0, 0, 1);

            // Act
            var normal = sut.NormalAt(point);

            // Assert
            Assert.Equal(new Vector(0, 0, 1), normal);
        }

        [Fact]
        [Trait("Category", "Normal At")]
        public void GivenPointOnANonaxialPoint_WhenGettingTheNormalAt_ThenReturnThePointAsVector()
        {
            // Arrange
            var nonaxial = MathF.Sqrt(3) / 3;
            var sut = new Sphere();
            var point = new Point(nonaxial, nonaxial, nonaxial);

            // Act
            var normal = sut.NormalAt(point);

            // Assert
            var expectedVector = new Vector(nonaxial, nonaxial, nonaxial);

            Assert.Equal(expectedVector.X, normal.X, ApproximateComparer.Default);
            Assert.Equal(expectedVector.Y, normal.Y, ApproximateComparer.Default);
            Assert.Equal(expectedVector.Z, normal.Z, ApproximateComparer.Default);
        }

        [Fact]
        [Trait("Category", "Normal At")]
        [Trait("Category", "Normalize")]
        public void GivenPoint_WhenGettingTheNormalAt_ThenReturnTheNormalAsANormalizedVector()
        {
            // Arrange
            var nonaxial = MathF.Sqrt(3) / 3;
            var sut = new Sphere();
            var point = new Point(nonaxial, nonaxial, nonaxial);

            // Act
            var normal = sut.NormalAt(point);
            var normalizedNormal = normal.Normalize();

            // Assert
            Assert.Equal(normalizedNormal.X, normal.X, ApproximateComparer.Default);
            Assert.Equal(normalizedNormal.Y, normal.Y, ApproximateComparer.Default);
            Assert.Equal(normalizedNormal.Z, normal.Z, ApproximateComparer.Default);
        }

        [Fact]
        [Trait("Category", "Normal At")]
        [Trait("Category", "Translate")]
        public void GivenATranslatedSphere_WhenGettingTheNormalAt_ThenReturnTheNormalAsAVectorInWorldSpace()
        {
            // Arrange
            var sut = new Sphere();
            var point = new Point(0f, 1.70711f, -0.70711f);
            
            sut.Transform = Matrix.Translate(0f, 1f, 0f);

            // Act
            var normal = sut.NormalAt(point);

            // Assert
            Assert.Equal(0f, normal.X, ApproximateComparer.Default);
            Assert.Equal(0.70711f, normal.Y, ApproximateComparer.Default);
            Assert.Equal(-0.70711f, normal.Z, ApproximateComparer.Default);
        }

        [Fact]
        [Trait("Category", "Normal At")]
        [Trait("Category", "Translate")]
        [Trait("Category", "Scale")]
        [Trait("Category", "Rotate")]
        public void GivenATransformedSphere_WhenGettingTheNormalAt_ThenReturnTheNormalAsAVectorInWorldSpace()
        {
            // Arrange
            var sqrt = MathF.Sqrt(2f);
            var sut = new Sphere();
            var point = new Point(0f, sqrt / 2f, -sqrt / 2f);
            var transform = Matrix.Scale(1f, 0.5f, 1f) * Matrix.RotateZ(MathF.PI / 5f);

            sut.Transform = transform;

            // Act
            var normal = sut.NormalAt(point);

            // Assert
            Assert.Equal(0f, normal.X, ApproximateComparer.Default);
            Assert.Equal(0.97014f, normal.Y, ApproximateComparer.Default);
            Assert.Equal(-0.24254f, normal.Z, ApproximateComparer.Default);
        }

        [Fact]
        [Trait("Category", "Material")]
        public void GivenASphereWithDefaultMaterial_WhenGettingMaterial_ThenReturnMaterial()
        {
            // Arrange
            var sut = new Sphere();

            // Act

            // Assert
            Assert.Equal(Material.Default.Color, sut.Material.Color);
            Assert.Equal(Material.Default.Ambient, sut.Material.Ambient);
            Assert.Equal(Material.Default.Diffuse, sut.Material.Diffuse);
            Assert.Equal(Material.Default.Specular, sut.Material.Specular);
            Assert.Equal(Material.Default.Shininess, sut.Material.Shininess);
        }

        [Fact]
        [Trait("Category", "Material")]
        public void GivenASphereWithMaterial_WhenGettingMaterial_ThenReturnMaterial()
        {
            // Arrange
            var sut = new Sphere();
            var expectedMaterial = new Material(Color.White, 1f, 1f, 1f, 1f);

            // Act
            sut.Material = expectedMaterial;

            // Assert
            Assert.Equal(expectedMaterial.Color, sut.Material.Color);
            Assert.Equal(expectedMaterial.Ambient, sut.Material.Ambient);
            Assert.Equal(expectedMaterial.Diffuse, sut.Material.Diffuse);
            Assert.Equal(expectedMaterial.Specular, sut.Material.Specular);
            Assert.Equal(expectedMaterial.Shininess, sut.Material.Shininess);
        }
    }
}
