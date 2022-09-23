using RayTracerChallenge.Core;
using System;

namespace RayTracerChallenge.ConsoleApplication.Scenes.PrintMatrix
{
    public class PrintMatrixScene : IScene
    {
        public string Name => "Chapter 3 - Print Matrix";

        public void Render(ICanvasRenderer canvasRenderer)
        {
            Console.WriteLine("Generating 4x4 Matrix: ");
            var matrix = new Matrix(4, 4)
            {
                [0, 0] = -6f,
                [0, 1] = 1f,
                [0, 2] = 1f,
                [0, 3] = 6f,
                [1, 0] = -8f,
                [1, 1] = 5f,
                [1, 2] = 8f,
                [1, 3] = 6f,
                [2, 0] = -1f,
                [2, 1] = 0f,
                [2, 2] = 8f,
                [2, 3] = 2f,
                [3, 0] = -7f,
                [3, 1] = 1f,
                [3, 2] = -1f,
                [3, 3] = 1f
            };

            Console.WriteLine(matrix);

            Console.WriteLine("Generating subMatrix:");
            var subMatrix = matrix.Submatrix(2, 1);

            Console.WriteLine(subMatrix);
        }
    }
}
