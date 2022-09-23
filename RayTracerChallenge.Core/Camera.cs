using System;
using System.Globalization;
using System.Threading.Tasks;

namespace RayTracerChallenge.Core;

public class Camera
{
    public int HorizontalSize;
    public int VerticalSize;
    public float FieldOfView;
    public Matrix Transform;
    public float PixelSize;
    public float HalfWidth;
    public float HalfHeight;

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
            HalfWidth = halfView;
            HalfHeight = halfView / aspect;
        }
        else
        {
            HalfWidth = halfView * aspect;
            HalfHeight = halfView;
        }

        PixelSize = (HalfWidth * 2) / horizontalSize;
    }

    public Ray RayForPixel(int px, int py)
    {
        var xOffset = (px + 0.5f) * PixelSize;
        var yOffset = (py + 0.5f) * PixelSize;

        var worldX = HalfWidth - xOffset;
        var worldY = HalfHeight - yOffset;

        var pixel = Transform.Inverse() * new Point(worldX, worldY, -1);
        var origin = Transform.Inverse() * new Point(0f, 0f, 0f);
        var direction = (pixel - origin).Normalize();

        return new Ray(origin, direction);
    }

    public Canvas Render(World world)
    {
        var canvas = new Canvas(HorizontalSize, VerticalSize);

        for (int y = 0; y < VerticalSize; y++)
        {
            for (int x = 0; x < HorizontalSize; x++)
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

        Parallel.For(0, VerticalSize, (y =>
        {
            for (int x = 0; x < HorizontalSize; x++)
            {
                var ray = RayForPixel(x, y);
                var color = world.ColorAt(ray);
                canvas.WritePixel(x, y, color);
            }
        }));

        return canvas;
    }
}