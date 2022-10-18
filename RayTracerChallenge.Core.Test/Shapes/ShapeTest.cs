using System;
using RayTracerChallenge.Core.Shapes;
using RayTracerChallenge.Core.Test.Comparers;
using Xunit;

namespace RayTracerChallenge.Core.Test.Shapes
{
    [Trait("Category", nameof(Shape))]
    public class ShapeTest
    {
        [Fact]
        [Trait("Category", nameof(Shape.Transform))]
        public void TheDefaultTransformation()
        {
            var shape = new TestShape();

            var transform = shape.Transform;

            Assert.Equal(Matrix.IdentityMatrix(), transform);
        }

        [Fact]
        [Trait("Category", nameof(Shape.Transform))]
        [Trait("Category", nameof(Matrix.Translate))]
        public void AssigningATransformation()
        {
            var shape = new TestShape()
            {
                Transform = Matrix.Translate(2f, 3f, 4f)
            };

            var transform = shape.Transform;

            Assert.Equal(Matrix.Translate(2f, 3f, 4f), transform);
        }

        [Fact]
        [Trait("Category", nameof(Material))]
        public void TheDefaultMaterial()
        {
            // Arrange
            var sut = new TestShape();

            // Act

            // Assert
            Assert.Equal(Material.Default.Color, sut.Material.Color);
            Assert.Equal(Material.Default.Ambient, sut.Material.Ambient);
            Assert.Equal(Material.Default.Diffuse, sut.Material.Diffuse);
            Assert.Equal(Material.Default.Specular, sut.Material.Specular);
            Assert.Equal(Material.Default.Shininess, sut.Material.Shininess);
        }

        [Fact]
        [Trait("Category", nameof(Material))]
        public void AssigningAMaterial()
        {
            // Arrange
            var sut = new TestShape();
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

        [Fact]
        [Trait("Category", nameof(Ray))]
        [Trait("Category", nameof(Matrix.Scale))]
        public void IntersectingAScaledShapeWithARay()
        {
            var ray = new Ray(new Point(0f, 0f, -5f), new Vector(0f, 0f, 1f));
            var shape = new TestShape
            {
                Transform = Matrix.Scale(2f, 2f, 2f)
            };

            var _ = shape.Intersects(ray);

            Assert.Equal(new Point(0f, 0f, -2.5f), shape.SavedRay.Origin);
            Assert.Equal(new Vector(0f, 0f, 0.5f), shape.SavedRay.Direction);
        }

        [Fact]
        [Trait("Category", nameof(Ray))]
        [Trait("Category", nameof(Matrix.Translate))]
        public void IntersectingATranslatedShapeWithARay()
        {
            var ray = new Ray(new Point(0f, 0f, -5f), new Vector(0f, 0f, 1f));
            var shape = new TestShape
            {
                Transform = Matrix.Translate(5f, 0f, 0f)
            };

            var _ = shape.Intersects(ray);

            Assert.Equal(new Point(-5f, 0f, -5f), shape.SavedRay.Origin);
            Assert.Equal(new Vector(0f, 0f, 1f), shape.SavedRay.Direction);
        }

        [Fact]
        [Trait("Category", nameof(Ray))]
        [Trait("Category", nameof(Matrix.Translate))]
        public void ComputingTheNormalOnATranslatedShape()
        {
            var shape = new TestShape
            {
                Transform = Matrix.Translate(0f, 1f, 0f)
            };

            var normalAt = shape.NormalAt(new Point(0f, 1.70711f, -0.70711f));

            var expectedNormalAt = new Vector(0f, 0.70711f, -0.70711f);
            Assert.Equal(expectedNormalAt.X, normalAt.X, ApproximateComparer.Default);
            Assert.Equal(expectedNormalAt.Y, normalAt.Y, ApproximateComparer.Default);
            Assert.Equal(expectedNormalAt.Z, normalAt.Z, ApproximateComparer.Default);
        }

        [Fact]
        [Trait("Category", nameof(Ray))]
        [Trait("Category", nameof(Matrix.Scale))]
        [Trait("Category", nameof(Matrix.RotateZ))]
        public void ComputingTheNormalOnATransformedShape()
        {
            var shape = new TestShape
            {
                Transform = Matrix.Scale(1f, 0.5f, 1f) * Matrix.RotateZ(MathF.PI / 5)
            };

            var normalAt = shape.NormalAt(new Point(0, MathF.Sqrt(2) / 2, -MathF.Sqrt(2) / 2));

            var expectedNormalAt = new Vector(0f, 0.97014f, -0.24254f);
            Assert.Equal(expectedNormalAt.X, normalAt.X, ApproximateComparer.Default);
            Assert.Equal(expectedNormalAt.Y, normalAt.Y, ApproximateComparer.Default);
            Assert.Equal(expectedNormalAt.Z, normalAt.Z, ApproximateComparer.Default);
        }
    }

    public class TestShape : Shape
    {
        public Ray SavedRay { get; private set; }
        public override Intersections LocalIntersects(Ray localRay)
        {
            SavedRay = localRay;

            return new Intersections();
        }

        public override Vector LocalNormalAt(Point localPoint)
        {
            return new Vector(localPoint.X, localPoint.Y, localPoint.Z);
        }
    }
}
