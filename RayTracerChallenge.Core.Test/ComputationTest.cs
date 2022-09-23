using Xunit;

namespace RayTracerChallenge.Core.Test
{
    [Trait("Category", nameof(Computation))]
    public class ComputationTest
    {
        [Fact]
        [Trait("Category", nameof(Ray))]
        [Trait("Category", nameof(Sphere))]
        [Trait("Category", nameof(Intersection))]
        public void GivenRayAndSphere_WhenCreatingComputation_ThenComputationIsCreated()
        {
            // Arrange
            var ray = new Ray(new Point(0f, 0f, -5f), new Vector(0f, 0f, 1f));
            var sphere = new Sphere();
            var intersection = new Intersection(4f, sphere);

            // Act
            var computation = new Computation(intersection, ray);

            // Assert
            Assert.Equal(intersection.TimeValue, computation.TimeValue);
            Assert.Equal(intersection.Object, computation.Object);
            Assert.Equal(new Point(0f, 0f, -1f), computation.Point);
            Assert.Equal(new Vector(0f, 0f, -1f), computation.EyeVector);
            Assert.Equal(new Vector(0f, 0f, -1f), computation.NormalVector);
        }

        [Fact]
        [Trait("Category", nameof(Ray))]
        [Trait("Category", nameof(Sphere))]
        [Trait("Category", nameof(Intersection))]
        public void GivenRayFromInsideSphere_WhenCreatingComputation_ThenComputationIsCreated()
        {
            // Arrange
            var ray = new Ray(new Point(0f, 0f, 0f), new Vector(0f, 0f, 1f));
            var sphere = new Sphere();
            var intersection = new Intersection(1f, sphere);

            // Act
            var computation = new Computation(intersection, ray);

            // Assert
            Assert.Equal(new Point(0f, 0f, 1f), computation.Point);
            Assert.Equal(new Vector(0f, 0f, -1f), computation.EyeVector);
            Assert.Equal(new Vector(0f, 0f, -1f), computation.NormalVector);
            Assert.True(computation.Inside);
        }

        [Fact]
        [Trait("Category", nameof(Ray))]
        [Trait("Category", nameof(Sphere))]
        [Trait("Category", nameof(Intersection))]
        public void GivenRayFromOutsideSphere_WhenCreatingComputation_ThenComputationIsCreated()
        {
            // Arrange
            var ray = new Ray(new Point(0f, 0f, -5f), new Vector(0f, 0f, 1f));
            var sphere = new Sphere();
            var intersection = new Intersection(4f, sphere);

            // Act
            var computation = new Computation(intersection, ray);

            // Assert
            Assert.False(computation.Inside);
        }
    }
}
