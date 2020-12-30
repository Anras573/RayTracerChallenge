using Xunit;

namespace RayTracerChallenge.Core.Test
{
    [Trait("Category", "Ray")]
    public class RayTest
    {

        [Fact]
        public void GivenRayWithOriginAndDirection_WhenQueryingOriginAndDirection_ThenReturnOriginAndDirection()
        {
            // Arrange
            var origin = new Point(1f, 2f, 3f);
            var direction = new Vector(4f, 5f, 6f);

            // Act
            var ray = new Ray(origin, direction);

            // Assert
            Assert.Equal(origin, ray.Origin);
            Assert.Equal(direction, ray.Direction);
        }

        [Theory]
        [InlineData(0f, 2f, 3f, 4f)]
        [InlineData(1f, 3f, 3f, 4f)]
        [InlineData(-1f, 1f, 3f, 4f)]
        [InlineData(2.5f, 4.5f, 3f, 4f)]
        public void GivenRayWithOriginAndDirection_WhenComputingPointFromDistance_ThenReturnPoint(
            float time,
            float pointX,
            float pointY,
            float pointZ)
        {
            // Arrange
            var origin = new Point(2f, 3f, 4f);
            var direction = new Vector(1f, 0f, 0f);
            var ray = new Ray(origin, direction);

            // Act
            var point = ray.Position(time);

            // Assert
            var expectedPosition = new Point(pointX, pointY, pointZ);

            Assert.Equal(expectedPosition, point);
        }

        [Fact]
        [Trait("Category", "Sphere")]
        public void GivenRayAndSphere_WhenRayIntersectsSphere_ThenReturnArrayOfIntersectingTimeValues()
        {
            // Arrange
            var origin = new Point(0f, 0f, -5f);
            var direction = new Vector(0f, 0f, 1f);
            var ray = new Ray(origin, direction);
            var sphere = new Sphere();

            // Act
            var intersections = sphere.Intersects(ray);

            // Assert
            Assert.Equal(2, intersections.Length);
            Assert.Equal(4f, intersections[0].TimeValue);
            Assert.Equal(6f, intersections[1].TimeValue);
        }

        [Fact]
        [Trait("Category", "Sphere")]
        public void GivenRayAndSphere_WhenRayIntersectsSphereAtTangent_ThenReturnArrayOfIntersectingTimeValues()
        {
            // Arrange
            var origin = new Point(0f, 1f, -5f);
            var direction = new Vector(0f, 0f, 1f);
            var ray = new Ray(origin, direction);
            var sphere = new Sphere();

            // Act
            var intersections = sphere.Intersects(ray);

            // Assert
            Assert.Equal(2, intersections.Length);
            Assert.Equal(5f, intersections[0].TimeValue);
            Assert.Equal(5f, intersections[1].TimeValue);
        }

        [Fact]
        [Trait("Category", "Sphere")]
        public void GivenRayAndSphere_WhenRayMissSphere_ThenReturnEmptyArray()
        {
            // Arrange
            var origin = new Point(0f, 2f, -5f);
            var direction = new Vector(0f, 0f, 1f);
            var ray = new Ray(origin, direction);
            var sphere = new Sphere();

            // Act
            var intersections = sphere.Intersects(ray);

            // Assert
            Assert.Equal(0, intersections.Length);
        }

        [Fact]
        [Trait("Category", "Sphere")]
        public void GivenTimeValueAndSphere_WhenCreatingIntersection_ThenReturnIntersectionWithTimeValueAndSphere()
        {
            // Arrange
            var timeValue = 3.5f;
            var sphere = new Sphere();

            // Act
            var intersection = new Intersection(timeValue, sphere);

            // Assert
            Assert.Equal(sphere, intersection.Object);
            Assert.Equal(timeValue, intersection.TimeValue);
        }

        [Fact]
        [Trait("Category", "Sphere")]
        public void GivenMultipleIntersections_WhenCreatingIntersections_ThenReturnArrayOfIntersections()
        {
            // Arrange
            var sphere = new Sphere();
            var intersection1 = new Intersection(1f, sphere);
            var intersection2 = new Intersection(2f, sphere);

            // Act
            var intersections = new Intersections(intersection1, intersection2);

            // Assert
            Assert.Equal(2, intersections.Length);
            Assert.Equal(1f, intersections[0].TimeValue);
            Assert.Equal(2f, intersections[1].TimeValue);
        }

        [Fact]
        [Trait("Category", "Sphere")]
        public void GivenRayAndSphere_WhenRayIntersectsSphere_ThenReturnArrayOfIntersectingSpheres()
        {
            // Arrange
            var origin = new Point(0f, 0f, -5f);
            var direction = new Vector(0f, 0f, 1f);
            var ray = new Ray(origin, direction);
            var sphere = new Sphere();

            // Act
            var intersections = sphere.Intersects(ray);

            // Assert
            Assert.Equal(2, intersections.Length);
            Assert.Equal(sphere, intersections[0].Object);
            Assert.Equal(sphere, intersections[1].Object);
        }

