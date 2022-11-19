using System;
using System.Collections.Generic;
using System.IO;
using RayTracerChallenge.ConsoleApplication.Utilities;
using RayTracerChallenge.Core;
using RayTracerChallenge.Core.Patterns;
using RayTracerChallenge.Core.Shapes;

namespace RayTracerChallenge.ConsoleApplication.Scenes.SceneWithCube;

public class SceneWithCube : IScene
{
    public string Name => "Chapter 12 - Cube Scene";
    public void Render(ICanvasRenderer canvasRenderer)
    {
        var path = ConsoleHelper.GetPath("output file");
        var fileName = Name;
        var filePath = Path.Combine(path, fileName);

        var lightBrown = new Color(0.36f, 0.28f, 0.19f);
        var darkBrown = new Color(0.28f, 0.21f, 0.15f);
        var wallScale = Matrix.Scale(0.5f, 0.5f, 0.5f);
        var legScale = Matrix.Scale(0.1f, 1.5f, 0.1f);
        var tableRotation = Matrix.RotateY(0.75f);
        
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
            Transform = Matrix.Translate(0.0f, 0.0f, 5.0f)
                        * Matrix.RotateY(-(MathF.PI / 4.0f))
                        * Matrix.RotateX(MathF.PI / 2.0f)
                        * wallScale,
            Material =
            {
                Pattern = new Stripe(lightBrown, darkBrown)
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
                Pattern = new Stripe(lightBrown, darkBrown)
            }
        };
        
        var rightWallBehind = new Plane
        {
            Transform = Matrix.Translate(0.0f, 0.0f, -28.0f)
                        * Matrix.RotateY(MathF.PI / 4.0f)
                        * Matrix.RotateX(MathF.PI / 2.0f)
                        * wallScale,
            Material =
            {
                Pattern = new Stripe(lightBrown, darkBrown)
            }
        };

        var leg = new Cube
        {
            Transform = Matrix.Translate(0.0f, 0.0f, -0.5f)
                        * legScale
                        * tableRotation,
            Material =
            {
                Color = lightBrown
            }
        };
        
        var leg2 = new Cube // Green
        {
            Transform = Matrix.Translate(-2.0f, 0.0f, -2.0f)
                        * tableRotation
                        * legScale,
            Material =
            {
                Color = lightBrown
            }
        };
        
        var leg3 = new Cube // Blue
        {
            Transform = Matrix.Translate(2.0f, 0.0f, -2.0f)
                        * tableRotation
                        * legScale,
            Material =
            {
                Color = lightBrown
            }
        };
        
        var leg4 = new Cube // Red
        {
            Transform = Matrix.Translate(0.0f, 0.0f, -3.5f)
                        * tableRotation
                        * legScale,
            Material =
            {
                Color = lightBrown
            }
        };

        var tableTop = new Cube
        {
            Transform = Matrix.Scale(2.25f, 0.1f, 1.5f)
                        * tableRotation
                        * Matrix.Translate(0.9f, 14.5f, -1.0f),
            Material =
            {
                Pattern = new Checker(lightBrown, darkBrown)
            }
        };
        
        var mirror = new Cube
        {
            Transform = Matrix.Translate(4.0f, 3.0f, 2.0f)
                        * tableRotation
                        * Matrix.Scale(2.5f, 1.5f, 1.0f),
            Material =
            {
                Color = new Color(0.5f, 0.5f, 0.5f),
                Reflective = 1.0f
            }
        };

        var cube = new Cube
        {
            Transform = Matrix.Translate(0.0f, 1.75f, -2.0f)
                        * tableRotation
                        * Matrix.Scale(0.2f, 0.2f, 0.2f),
            Material =
            {
                Pattern = new Checker(new Color(0.9f, 0.75f, 0.0f), new Color(0.6f, 0.5f, 0.0f))
            }
        };
        
        var world = new World
        {
            Lights = new List<Light>
            {
                new (new Point(-4.0f, 8.0f, -10.0f), new Color(0.9f, 0.9f, 0.9f)),
            },
            Objects = new List<Shape>
            {
                floor, leftWall, rightWall, leg, leg2, leg3, leg4, tableTop, mirror, rightWallBehind, cube
            }
        };
        
        var camera = new Camera(800, 600, MathF.PI / 3)
        {
            Transform = Matrix.ViewTransformation(
                from: new Point(0.0f, 4.0f, -10.0f),
                to: new Point(0.0f, 1.0f, 0.0f),
                up: new Vector(0.0f, 1.0f, 0.0f))
        };

        var canvas = camera.ParallelRender(world);

        canvasRenderer.Render(canvas, filePath);
    }
}