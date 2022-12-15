using System;
using System.Collections.Generic;
using System.IO;
using RayTracerChallenge.ConsoleApplication.Utilities;
using RayTracerChallenge.Core;
using RayTracerChallenge.Core.Patterns;
using RayTracerChallenge.Core.Shapes;

namespace RayTracerChallenge.ConsoleApplication.Scenes.SceneWithConesAndCylinders;

public class SceneWithConesAndCylinders : IScene
{
    public string Name => "Chapter 13 - Cones and Cylinders";
    public void Render(ICanvasRenderer canvasRenderer)
    {
        var path = ConsoleHelper.GetPath("output file");
        var fileName = Name;
        var filePath = Path.Combine(path, fileName);
        
        var wallScale = Matrix.Scale(0.5f, 0.5f, 0.5f);
        
        var floor = new Plane
        {
            Material =
            {
                Pattern = new Checker(Color.White, Color.Black)
            }
        };
        
        var leftWall = new Plane
        {
            Transform = Matrix.Translate(0.0f, 0.0f, 5.0f)
                        * Matrix.RotateY(-(MathF.PI / 4.0f))
                        * Matrix.RotateX(MathF.PI / 2.0f)
                        * wallScale,
            Material =
            {
                Color = new Color(0.7f, 0.8f, 0.9f)
            }
        };
        
        var leftWall2 = new Plane
        {
            Transform = Matrix.Translate(0.0f, 0.0f, -15.0f)
                        * Matrix.RotateY(-(MathF.PI / 4.0f))
                        * Matrix.RotateX(MathF.PI / 2.0f)
                        * wallScale,
            Material =
            {
                Color = new Color(0.7f, 0.8f, 0.9f)
            }
        };
        
        var rightWall = new Plane
        {
            Transform = Matrix.Translate(0.0f, 0.0f, 5.0f)
                        * Matrix.RotateY(MathF.PI / 4.0f)
                        * Matrix.RotateX(MathF.PI / 2.0f)
                        * wallScale,
            Material =
            {
                Color = new Color(0.9f, 0.8f, 0.7f)
            }
        };
        
        var rightWall2 = new Plane
        {
            Transform = Matrix.Translate(0.0f, 0.0f, -15.0f)
                        * Matrix.RotateY(MathF.PI / 4.0f)
                        * Matrix.RotateX(MathF.PI / 2.0f)
                        * wallScale,
            Material =
            {
                Color = new Color(0.9f, 0.8f, 0.7f)
            }
        };
        
        var mirror = new Cube
        {
            Transform = Matrix.Translate(4.0f, 3.0f, 2.0f)
                        * Matrix.RotateY(0.75f)
                        * Matrix.Scale(2.5f, 1.5f, 1.0f),
            Material =
            {
                Color = new Color(0.5f, 0.5f, 0.5f),
                Reflective = 1.0f
            }
        };
        
        var cone = new Cone
        {
             Transform = Matrix.Translate(0.0f, 2.5f, -2.0f)
                         * Matrix.RotateX(MathF.PI)  
                         * Matrix.Scale(0.75f, 1.0f, 0.75f),
            Closed = true,
            Minimum = 0.0f,
            Maximum = 1.0f,
            Material =
            {
                Color = Color.Green
            }
        };
        
        var cylinder = new Cylinder
        {
            Transform = Matrix.Translate(0.0f, 0.0f, -2.0f)
                        * Matrix.Scale(0.5f, 1.5f, 0.5f),
            Minimum = 0.0f,
            Maximum = 1.0f,
            Material =
            {
                Color = Color.Blue
            }
        };

        var sphere = new Sphere
        {
            Transform = Matrix.Translate(0.0f, 3.5f, -2.0f),
            Material =
            {
                Color = Color.Red,
                Reflective = 0.1f
            }
        };
        
        var world = new World
        {
            Lights = new List<Light>
            {
                new (new Point(-3.0f, 8.0f, -10.0f), new Color(0.9f, 0.9f, 0.9f)),
            },
            Objects = new List<Shape>
            {
                floor, leftWall, leftWall2, rightWall, rightWall2, cone, cylinder, sphere, mirror
            }
        };
        
        var camera = new Camera(800, 600, MathF.PI / 3)
        {
            Transform = Matrix.ViewTransformation(
                from: new Point(0.0f, 3.0f, -10.0f),
                to: new Point(0.0f, 1.0f, 0.0f),
                up: new Vector(0.0f, 1.0f, 0.0f))
        };

        var canvas = camera.ParallelRender(world);

        canvasRenderer.Render(canvas, filePath);
    }
}