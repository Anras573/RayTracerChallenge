﻿using RayTracerChallenge.ConsoleApplication.Utilities;
using RayTracerChallenge.Core;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RayTracerChallenge.ConsoleApplication.Scenes.DrawSphereWithLight
{
    public class DrawSphereWithLightScene : IScene
    {
        public string Name => "Draw Sphere With Light";

        private const int CanvasPixels = 400;
        private const float WallZ = 10f;
        private const float WallSize = 7f;

        public void Render(ICanvasRenderer canvasRenderer)
        {
            var sphere = new Sphere();
            sphere.Material.Color = new Color(1f, 0.2f, 1f);

            var light = new Light(new Point(-10f, 10f, -10f), Colors.White);

            var path = ConsoleHelper.GetPath("output file");
            var fileName = "sphere light";
            var filePath = Path.Combine(path, fileName);

            var canvas = new Canvas(CanvasPixels, CanvasPixels);
            var rayOrigin = new Point(0f, 0f, -5f);
            var pixelSize = WallSize / CanvasPixels;
            var half = WallSize / 2;

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

            canvasRenderer.Render(canvas, filePath);

            Console.WriteLine($"Image saved as {filePath}");
        }
    }
}
