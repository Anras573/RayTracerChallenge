using RayTracerChallenge.Core.Test.Comparers;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Xunit;

namespace RayTracerChallenge.Core.Test
{
    [Trait("Category", nameof(World))]
    public class WorldTest
    {

        [Fact]
        [Trait("Category", nameof(Light))]
        public void GivenWorld_WhenCallingConstructor_ThenEmptyWorldIsCreated()
        {
            // Arrange
            var world = new World();

            // Act

            // Assert
            Assert.Empty(world.Objects);
            Assert.Empty(world.Lights);
        }

        [Fact]
        [Trait("Category", nameof(Light))]
        [Trait("Category", nameof(Sphere))]
        public void GivenWorld_WhenCreatingDefault_ThenDefaultWorldIsCreated()
        {
            // Arrange
            var light = new Light(new Point(-10f, 10f, -10f), Color.White);
            
            var sphere1 = new Sphere
            {
                Material =
                {
                    Color = new Color(0.8f, 1.0f, 0.6f),
                    Diffuse = 0.7f,
                    Specular = 0.2f
                }
            };

            var sphere2 = new Sphere
            {
                Transform = Matrix.Scale(0.5f, 0.5f, 0.5f)
            };

            // Act
            var world = World.Default();

            // Assert
            Assert.NotEmpty(world.Objects);
            Assert.NotNull(world.Lights);
            Assert.Equal(light, world.Lights.First());

            Assert.Equal(sphere1.Material, world.Objects[0].Material);
            Assert.Equal(sphere1.Origin, world.Objects[0].Origin);
            Assert.Equal(sphere1.Transform, world.Objects[0].Transform);

            Assert.Equal(sphere2.Material, world.Objects[1].Material);
            Assert.Equal(sphere2.Origin, world.Objects[1].Origin);
            Assert.Equal(sphere2.Transform, world.Objects[1].Transform);
        }

        [Fact]
        [Trait("Category", nameof(Ray))]
        [Trait("Category", nameof(Intersection))]
        [Trait("Category", nameof(Intersections))]
        public void GivenDefaultWorld_WhenIntersectingWithRay_ThenReturnIntersections()
        {
            // Arrange
            var world = World.Default();
            var ray = new Ray(new Point(0f, 0f, -5f), new Vector(0f, 0f, 1f));

            // Act
            var intersections = world.Intersect(ray);

            // Assert
            Assert.Equal(4, intersections.Length);
            Assert.Equal(4f, intersections[0].TimeValue);
            Assert.Equal(4.5f, intersections[1].TimeValue);
            Assert.Equal(5.5f, intersections[2].TimeValue);
            Assert.Equal(6f, intersections[3].TimeValue);
        }

        [Fact]
        [Trait("Category", nameof(Ray))]
        [Trait("Category", nameof(Intersection))]
        [Trait("Category", nameof(Intersections))]
        public void GivenDefaultWorld_WhenPreparingComputations_ThenReturnComputations()
        {
            // Arrange
            var world = World.Default();
            var ray = new Ray(new Point(0f, 0f, -5f), new Vector(0f, 0f, 1f));

            // Act
            var intersections = world.Intersect(ray);

            // Assert
            Assert.Equal(4, intersections.Length);
            Assert.Equal(4f, intersections[0].TimeValue);
            Assert.Equal(4.5f, intersections[1].TimeValue);
            Assert.Equal(5.5f, intersections[2].TimeValue);
            Assert.Equal(6f, intersections[3].TimeValue);
        }

        [Fact]
        [Trait("Category", nameof(Ray))]
        [Trait("Category", nameof(Intersection))]
        [Trait("Category", nameof(Computation))]
        [Trait("Category", nameof(Color))]
        public void GivenDefaultWorld_WhenShadingIntersection_ThenReturnColor()
        {
            // Arrange
            var world = World.Default();
            var ray = new Ray(new Point(0f, 0f, -5f), new Vector(0f, 0f, 1f));
            var shape = world.Objects.First();
            var intersection = new Intersection(4f, shape);

            // Act
            var computation = new Computation(intersection, ray);
            var color = world.ShadeHit(computation);

            // Assert
            var expectedColor = new Color(0.38066f, 0.47583f, 0.2855f);
            Assert.Equal(expectedColor.R, color.R, ApproximateComparer.Default);
            Assert.Equal(expectedColor.G, color.G, ApproximateComparer.Default);
            Assert.Equal(expectedColor.B, color.B, ApproximateComparer.Default);
        }

        [Fact]
        [Trait("Category", nameof(Ray))]
        [Trait("Category", nameof(Intersection))]
        [Trait("Category", nameof(Computation))]
        [Trait("Category", nameof(Color))]
        public void GivenDefaultWorld_WhenShadingIntersectionFromInside_ThenReturnColor()
        {
            // Arrange
            var world = World.Default();
            world.Lights = new List<Light> { new(new Point(0f, 0.25f, 0f), Color.White) };
            
            var ray = new Ray(new Point(0f, 0f, 0f), new Vector(0f, 0f, 1f));
            var shape = world.Objects[1]; // Second object in world
            var intersection = new Intersection(0.5f, shape);

            // Act
            var computation = new Computation(intersection, ray);
            var color = world.ShadeHit(computation);

            // Assert
            var expectedColor = new Color(0.90498f, 0.90498f, 0.90498f);
            Assert.Equal(expectedColor.R, color.R, ApproximateComparer.Default);
            Assert.Equal(expectedColor.G, color.G, ApproximateComparer.Default);
            Assert.Equal(expectedColor.B, color.B, ApproximateComparer.Default);
        }

