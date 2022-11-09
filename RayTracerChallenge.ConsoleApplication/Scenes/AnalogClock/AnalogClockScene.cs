using RayTracerChallenge.ConsoleApplication.Utilities;
using RayTracerChallenge.Core;
using System;
using System.IO;

namespace RayTracerChallenge.ConsoleApplication.Scenes.AnalogClock;

public class AnalogClockScene : IScene
{
    public string Name => "Chapter 4 - Analog Clock";

    private const int CanvasSize = 500;
    private static float ClockRadius => CanvasSize * 3.0f / 8.0f;

    public void Render(ICanvasRenderer canvasRenderer)
    {            
        var path = ConsoleHelper.GetPath("output file");
        var fileName = Name;
        var filePath = Path.Combine(path, fileName);            

        var canvas = new Canvas(CanvasSize, CanvasSize);

        var twelve = new Point(0.0f, 0.0f, 1.0f);
        DrawPoint(twelve, canvas);

        for (var i = 1; i < 12; i++)
        {
            var rotation = Matrix.RotateY(i * MathF.PI / 6.0f);
            var point = rotation * twelve;

            DrawPoint(point, canvas);
        }

        canvasRenderer.Render(canvas, filePath);

        Console.WriteLine($"Image saved as {filePath}");
    }

    private static void DrawPoint(Point point, Canvas canvas)
    {
        var x = point.X * ClockRadius + CanvasSize / 2.0f;
        var y = point.Z * ClockRadius + CanvasSize / 2.0f;

        canvas.WritePixel((int)x, (int)y, Color.White);
    }
}