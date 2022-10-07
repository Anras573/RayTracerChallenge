using RayTracerChallenge.ConsoleApplication.Utilities;
using RayTracerChallenge.Core;
using RayTracerChallenge.Core.Shapes;
using System.Collections.Generic;
using System.IO;
using System;

namespace RayTracerChallenge.ConsoleApplication.Scenes.SceneWithPlane;

public class SceneWithPlane : IScene
{
    public string Name => "Chapter 9 - Scene With Plane";
    public void Render(ICanvasRenderer canvasRenderer)
    {
        var path = ConsoleHelper.GetPath("output file");
        var fileName = Name;
        var filePath = Path.Combine(path, fileName);

        var floor = new Plane
        {
            Material = Material.Default
        };

        floor.Material.Color = new Color(1f, 0.9f, 0.9f);
        floor.Material.Specular = 0f;

        var middle = new Sphere
        {
            Transform = Matrix.Translate(-0.5f, 1f, 0.5f),
            Material = Material.Default
        };
        middle.Material.Color = new Color(0.1f, 1f, 0.5f);
        middle.Material.Diffuse = 0.7f;
        middle.Material.Specular = 0.3f;

        var right = new Sphere
        {
            Transform = Matrix.Translate(1.5f, 0.5f, -0.5f) * Matrix.Scale(0.5f, 0.5f, 0.5f),
            Material = Material.Default
        };
        right.Material.Color = new Color(0.5f, 1f, 0.1f);
        right.Material.Diffuse = 0.7f;
        right.Material.Specular = 0.3f;

        var left = new Sphere
        {
            Transform = Matrix.Translate(-1.5f, 0.33f, -0.75f) * Matrix.Scale(0.33f, 0.33f, 0.33f),
            Material = Material.Default
        };
        left.Material.Color = new Color(1f, 0.8f, 0.1f);
        left.Material.Diffuse = 0.7f;
        left.Material.Specular = 0.3f;

        var world = new World
        {
            Lights = new List<Light>
            {
                new (new Point(-10f, 10f, -10f), Color.White)
            },
            Objects = new List<Shape>
            {
                floor, middle, right, left
            }
        };

        var camera = new Camera(800, 400, MathF.PI / 3)
        {
            Transform = Matrix.ViewTransformation(
                from: new Point(0f, 1.5f, -5f),
                to: new Point(0f, 1f, 0f),
                up: new Vector(0f, 1f, 0f))
        };

        var canvas = camera.ParallelRender(world);

        canvasRenderer.Render(canvas, filePath);
    }
}