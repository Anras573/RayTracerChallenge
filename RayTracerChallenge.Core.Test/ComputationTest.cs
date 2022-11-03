using System;
using RayTracerChallenge.Core.Shapes;
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

        [Fact]
        [Trait("Category", nameof(Ray))]
        [Trait("Category", nameof(Sphere))]
        [Trait("Category", nameof(Intersection))]
        public void TheHitShouldOffsetThePoint()
        {
            // Arrange
            var ray = new Ray(new Point(0f, 0f, -5f), new Vector(0f, 0f, 1f));
            var sphere = new Sphere
            {
                Transform = Matrix.Translate(0f, 0f, 1f)
            };
            var intersection = new Intersection(5f, sphere);

            // Act
            var computation = new Computation(intersection, ray);

            // Assert
            Assert.True(computation.OverPoint.Z < -float.Epsilon/2);
            Assert.True(computation.Point.Z > computation.OverPoint.Z);
        }
        
        [Fact]
        [Trait("Category", nameof(Ray))]
        [Trait("Category", nameof(Plane))]
        [Trait("Category", nameof(Intersection))]
        public void PrecomputingTheReflectionVector()
        {
            var shape = new Plane();
            var ray = new Ray(new Point(0.0f, 1.0f, -1.0f),
                new Vector(0.0f, -MathF.Sqrt(2.0f) / 2.0f, MathF.Sqrt(2.0f) / 2.0f));
            var intersection = new Intersection(MathF.Sqrt(2.0f), shape);

            var computations = new Computation(intersection, ray);

            Assert.Equal(new Vector(0.0f, MathF.Sqrt(2.0f) / 2.0f, MathF.Sqrt(2.0f) / 2.0f),
                computations.ReflectVector);
        }

        [Fact]
        [Trait("Category", nameof(Sphere))]
        [Trait("Category", nameof(Ray))]
        [Trait("Category", nameof(Intersections))]
        public void TheSchlickApproximationUnderTotalInternalReflection()
        {
            var shape = new Sphere
            {
                Material = Material.Glass
            };
            var ray = new Ray(
                new Point(0.0f, 0.0f, MathF.Sqrt(2) / 2),
                new Vector(0.0f, 1.0f, 0.0f));
            var intersections = new Intersections(
                new Intersection(-MathF.Sqrt(2) / 2, shape),
                new Intersection(MathF.Sqrt(2) / 2, shape));

            var computations = new Computation(intersections[1], ray, intersections);
            var reflectance = computations.Schlick();
            
            Assert.Equal(1.0f, reflectance);
        }
        
        [Fact]
        [Trait("Category", nameof(Sphere))]
        [Trait("Category", nameof(Ray))]
        [Trait("Category", nameof(Intersections))]
        public void TheSchlickApproximationWithAPerpendicularViewingAngle()
        {
            var shape = new Sphere
            {
                Material = Material.Glass
            };
            shape.Material.RefractiveIndex = 1.5f;
            var ray = new Ray(
                new Point(0.0f, 0.0f, 0.0f), new Vector(0.0f, 1.0f, 0.0f));
            var intersections = new Intersections(
                new Intersection(-1.0f, shape),
                new Intersection(1.0f, shape));

            var computations = new Computation(intersections[1], ray, intersections);
            var reflectance = computations.Schlick();
            
            Assert.Equal(0.04f, reflectance, Utilities.Epsilon);
        }
        
        [Fact]
        [Trait("Category", nameof(Sphere))]
        [Trait("Category", nameof(Ray))]
        [Trait("Category", nameof(Intersections))]
        public void TheSchlickApproximationWithSmallAngleAndN2GreaterThanN1()
        {
            var shape = new Sphere
            {
                Material = Material.Glass
            };
            shape.Material.RefractiveIndex = 1.5f;
            var ray = new Ray(
                new Point(0.0f, 0.99f, -2.0f), new Vector(0.0f, 0.0f, 1.0f));
            var intersections = new Intersections(new Intersection(1.8589f, shape));

            var computations = new Computation(intersections[0], ray, intersections);
            var reflectance = computations.Schlick();
            
            Assert.Equal(0.48873f, reflectance, Utilities.Epsilon);
        }
    }
}
