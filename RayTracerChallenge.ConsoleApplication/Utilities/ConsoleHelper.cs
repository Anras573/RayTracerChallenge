using System;
using System.IO;

namespace RayTracerChallenge.ConsoleApplication.Utilities;

public static class ConsoleHelper
{
    public static string GetPath(string name)
    {
        Console.WriteLine($"Enter path for {name}:");

        var path = string.Empty;

        while(string.IsNullOrWhiteSpace(path))
        {
            path = Console.ReadLine();

            if (Directory.Exists(path)) continue;
            
            Console.WriteLine($"{path} does not exist!");
            path = string.Empty;
        }

        return path;
    }
}