using RayTracerChallenge.ConsoleApplication.Utilities;
using RayTracerChallenge.Core;
using RayTracerChallenge.Core.Shapes;
using RayTracerChallenge.Integration.ImageSharp;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RayTracerChallenge.ConsoleApplication.Scenes.MovingLight;

public class MovingLightScene : IScene
{
    public string Name => "Chapter 6 (gif) - Draw Sphere With Moving Lights";

    private const int CanvasPixels = 400;
    private const float WallZ = 10f;
    private const float WallSize = 7f;
    private const float PixelSize = WallSize / CanvasPixels;
    private const float Half = WallSize / 2;
    private const int MaxFrameNumber = 42;

    public void Render(ICanvasRenderer canvasRenderer)
    {
        var renderer = new ImageSharpGifRenderer(CanvasPixels, CanvasPixels, 8);

        var path = ConsoleHelper.GetPath("output file");
        var fileName = Name;
        var filePath = Path.Combine(path, fileName);

        var sphere = new Sphere
        {
            Material =
            {
                Color = new Color(1f, 0.2f, 1f)
            }
        };

        var canvas = new Canvas(CanvasPixels, CanvasPixels);
        var rayOrigin = new Point(0f, 0f, -5f);

        var currentFrameNumber = 1;
        

        for (var i = -10f; i < 11f; i += 0.5f)
        {
            var light = new Light(new Point(i, 10f, -10f), Color.White);

            Console.WriteLine($"Rendering frame {currentFrameNumber++} out of {MaxFrameNumber}");

            Parallel.For(0, CanvasPixels, y =>
            {
                var worldY = Half - PixelSize * y;
                for (var x = 0; x < CanvasPixels; x++)
                {
                    var worldX = -Half + PixelSize * x;

                    var position = new Point(worldX, worldY, WallZ);
                    var direction = position - rayOrigin;

                    var ray = new Ray(rayOrigin, direction.Normalize());
                    var intersection = sphere.Intersects(ray);

                    if (intersection.Hit() == null) continue;
                    
                    var point = ray.Position(intersection.Hit().TimeValue);
                    var normal = intersection.Hit().Object.NormalAt(point);
                    var eye = ray.Direction;
                    var color = sphere.Material.Lightning(sphere, light, point, eye, normal);

                    canvas.WritePixel(x, y, color);
                }
            });

            renderer.RenderFrame(canvas);
        }

        renderer.Render(filePath);

        Console.WriteLine($"Image saved as {filePath}");
    }
}