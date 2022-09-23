using RayTracerChallenge.ConsoleApplication.Utilities;
using RayTracerChallenge.Core;
using System;
using System.IO;

namespace RayTracerChallenge.ConsoleApplication.Scenes.AnalogClock
{
    public class AnalogClockScene : IScene
    {
        public string Name => "Chapter 4 - Analog Clock";

        private const int CanvasSize = 500;
        private static float ClockRadius => CanvasSize * 3f / 8f;

        public void Render(ICanvasRenderer canvasRenderer)
        {            
            var path = ConsoleHelper.GetPath("output file");
            var fileName = Name;
            var filePath = Path.Combine(path, fileName);            

            var canvas = new Canvas(CanvasSize, CanvasSize);

            var twelve = new Point(0, 0, 1);
            DrawPoint(twelve, canvas);

            for (int i = 1; i < 12; i++)
            {
                var rotation = Matrix.RotateY(i * MathF.PI / 6);
                var point = rotation * twelve;

                DrawPoint(point, canvas);
            }

            canvasRenderer.Render(canvas, filePath);

            Console.WriteLine($"Image saved as {filePath}");
        }

        private void DrawPoint(Point point, Canvas canvas)
        {
            var x = point.X * ClockRadius + CanvasSize / 2;
            var y = point.Z * ClockRadius + CanvasSize / 2;

            canvas.WritePixel((int)x, (int)y, Colors.White);
        }
    }
}
