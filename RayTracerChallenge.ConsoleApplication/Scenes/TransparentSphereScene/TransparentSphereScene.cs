using System;
using System.Collections.Generic;
using System.IO;
using RayTracerChallenge.ConsoleApplication.Utilities;
using RayTracerChallenge.Core;
using RayTracerChallenge.Core.Patterns;
using RayTracerChallenge.Core.Shapes;

namespace RayTracerChallenge.ConsoleApplication.Scenes.TransparentSphereScene;

public class TransparentSphereScene : IScene
{
    public string Name => "Chapter 11 - Transparent Sphere Scene";
    public void Render(ICanvasRenderer canvasRenderer)
    {
        var path = ConsoleHelper.GetPath("output file");
        var fileName = Name;
        var filePath = Path.Combine(path, fileName);

        var floor = new Plane
        {
            Material =
            {
                Reflective = 0.1f,
                Pattern = new Checker(Color.Black, Color.White)
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
                Pattern = new Checker(Color.Black, Color.White)
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
                Pattern = new Checker(Color.Black, Color.White)
            }
        };
        
        var transparent = new Sphere
        {
            Transform = Matrix.Translate(0.5f, 1.0f, -1.5f)
                        * Matrix.Scale(0.8f, 0.8f, 0.8f),
            Material =
            {
                Ambient = 0.0f,
                Diffuse = 0.0f,
                Transparency = 1.0f,
                RefractiveIndex = 5.0f
            }
        };
        
        var red = new Sphere
        {
            Transform = Matrix.Translate(0.5f, 1f, 3.0f)
                        * Matrix.Scale(0.9f, 0.9f, 0.9f),
            Material =
            {
                Color = new Color(1.0f, 0.0f, 0.0f),
                Specular = 0.0f
            }
        };
        
        var green = new Sphere
        {
            Transform = Matrix.Translate(1.5f, 0.5f, 2.25f)
                        * Matrix.Scale(0.4f, 0.4f, 0.4f),
            Material =
            {
                Color = new Color(0.0f, 1.0f, 0.0f),
            }
        };
        
        var blue = new Sphere
        {
            Transform = Matrix.Translate(-0.5f, 0.6f, 2.0f)
                        * Matrix.Scale(0.6f, 0.6f, 0.6f),
            Material =
            {
                Color = new Color(0.0f, 0.0f, 1.0f),
                Shininess = 10.0f
            }
        };
        
        var world = new World
        {
            Lights = new List<Light>
            {
                new (new Point(-8.0f, 15.0f, -18.0f), new Color(0.75f, 0.75f, 0.75f)),
            },
            Objects = new List<Shape>
            {
                floor, leftWall, rightWall, red, green, blue, transparent
            }
        };

        var camera = new Camera(800, 600, MathF.PI / 3)
        {
            Transform = Matrix.ViewTransformation(
                from: new Point(0.0f, 1.0f, -5.5f),
                to: new Point(0.0f, 1.0f, 0.0f),
                up: new Vector(0.0f, 1.0f, 0.0f))
        };

        var canvas = camera.ParallelRender(world);

        canvasRenderer.Render(canvas, filePath);
    }
}