        [Fact]
        [Trait("Category", nameof(Ray))]
        [Trait("Category", nameof(Color))]
        public void GivenDefaultWorld_WhenRayMisses_ThenReturnColorBlack()
        {
            // Arrange
            var world = World.Default();
            var ray = new Ray(new Point(0f, 0f, -5f), new Vector(0f, 1f, 0f));

            // Act
            var color = world.ColorAt(ray);

            // Assert
            Assert.Equal(Color.Black, color);
        }

        [Fact]
        [Trait("Category", nameof(Ray))]
        [Trait("Category", nameof(Intersection))]
        [Trait("Category", nameof(Computation))]
        [Trait("Category", nameof(Color))]
        public void GivenDefaultWorld_WhenRayHit_ThenReturnColor()
        {
            // Arrange
            var world = World.Default();
            var ray = new Ray(new Point(0f, 0f, -5f), new Vector(0f, 0f, 1f));

            // Act
            var color = world.ColorAt(ray);

            // Assert
            var expectedColor = new Color(0.38066f, 0.47583f, 0.2855f);
            Assert.Equal(expectedColor.R, color.R, ApproximateComparer.Default);
            Assert.Equal(expectedColor.G, color.G, ApproximateComparer.Default);
            Assert.Equal(expectedColor.B, color.B, ApproximateComparer.Default);
        }

        [Fact]
        [Trait("Category", nameof(Ray))]
        [Trait("Category", nameof(Intersection))]
        [Trait("Category", nameof(Computation))]
        [Trait("Category", nameof(Color))]
        public void GivenDefaultWorld_WhenRayHitBetweenSpheres_ThenReturnHitSpheresColor()
        {
            // Arrange
            var world = World.Default();
            world.Objects[0].Material.Ambient = 1f;
            world.Objects[1].Material.Ambient = 1f;

            var ray = new Ray(new Point(0f, 0f, 0.75f), new Vector(0f, 0f, -1f));

            // Act
            var color = world.ColorAt(ray);

            // Assert
            Assert.Equal(world.Objects[1].Material.Color, color);
        }

        [Fact]
        [Trait("Category", "Lights")]
        [Trait("Category", "Shadow")]
        public void ThereIsNoShadowWhenNothingIsCollinearWithPointAndLight()
        {
            // Arrange
            var world = World.Default();
            var point = new Point(0f, 10f, 0f);

            // Act
            var result = world.IsShadowed(point);

            // Assert
            Assert.False(result);
        }

        [Fact]
        [Trait("Category", "Lights")]
        [Trait("Category", "Shadow")]
        public void TheShadowWhenAnObjectIsBetweenThePointAndTheLight()
        {
            // Arrange
            var world = World.Default();
            var point = new Point(10f, -10f, 10f);

            // Act
            var result = world.IsShadowed(point);

            // Assert
            Assert.True(result);
        }

        [Fact]
        [Trait("Category", "Lights")]
        [Trait("Category", "Shadow")]
        public void ThereIsNoShadowWhenAnObjectIsBehindTheLight()
        {
            // Arrange
            var world = World.Default();
            var point = new Point(-20f, 20f, -20f);

            // Act
            var result = world.IsShadowed(point);

            // Assert
            Assert.False(result);
        }

        [Fact]
        [Trait("Category", "Lights")]
        [Trait("Category", "Shadow")]
        public void ThereIsNoShadowWhenAnObjectIsBehindThePoint()
        {
            // Arrange
            var world = World.Default();
            var point = new Point(-2f, 2f, -2f);

            // Act
            var result = world.IsShadowed(point);

            // Assert
            Assert.False(result);
        }

        [Fact]
        [Trait("Category", "Lights")]
        [Trait("Category", "Shadow")]
        public void ShadeHitIsGivenAnIntersectionInShadow()
        {
            // Arrange
            var sphere = new Sphere
            {
                Transform = Matrix.Translate(0f, 0f, 10f)
            };

            var world = new World
            {
                Lights = new()
                {
                    new Light(new Point(0f, 0f, -10f), Color.White)
                },
                Objects = new()
                {
                    new Sphere(),
                    sphere
                }
            };
            var ray = new Ray(new Point(0f, 0f, 5f), new Vector(0f, 0f, 1f));
            var intersection = new Intersection(4, sphere);
            var computations = new Computation(intersection, ray);

            // Act
            var result = world.ShadeHit(computations);

            // Assert
            var expectedColor = new Color(0.1f, 0.1f, 0.1f);
            Assert.Equal(expectedColor, result);
        }
    }
}
