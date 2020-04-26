using RayTracerChallenge.ConsoleApplication.Actions.DrawProjectile;
using RayTracerChallenge.ConsoleApplication.Actions.PrintMatrix;
using RayTracerChallenge.ConsoleApplication.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RayTracerChallenge.ConsoleApplication
{
    public static class Program
    {
        public static void Main()
        {
            var actions = new List<Action>
            {
                DrawProjectile,
                PrintMatrix
            };

            ListOptions(actions);
        }

        private static void ListOptions(List<Action> actions)
        {
            Console.WriteLine("Choose option to run, or enter exit to exit program.");

            foreach (var (i, action) in actions.AsIndexable())
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
                var action = actions.ElementAtOrDefault(choice);

                if (action != null)
                {
                    action.Invoke();
                }
            }

            ListOptions(actions);
        }

        private static void PrintMatrix()
        {
            PrintMatrixService.Run();
        }

        private static void DrawProjectile()
        {
            DrawProjectileService.Run();
        }
    }
}
