using RayTracerChallenge.ConsoleApplication.Actions.AnalogClock;
using RayTracerChallenge.ConsoleApplication.Actions.DrawProjectile;
using RayTracerChallenge.ConsoleApplication.Actions.DrawSphere;
using RayTracerChallenge.ConsoleApplication.Actions.PrintMatrix;
using RayTracerChallenge.ConsoleApplication.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RayTracerChallenge.ConsoleApplication
{
    public static class Program
    {
        private static readonly List<Action> _actions = new List<Action>
        {
            DrawProjectile,
            PrintMatrix,
            DrawAnalogClock,
            DrawSphere
        };

        public static void Main()
        {
            ListOptions();
        }

        private static void ListOptions()
        {
            Console.WriteLine("Choose option to run, or enter exit to exit program.");

            foreach (var (i, action) in _actions.AsIndexable())
            {
                Console.WriteLine($"{i} - {action.Method.Name}");
            }

            var input = Console.ReadLine().Trim();

            if (string.Equals(input, "exit", StringComparison.OrdinalIgnoreCase))
            {
                System.Environment.Exit(0);
            }

            if (int.TryParse(input, out var choice))
            {
                var action = _actions.ElementAtOrDefault(choice);

                if (action != null)
                {
                    action.Invoke();
                }
            }

            ListOptions();
        }

        private static void PrintMatrix()
        {
            PrintMatrixScene.Render();
        }

        private static void DrawProjectile()
        {
            DrawProjectileScene.Render();
        }

        private static void DrawAnalogClock()
        {
            AnalogClockScene.Render();
        }

        private static void DrawSphere()
        {
            DrawSphereScene.Render();
        }
    }
}
