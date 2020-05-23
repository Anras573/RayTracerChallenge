using RayTracerChallenge.ConsoleApplication.Utilities;
using RayTracerChallenge.Core;
using System;
using System.IO;
using Tuple = RayTracerChallenge.Core.Tuple;

namespace RayTracerChallenge.ConsoleApplication.Actions.AnalogClock
{
    public static class AnalogClockService
    {
        private const int canvasSize = 500;
        private static float ClockRadius => canvasSize * 3f / 8f;

        public static void Run()
        {
            var path = ConsoleHelper.GetPath("output file");
            var fileName = "analogClock.ppm";
            var filePath = Path.Combine(path, fileName);            

            var canvas = new Canvas(canvasSize, canvasSize);

            var twelve = Tuple.Point(0, 0, 1);
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

        private static void DrawPoint(Tuple point, Canvas canvas)
        {
            var x = point.X * ClockRadius + canvasSize / 2;
            var y = point.Z * ClockRadius + canvasSize / 2;

            canvas.WritePixel((int)x, (int)y, Color.White);
        }
    }
}
