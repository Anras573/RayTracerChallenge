using System;
using System.Threading.Tasks;

namespace RayTracerChallenge.Core;

public class Camera
{
    public readonly int HorizontalSize;
    public readonly int VerticalSize;
    public readonly float FieldOfView;
    public Matrix Transform;
    public readonly float PixelSize;
    private readonly float _halfWidth;
    private readonly float _halfHeight;

    public Camera(int horizontalSize, int verticalSize, float fieldOfView)
    {
        HorizontalSize = horizontalSize;
        VerticalSize = verticalSize;
        FieldOfView = fieldOfView;
        Transform = Matrix.IdentityMatrix();

        var halfView = MathF.Tan(fieldOfView / 2);
        var aspect = (float) horizontalSize / verticalSize;

        if (aspect >= 1)
        {
            _halfWidth = halfView;
            _halfHeight = halfView / aspect;
        }
        else
        {
            _halfWidth = halfView * aspect;
            _halfHeight = halfView;
        }

        PixelSize = _halfWidth * 2.0f / horizontalSize;
    }

    public Ray RayForPixel(int px, int py)
    {
        var xOffset = (px + 0.5f) * PixelSize;
        var yOffset = (py + 0.5f) * PixelSize;

        var worldX = _halfWidth - xOffset;
        var worldY = _halfHeight - yOffset;

        var pixel = Transform.Inverse() * new Point(worldX, worldY, -1.0f);
        var origin = Transform.Inverse() * new Point(0.0f, 0.0f, 0.0f);
        var direction = (pixel - origin).Normalize();

        return new Ray(origin, direction);
    }

    public Canvas Render(World world)
    {
        var canvas = new Canvas(HorizontalSize, VerticalSize);

        for (var y = 0; y < VerticalSize; y++)
        {
            for (var x = 0; x < HorizontalSize; x++)
            {
                var ray = RayForPixel(x, y);
                var color = world.ColorAt(ray);
                canvas.WritePixel(x, y, color);
            }
        }

        return canvas;
    }

    public Canvas ParallelRender(World world)
    {
        var canvas = new Canvas(HorizontalSize, VerticalSize);

        Parallel.For(0, VerticalSize, y =>
        {
            for (var x = 0; x < HorizontalSize; x++)
            {
                var ray = RayForPixel(x, y);
                var color = world.ColorAt(ray);
                canvas.WritePixel(x, y, color);
            }
        });

        return canvas;
    }
}