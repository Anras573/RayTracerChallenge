using RayTracerChallenge.ConsoleApplication.Utilities;
using RayTracerChallenge.Core;
using RayTracerChallenge.Integration.ImageSharp;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RayTracerChallenge.ConsoleApplication.Scenes.MovingLight
{
    public class MovingLightScene : IScene
    {
        public string Name => "Draw Sphere With Moving Light";

        private const int CanvasPixels = 400;
        private const float WallZ = 10f;
        private const float WallSize = 7f;

        public void Render(ICanvasRenderer canvasRenderer)
        {
            var renderer = new ImageSharpGifRenderer(CanvasPixels, CanvasPixels, 8);

            var path = ConsoleHelper.GetPath("output file");
            var fileName = "sphere light";
            var filePath = Path.Combine(path, fileName);

            var sphere = new Sphere();
            sphere.Material.Color = new Color(1f, 0.2f, 1f);

            var canvas = new Canvas(CanvasPixels, CanvasPixels);
            var rayOrigin = new Point(0f, 0f, -5f);
            var pixelSize = WallSize / CanvasPixels;
            var half = WallSize / 2;

            var currentFrameNumber = 1;
            var maxFrameNumber = 42;

            for (float i = -10f; i < 11f; i += 0.5f)
            {
                var light = new Light(new Point(i, 10f, -10f), Colors.White);

                Console.WriteLine($"Rendering frame {currentFrameNumber++} out of {maxFrameNumber}");

                Parallel.For(0, CanvasPixels, (y) =>
                {
                    var worldY = half - pixelSize * y;
                    for (int x = 0; x < CanvasPixels; x++)
                    {
                        var worldX = -half + pixelSize * x;

                        var position = new Point(worldX, worldY, WallZ);
                        var direction = position - rayOrigin;

                        var ray = new Ray(rayOrigin, direction.Normalize());
                        var intersection = sphere.Intersects(ray);

                        if (intersection.Hit() != null)
                        {
                            var point = ray.Position(intersection.Hit().TimeValue);
                            var normal = intersection.Hit().Object.NormalAt(point);
                            var eye = ray.Direction;
                            var color = sphere.Material.Lightning(light, point, eye, normal);

                            canvas.WritePixel(x, y, color);
                        }
                    }
                });

                renderer.RenderFrame(canvas);
            }

            renderer.Render(filePath);

            Console.WriteLine($"Image saved as {filePath}");
        }
    }
}
