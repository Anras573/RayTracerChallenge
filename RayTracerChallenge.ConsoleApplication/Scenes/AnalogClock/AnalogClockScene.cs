using RayTracerChallenge.ConsoleApplication.Utilities;
using RayTracerChallenge.Core;
using System;
using System.IO;

namespace RayTracerChallenge.ConsoleApplication.Scenes.AnalogClock
{
    public class AnalogClockScene : IScene
    {
        public string Name => "Analog Clock";

        private const int canvasSize = 500;
        private float ClockRadius => canvasSize * 3f / 8f;

        public void Render()
        {
            var path = ConsoleHelper.GetPath("output file");
            var fileName = "analogClock.ppm";
            var filePath = Path.Combine(path, fileName);            

            var canvas = new Canvas(canvasSize, canvasSize);

            var twelve = new Point(0, 0, 1);
            DrawPoint(twelve, canvas);

            for (int i = 1; i < 12; i++)
            {
                var rotation = Matrix.RotateY(i * MathF.PI / 6);
                var point = rotation * twelve;

                DrawPoint(point, canvas);
            }

            var imageAsString = canvas.ToPpm();

            File.WriteAllText(filePath, imageAsString);

            Console.WriteLine($"Image saved as {filePath}");
        }

        private void DrawPoint(Point point, Canvas canvas)
        {
            var x = point.X * ClockRadius + canvasSize / 2;
            var y = point.Z * ClockRadius + canvasSize / 2;

            canvas.WritePixel((int)x, (int)y, Colors.White);
        }
    }
}
