using System;
using System.Collections.Generic;
using System.IO;
using RayTracerChallenge.ConsoleApplication.Utilities;
using RayTracerChallenge.Core;
using RayTracerChallenge.Core.Shapes;

namespace RayTracerChallenge.ConsoleApplication.Scenes.HexagonScene;

public class HexagonScene : IScene
{
    public string Name => $"Chapter 14 - Render a Hexagon";
    public void Render(ICanvasRenderer canvasRenderer)
    {
        var path = ConsoleHelper.GetPath("output file");
        var fileName = Name;
        var filePath = Path.Combine(path, fileName);

        var hexagon = new Group();

        for (var i = 0; i < 6; i++)
        {
            var side = HexagonSide();
            side.Transform = Matrix.RotateY(i * MathF.PI / 3);
            hexagon.Add(side);
        }

        var world = new World
        {
            Lights = new List<Light>
            {
                new (new Point(-10f, 10f, -10f), Color.White),
            },
            Objects = new List<Shape>
            {
                hexagon
            }
        };

        var camera = new Camera(800, 600, MathF.PI / 3)
        {
            Transform = Matrix.ViewTransformation(
                from: new Point(1.0f, 1.5f, -2.5f),
                to: new Point(0.0f, 0.0f, 0.0f),
                up: new Vector(0.0f, 1.0f, 0.0f))
        };
        
        var canvas = camera.ParallelRender(world);

        canvasRenderer.Render(canvas, filePath);
    }

    private static Group HexagonSide()
    {
        var side = new Group
        {
            HexagonCorner(),
            HexagonEdge()
        };

        return side;
    }

    private static Cylinder HexagonEdge()
    {
        var edge = new Cylinder
        {
            Minimum = 0.0f,
            Maximum = 1.0f,
            Transform = Matrix.Translate(0.0f, 0.0f, -1.0f)
                        * Matrix.RotateY(-MathF.PI / 6)
                        * Matrix.RotateZ(-MathF.PI / 2)
                        * Matrix.Scale(0.25f, 1.0f, 0.25f)
        };

        return edge;
    }

    private static Sphere HexagonCorner()
    {
        var corner = new Sphere
        {
            Transform = Matrix.Translate(0.0f, 0.0f, -1.0f)
                        * Matrix.Scale(0.25f, 0.25f, 0.25f)
        };
        return corner;
    }
} 