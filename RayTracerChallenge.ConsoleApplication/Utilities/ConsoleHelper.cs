using System;
using System.IO;

namespace RayTracerChallenge.ConsoleApplication.Utilities
{
    public static class ConsoleHelper
    {
        public static string GetPath(string name)
        {
            Console.WriteLine($"Enter path for {name}:");

            string path = string.Empty;

            while(string.IsNullOrWhiteSpace(path))
            {
                path = Console.ReadLine();

                if (!Directory.Exists(path))
                {
                    Console.WriteLine($"{path} does not exitst!");
                    path = string.Empty;
                }
            }

            return path;
        }
    }
}
