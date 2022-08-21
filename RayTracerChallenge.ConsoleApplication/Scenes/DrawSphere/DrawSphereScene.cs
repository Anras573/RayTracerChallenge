using RayTracerChallenge.ConsoleApplication.Utilities;
using RayTracerChallenge.Core;
using System;
using System.IO;

namespace RayTracerChallenge.ConsoleApplication.Scenes.DrawSphere
{
    public  class DrawSphereScene : IScene
    {
        public string Name => "Draw Sphere";

        private const int CanvasPixels = 100;
        private const float WallZ = 10f;
        private const float WallSize = 7f;

        public void Render(ICanvasRenderer canvasRenderer)
        {
            var path = ConsoleHelper.GetPath("output file");
            var fileName = "sphere.ppm";
            var filePath = Path.Combine(path, fileName);

            var canvas = new Canvas(CanvasPixels, CanvasPixels);
            var rayOrigin = new Point(0f, 0f, -5f);
            var pixelSize = WallSize / CanvasPixels;
            var half = WallSize / 2;
            var color = Colors.Red;
            var sphere = new Sphere();

            //sphere.Transform = Matrix.Scale(1f, .5f, 1f);
            //sphere.Transform = Matrix.Scale(.5f, 1f, 1f);
            //sphere.Transform = Matrix.RotateZ(MathF.PI / 4f) * Matrix.Scale(.5f, 1f, 1f);
            //sphere.Transform = Matrix.Shear(1f, 0f, 0f, 0f, 0f, 0f) * Matrix.Scale(.5f, 1f, 1f);

            for (int y = 0; y < CanvasPixels; y++)
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
                        canvas.WritePixel(x, y, color);
                    }
                }
            }

            canvasRenderer.Render(canvas, filePath);

            Console.WriteLine($"Image saved as {filePath}");
        }
    }
}
