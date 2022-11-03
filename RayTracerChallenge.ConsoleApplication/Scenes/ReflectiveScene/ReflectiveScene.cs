using System;
using System.Collections.Generic;
using System.IO;
using RayTracerChallenge.ConsoleApplication.Utilities;
using RayTracerChallenge.Core;
using RayTracerChallenge.Core.Patterns;
using RayTracerChallenge.Core.Shapes;

namespace RayTracerChallenge.ConsoleApplication.Scenes.ReflectiveScene;

public class ReflectiveScene : IScene
{
    public string Name => "Chapter 11 - Reflective Scene";
    public void Render(ICanvasRenderer canvasRenderer)
    {
        var path = ConsoleHelper.GetPath("output file");
        var fileName = Name;
        var filePath = Path.Combine(path, fileName);

        var floor = new Plane
        {
            Material =
            {
                Reflective = 0.3f,
                Pattern = new Checker(Color.Black, new Color(0.3f, 0.3f, 0.3f))
            }
        };

        var leftWall = new Plane
        {
            Transform = Matrix.Translate(0f, 0f, 5f)
                        * Matrix.RotateY(-(MathF.PI / 4))
                        * Matrix.RotateX(MathF.PI / 2)
                        * Matrix.Scale(0.5f, 0.5f, 0.5f),
            Material =
            {
                Pattern = new Stripe(new Color(0.6f, 0.6f, 0.6f), new Color(0.9f, 0.9f, 0.9f))
            }
        };

        var rightWall = new Plane
        {
            Transform = Matrix.Translate(0f, 0f, 5f)
                        * Matrix.RotateY(MathF.PI / 4)
                        * Matrix.RotateX(MathF.PI / 2)
                        * Matrix.Scale(0.5f, 0.5f, 0.5f),
            Material =
            {
                Pattern = new Stripe(new Color(0.6f, 0.6f, 0.6f), new Color(0.9f, 0.9f, 0.9f))
            }
        };
        
        var sphere = new Sphere
        {
            Transform = Matrix.Translate(-0.5f, 1f, 0.5f),
            Material =
            {
                Color = new Color(1.0f, 0.1f, 0.5f),
            }
        };
        
        var world = new World
        {
            Lights = new List<Light>
            {
                new (new Point(-8.0f, 15.0f, -10.0f), Color.White)
            },
            Objects = new List<Shape>
            {
                floor, leftWall, rightWall, sphere
            }
        };

        var camera = new Camera(800, 600, MathF.PI / 3)
        {
            Transform = Matrix.ViewTransformation(
                from: new Point(0.0f, 2.0f, -6.5f),
                to: new Point(0.0f, 1.0f, 0.0f),
                up: new Vector(0.0f, 1.0f, 0.0f))
        };

        var canvas = camera.ParallelRender(world);

        canvasRenderer.Render(canvas, filePath);
    }
}