        [Fact]
        [Trait("Category", "Sphere")]
        public void GivenIntersectionHit_WhenAllIntersectionsHavePositiveTimeValue_ThenReturnLowestPositiveIntersection()
        {
            // Arrange
            var sphere = new Sphere();
            var i1 = new Intersection(1, sphere);
            var i2 = new Intersection(2, sphere);
            var intersections = new Intersections(i1, i2);

            // Act
            var hit = intersections.Hit();

            // Assert
            Assert.Equal(i1, hit);
        }

        [Fact]
        [Trait("Category", "Sphere")]
        public void GivenIntersectionHit_WhenSomeIntersectionsHaveNegativeTimeValue_ThenReturnLowestPositiveIntersection()
        {
            // Arrange
            var sphere = new Sphere();
            var i1 = new Intersection(-1, sphere);
            var i2 = new Intersection(1, sphere);
            var intersections = new Intersections(i1, i2);

            // Act
            var hit = intersections.Hit();

            // Assert
            Assert.Equal(i2, hit);
        }

        [Fact]
        [Trait("Category", "Sphere")]
        public void GivenIntersectionHit_WhenAllIntersectionsHaveNegativeTimeValue_ThenReturnLowestPositiveIntersection()
        {
            // Arrange
            var sphere = new Sphere();
            var i1 = new Intersection(-2, sphere);
            var i2 = new Intersection(-1, sphere);
            var intersections = new Intersections(i1, i2);

            // Act
            var hit = intersections.Hit();

            // Assert
            Assert.Null(hit);
        }

        [Fact]
        [Trait("Category", "Sphere")]
        public void GivenIntersectionHit_WhenIntersectionsHaveMixedTimeValue_ThenReturnLowestPositiveIntersection()
        {
            // Arrange
            var sphere = new Sphere();
            var i1 = new Intersection(5, sphere);
            var i2 = new Intersection(7, sphere);
            var i3 = new Intersection(-3, sphere);
            var i4 = new Intersection(2, sphere);
            var intersections = new Intersections(i1, i2, i3, i4);

            // Act
            var hit = intersections.Hit();

            // Assert
            Assert.Equal(i4, hit);
        }

        [Fact]
        [Trait("Category", "Sphere")]
        public void GivenRay_WhenTranslating_ThenReturnTranslatedRay()
        {
            // Arrange
            var ray = new Ray(origin: new Point(1, 2, 3), direction: new Vector(0, 1, 0));
            var translation = Matrix.Translate(3, 4, 5);

            // Act
            var transformedRay = ray.Transform(translation);

            // Assert
            var expectedOrigin = new Point(4, 6, 8);
            var expectedDirection = new Vector(0, 1, 0);
            Assert.Equal(expectedOrigin, transformedRay.Origin);
            Assert.Equal(expectedDirection, transformedRay.Direction);
        }

        [Fact]
        [Trait("Category", "Sphere")]
        public void GivenRay_WhenScaling_ThenReturnScaledRay()
        {
            // Arrange
            var ray = new Ray(origin: new Point(1, 2, 3), direction: new Vector(0, 1, 0));
            var scaling = Matrix.Scale(2, 3, 4);

            // Act
            var scaledRay = ray.Transform(scaling);

            // Assert
            var expectedOrigin = new Point(2, 6, 12);
            var expectedDirection = new Vector(0, 3, 0);
            Assert.Equal(expectedOrigin, scaledRay.Origin);
            Assert.Equal(expectedDirection, scaledRay.Direction);
        }

        [Fact]
        [Trait("Category", "Sphere")]
        public void GivenSphere_WhenGettingDefaultTransformation_ThenReturnIdentityMatrix()
        {
            // Arrange
            var sphere = new Sphere();

            // Act
            var transform = sphere.Transform;

            // Assert
            Assert.Equal(Matrix.IdentityMatrix(), transform);
        }

        [Fact]
        [Trait("Category", "Sphere")]
        public void GivenSphere_WhenSettingTransformation_ThenDefaultTransformationIsOverwritten()
        {
            // Arrange
            var sphere = new Sphere();
            var translation = Matrix.Translate(2, 3, 4);

            // Act
            sphere.Transform = translation;

            // Assert
            Assert.Equal(translation, sphere.Transform);
        }

        [Fact]
        [Trait("Category", "Sphere")]
        public void GivenScaledSphere_WhenIntersectingWithRay_ThenIntersectionIsReturned()
        {
            // Arrange
            var ray = new Ray(origin: new Point(0, 0, -5), direction: new Vector(0, 0, 1));
            var sphere = new Sphere
            {
                Transform = Matrix.Scale(2, 2, 2)
            };

            // Act
            var intersections = sphere.Intersects(ray);

            // Assert
            Assert.Equal(2, intersections.Length);
            Assert.Equal(3, intersections[0].TimeValue);
            Assert.Equal(7, intersections[1].TimeValue);
        }

        [Fact]
        [Trait("Category", "Sphere")]
        public void GivenTranslatedSphere_WhenIntersectingWithRay_ThenIntersectionIsReturned()
        {
            // Arrange
            var ray = new Ray(origin: new Point(0, 0, -5), direction: new Vector(0, 0, 1));
            var sphere = new Sphere
            {
                Transform = Matrix.Translate(5, 0, 0)
            };

            // Act
            var intersections = sphere.Intersects(ray);

            // Assert
            Assert.Equal(0, intersections.Length);
        }
    }
}
