using RayTracerChallenge.ConsoleApplication.Utilities;
using RayTracerChallenge.Core;
using System;
using System.IO;

namespace RayTracerChallenge.ConsoleApplication.Scenes.DrawProjectile;

public class DrawProjectileScene : IScene
{
    public string Name => "Chapter 2 - Draw Projectile";

    public void Render(ICanvasRenderer canvasRenderer)
    {
        var path = ConsoleHelper.GetPath("output file");
        var fileName = Name;
        var filePath = Path.Combine(path, fileName);

        var projectile = new Projectile(
            Position: new Point(0.0f, 1.0f, 0.0f),
            Velocity: new Vector(1f, 1.8f, 0f).Normalize() * 11.25f);

        var environment = new Environment(
            Gravity: new Vector(0f, -.1f, 0f),
            Wind: new Vector(-.01f, 0f, 0f));

        var canvas = new Canvas(900, 550);

        var numberOfTicks = 0;

        Console.WriteLine("Shooting bullet!");
        Console.WriteLine($"Bullets position: X: {projectile.Position.X} Y: {projectile.Position.Y} Z: {projectile.Position.Z}");
        DrawOnCanvas(projectile, canvas);

        while (projectile.Position.Y >= 0f)
        {
            projectile = Tick(environment, projectile);
            Console.WriteLine($"Bullets position: X: {projectile.Position.X} Y: {projectile.Position.Y} Z: {projectile.Position.Z}");
            numberOfTicks++;
            DrawOnCanvas(projectile, canvas);
        }

        Console.WriteLine("Bullet hit the ground!");
        Console.WriteLine($"It took {numberOfTicks} ticks to hit the ground!");
        DrawOnCanvas(projectile, canvas);

        canvasRenderer.Render(canvas, filePath);

        Console.WriteLine($"Image saved as {filePath}");
    }

    private static Projectile Tick(Environment environment, Projectile projectile)
    {
        var position = projectile.Position + projectile.Velocity;
        var velocity = projectile.Velocity + environment.Gravity + environment.Wind;

        return new Projectile(position, velocity);
    }

    private static void DrawOnCanvas(Projectile projectile, Canvas canvas)
    {
        var y = canvas.Height - projectile.Position.Y;
        var x = projectile.Position.X;

        x = x.Clamp(canvas.Width - 1.0f);
        y = y.Clamp(canvas.Height - 1.0f);

        canvas.WritePixel((int)x, (int)y, Color.Blue);
    }
}