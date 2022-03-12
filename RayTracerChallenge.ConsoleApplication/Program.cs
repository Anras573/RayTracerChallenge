using RayTracerChallenge.ConsoleApplication.Scenes;
using RayTracerChallenge.ConsoleApplication.Scenes.AnalogClock;
using RayTracerChallenge.ConsoleApplication.Scenes.DrawProjectile;
using RayTracerChallenge.ConsoleApplication.Scenes.DrawSphere;
using RayTracerChallenge.ConsoleApplication.Scenes.DrawSphereWithLight;
using RayTracerChallenge.ConsoleApplication.Scenes.PrintMatrix;
using RayTracerChallenge.ConsoleApplication.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RayTracerChallenge.ConsoleApplication
{
    public static class Program
    {
        private static readonly List<IScene> _scenes = new List<IScene>
        {
            new DrawProjectileScene(),
            new PrintMatrixScene(),
            new AnalogClockScene(),
            new DrawSphereScene(),
            new DrawSphereWithLightScene()
        };

        public static void Main()
        {
            ListOptions();
        }

        private static void ListOptions()
        {
            Console.WriteLine("Choose option to run, or enter exit to exit program.");

            foreach (var (i, scene) in _scenes.AsIndexable())
            {
                Console.WriteLine($"{i} - {scene.Name}");
            }

            var input = Console.ReadLine().Trim();

            if (string.Equals(input, "exit", StringComparison.OrdinalIgnoreCase))
            {
                System.Environment.Exit(0);
            }

            if (int.TryParse(input, out var choice))
            {
                var scene = _scenes.ElementAtOrDefault(choice);

                if (scene != null)
                {
                    scene.Render();
                }
            }

            ListOptions();
        }
    }
}
