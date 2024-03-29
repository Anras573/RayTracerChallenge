﻿using RayTracerChallenge.ConsoleApplication.Scenes;
using RayTracerChallenge.ConsoleApplication.Scenes.AnalogClock;
using RayTracerChallenge.ConsoleApplication.Scenes.Cover;
using RayTracerChallenge.ConsoleApplication.Scenes.DrawProjectile;
using RayTracerChallenge.ConsoleApplication.Scenes.DrawSphere;
using RayTracerChallenge.ConsoleApplication.Scenes.DrawSphereWithLight;
using RayTracerChallenge.ConsoleApplication.Scenes.MovingLight;
using RayTracerChallenge.ConsoleApplication.Scenes.PrintMatrix;
using RayTracerChallenge.ConsoleApplication.Scenes.ReflectiveScene;
using RayTracerChallenge.ConsoleApplication.Scenes.SceneWithConesAndCylinders;
using RayTracerChallenge.ConsoleApplication.Scenes.SceneWithCube;
using RayTracerChallenge.ConsoleApplication.Scenes.SceneWithPlane;
using RayTracerChallenge.ConsoleApplication.Scenes.SceneWithSpheres;
using RayTracerChallenge.ConsoleApplication.Scenes.TransparentSphereScene;
using RayTracerChallenge.ConsoleApplication.Utilities;
using RayTracerChallenge.Core;
using RayTracerChallenge.Integration.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using RayTracerChallenge.ConsoleApplication.Scenes.HexagonScene;

namespace RayTracerChallenge.ConsoleApplication;

public static class Program
{
    //private static readonly ICanvasRenderer _canvasRenderer = new PpmCanvasRenderer();
    private static readonly ICanvasRenderer _canvasRenderer = new ImageSharpCanvasRenderer(".png");

    private static readonly List<IScene> _scenes = new()
    {
        new DrawProjectileScene(),
        new PrintMatrixScene(),
        new AnalogClockScene(),
        new DrawSphereScene(),
        new DrawSphereWithLightScene(),
        new MovingLightScene(),
        new RenderSpheresScene(),
        new SceneWithPlane(),
        new ReflectiveScene(),
        new TransparentSphereScene(),
        new SceneWithCube(),
        new SceneWithConesAndCylinders(),
        new HexagonScene(),
        new CoverScene()
    };

    public static void Main()
    {
        ListOptions();
    }

    private static void ListOptions()
    {
        var keepGoing = true;
        while (keepGoing)
        {
            Console.WriteLine("Choose option to run, or enter exit to exit program.");

            foreach (var (i, scene) in _scenes.AsIndexable())
            {
                var prepend = i < 10 ? " " : string.Empty;
                Console.WriteLine($"{prepend}{i} - {scene.Name}");
            }

            var input = Console.ReadLine()?.Trim();

            if (string.Equals(input, "exit", StringComparison.OrdinalIgnoreCase))
            {
                keepGoing = false;
            }
            else if (int.TryParse(input, out var choice))
            {
                var scene = _scenes.ElementAtOrDefault(choice);

                scene?.Render(_canvasRenderer);
            }
        }
    }
}