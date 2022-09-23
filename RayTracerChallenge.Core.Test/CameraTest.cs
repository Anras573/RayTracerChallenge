using RayTracerChallenge.Core.Test.Comparers;
using System;
using Xunit;

namespace RayTracerChallenge.Core.Test;

[Trait("Category", nameof(Camera))]
public class CameraTest
{
    [Fact]
    [Trait("Category", "Identity Matrix")]
    public void GivenConstructor_WhenCreatingCamera_ThenReturnValidCamera()
    {
        // Arrange
        var horizontalSize = 160;
        var verticalSize = 120;
        var fieldOfView = MathF.PI / 2f;

        // Act
        var camera = new Camera(horizontalSize, verticalSize, fieldOfView);

        // Assert
        Assert.Equal(horizontalSize, camera.HorizontalSize);
        Assert.Equal(verticalSize, camera.VerticalSize);
        Assert.Equal(fieldOfView, camera.FieldOfView);
        Assert.Equal(Matrix.IdentityMatrix(), camera.Transform);
    }

    [Fact]
    public void GivenHorizontalCanvas_WhenCreatingCamera_ThenReturnPixelSize()
    {
        // Arrange
        var horizontalSize = 200;
        var verticalSize = 125;
        var fieldOfView = MathF.PI / 2f;

        // Act
        var camera = new Camera(horizontalSize, verticalSize, fieldOfView);

        // Assert
        Assert.Equal(0.01f, camera.PixelSize);
    }

    [Fact]
    public void GivenVerticalCanvas_WhenCreatingCamera_ThenReturnPixelSize()
    {
        // Arrange
        var horizontalSize = 125;
        var verticalSize = 200;
        var fieldOfView = MathF.PI / 2f;

        // Act
        var camera = new Camera(horizontalSize, verticalSize, fieldOfView);

        // Assert
        Assert.Equal(0.01f, camera.PixelSize);
    }

    [Fact]
    [Trait("Category", "Ray")]
    public void GivenCamera_WhenCastingRayThroughCenter_ThenReturnRay()
    {
        // Arrange
        var camera = new Camera(201, 101, MathF.PI / 2);

        // Act
        var ray = camera.RayForPixel(100, 50);

        // Assert
        var expectedOrigin = new Point(0f, 0f, 0f);
        Assert.Equal(expectedOrigin.X, ray.Origin.X, ApproximateComparer.Default);
        Assert.Equal(expectedOrigin.Y, ray.Origin.Y, ApproximateComparer.Default);
        Assert.Equal(expectedOrigin.Z, ray.Origin.Z, ApproximateComparer.Default);

        var expectedDirection = new Vector(0f, 0f, -1f);
        Assert.Equal(expectedDirection.X, ray.Direction.X, ApproximateComparer.Default);
        Assert.Equal(expectedDirection.Y, ray.Direction.Y, ApproximateComparer.Default);
        Assert.Equal(expectedDirection.Z, ray.Direction.Z, ApproximateComparer.Default);
    }

    [Fact]
    [Trait("Category", "Ray")]
    public void GivenCamera_WhenCastingRayThroughCorner_ThenReturnRay()
    {
        // Arrange
        var camera = new Camera(201, 101, MathF.PI / 2);

        // Act
        var ray = camera.RayForPixel(0, 0);

        // Assert
        var expectedOrigin = new Point(0f, 0f, 0f);
        Assert.Equal(expectedOrigin.X, ray.Origin.X, ApproximateComparer.Default);
        Assert.Equal(expectedOrigin.Y, ray.Origin.Y, ApproximateComparer.Default);
        Assert.Equal(expectedOrigin.Z, ray.Origin.Z, ApproximateComparer.Default);

        var expectedDirection = new Vector(0.66519f, 0.33259f, -0.66851f);
        Assert.Equal(expectedDirection.X, ray.Direction.X, ApproximateComparer.Default);
        Assert.Equal(expectedDirection.Y, ray.Direction.Y, ApproximateComparer.Default);
        Assert.Equal(expectedDirection.Z, ray.Direction.Z, ApproximateComparer.Default);
    }

    [Fact]
    [Trait("Category", "Ray")]
    public void GivenTransformedCamera_WhenCastingRay_ThenReturnRay()
    {
        // Arrange
        var camera = new Camera(201, 101, MathF.PI / 2)
        {
            Transform = Matrix.RotateY(MathF.PI / 4) * Matrix.Translate(0, -2, 5)
        };

        // Act
        var ray = camera.RayForPixel(100, 50);

        // Assert
        var expectedOrigin = new Point(0f, 2f, -5f);
        Assert.Equal(expectedOrigin.X, ray.Origin.X, ApproximateComparer.Default);
        Assert.Equal(expectedOrigin.Y, ray.Origin.Y, ApproximateComparer.Default);
        Assert.Equal(expectedOrigin.Z, ray.Origin.Z, ApproximateComparer.Default);
        
        var expectedDirection = new Vector(MathF.Sqrt(2) / 2f, 0f, -(MathF.Sqrt(2) / 2f));
        Assert.Equal(expectedDirection.X, ray.Direction.X, ApproximateComparer.Default);
        Assert.Equal(expectedDirection.Y, ray.Direction.Y, ApproximateComparer.Default);
        Assert.Equal(expectedDirection.Z, ray.Direction.Z, ApproximateComparer.Default);
    }

    [Fact]
    [Trait("Category", "Canvas")]
    [Trait("Category", "Color")]
    public void GivenCamera_WhenRendering_ThenReturnCanvas()
    {
        // Arrange
        var world = World.Default();
        var from = new Point(0f, 0f, -5f);
        var to = new Point(0f, 0f, 0f);
        var up = new Vector(0f, 1f, 0f);
        var camera = new Camera(11, 11, MathF.PI / 2)
        {
            Transform = Matrix.ViewTransformation(from, to, up)
        };

        // Act
        var image = camera.Render(world);

        // Assert
        var pixel = image.GetPixel(5, 5);
        var expectedPixel = new Color(0.38066f, 0.47583f, 0.2855f);
        Assert.Equal(expectedPixel.R, pixel.R, ApproximateComparer.Default);
        Assert.Equal(expectedPixel.G, pixel.G, ApproximateComparer.Default);
        Assert.Equal(expectedPixel.B, pixel.B, ApproximateComparer.Default);
    }
}