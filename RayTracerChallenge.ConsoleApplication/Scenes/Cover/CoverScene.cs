using System;
using System.Collections.Generic;
using System.IO;
using RayTracerChallenge.ConsoleApplication.Utilities;
using RayTracerChallenge.Core;
using RayTracerChallenge.Core.Shapes;

namespace RayTracerChallenge.ConsoleApplication.Scenes.Cover;

public class CoverScene : IScene
{
    public string Name => "Appendix 1 - Render the Cover Image";
    public void Render(ICanvasRenderer canvasRenderer)
    {
        var path = ConsoleHelper.GetPath("output file");
        var fileName = Name;
        var filePath = Path.Combine(path, fileName);

        var whiteMaterial = GetMaterial(Color.White);
        var blueMaterial = GetMaterial(new Color(0.537f, 0.831f, 0.914f));
        var redMaterial = GetMaterial(new Color(0.941f, 0.322f, 0.388f));
        var purpleMaterial = GetMaterial(new Color(0.373f, 0.404f, 0.55f));

        var largeScale = new Scale(3.5f, 3.5f, 3.5f);
        var mediumScale = new Scale(3.0f, 3.0f, 3.0f);
        var smallScale = new Scale(2.0f, 2.0f, 2.0f);
        
        var world = new World
        {
            Lights = new List<Light>
            {
                new (new Point(50.0f, 100.0f, -50.0f), Color.White),
                new (new Point(-400.0f, 50.0f, -10.0f), new Color(0.2f, 0.2f, 0.2f))
            },
            Objects = new List<Shape>
            {
                new Plane
                {
                    Material =
                    {
                        Color = Color.White,
                        Ambient = 1.0f,
                        Diffuse = 0.0f,
                        Specular = 0.0f
                    },
                    Transform = Matrix.Translate(0.0f, 0.0f, 500.0f) 
                                * Matrix.RotateX(MathF.PI / 2)
                },
                new Sphere
                {
                    Material = new Material(new Color(0.373f, 0.404f, 0.55f))
                    {
                        Diffuse = 0.2f,
                        Ambient = 0.0f,
                        Specular = 1.0f,
                        Shininess = 200.0f,
                        Reflective = 0.7f,
                        Transparency = 0.7f,
                        RefractiveIndex = Core.Utilities.RefractiveIndex.Glass
                    },
                    Transform = GetTransform(1.0f, -1.0f, 1.0f, largeScale)
                },
                new Cube
                {
                    Material = whiteMaterial,
                    Transform = GetTransform(4.0f, 0.0f, 0.0f, mediumScale)
                },
                new Cube
                {
                    Material = blueMaterial,
                    Transform = GetTransform(8.5f, 1.5f, -0.5f, largeScale)
                },
                new Cube
                {
                    Material = redMaterial,
                    Transform = GetTransform(0.0f, 0.0f, 4.0f, largeScale)
                },
                new Cube
                {
                    Material = whiteMaterial,
                    Transform = GetTransform(4.0f, 0.0f, 4.0f, mediumScale)
                },
                new Cube
                {
                    Material = purpleMaterial,
                    Transform = GetTransform(7.5f, 0.5f, 4.0f, mediumScale)
                },
                new Cube
                {
                    Material = whiteMaterial,
                    Transform = GetTransform(-0.25f, 0.25f, 8.0f, mediumScale)
                },
                new Cube
                {
                    Material = blueMaterial,
                    Transform = GetTransform(4.0f, 1.0f, 7.5f, largeScale)
                },
                new Cube
                {
                    Material = redMaterial,
                    Transform = GetTransform(10.0f, 2.0f, 7.5f, mediumScale)
                },
                new Cube
                {
                    Material = whiteMaterial,
                    Transform = GetTransform(8.0f, 2.0f, 12.0f, smallScale)
                },
                new Cube
                {
                    Material = whiteMaterial,
                    Transform = GetTransform(20.0f, 1.0f, 9.0f, smallScale)
                },
                new Cube
                {
                    Material = blueMaterial,
                    Transform = GetTransform(-0.5f, -5.0f, 0.25f, largeScale)
                },
                new Cube
                {
                    Material = redMaterial,
                    Transform = GetTransform(4.0f, -4.0f, 0.0f, largeScale)
                },
                new Cube
                {
                    Material = whiteMaterial,
                    Transform = GetTransform(8.5f, -4.0f, 0.0f, largeScale)
                },
                new Cube
                {
                    Material = whiteMaterial,
                    Transform = GetTransform(0.0f, -4.0f, 4.0f, largeScale)
                },
                new Cube
                {
                    Material = purpleMaterial,
                    Transform = GetTransform(-0.5f, -4.5f, 8.0f, largeScale)
                },
                new Cube
                {
                    Material = whiteMaterial,
                    Transform = GetTransform(0.0f, -8.0f, 4.0f, largeScale)
                },
                new Cube
                {
                    Material = whiteMaterial,
                    Transform = GetTransform(-0.5f, -8.5f, 8.0f, largeScale)
                }
            }
        };

        var camera = new Camera(600, 600, 0.785f)
        {
            Transform = Matrix.ViewTransformation(
                from: new Point(-6.0f, 6.0f, -10.0f),
                to: new Point(6.0f, 0.0f, 6.0f),
                up: new Vector(-0.45f, 1.0f, 0.0f))
        };

        var canvas = camera.ParallelRender(world);

        canvasRenderer.Render(canvas, filePath);
    }

    private static Material GetMaterial(Color color)
    {
        return new Material(color)
        {
            Diffuse = 0.7f,
            Ambient = 0.1f,
            Specular = 0.0f,
            Reflective = 0.1f
        };
    }

    private static Matrix GetTransform(float x, float y, float z, Scale scale)
    {
        return Matrix.Translate(1.0f, -1.0f, 1.0f)
               * Matrix.Translate(x, y, z)
               * Matrix.Scale(0.5f, 0.5f, 0.5f)
               * Matrix.Scale(scale.X, scale.Y, scale.Z);
    }

    private record struct Scale(float X, float Y, float Z);
}