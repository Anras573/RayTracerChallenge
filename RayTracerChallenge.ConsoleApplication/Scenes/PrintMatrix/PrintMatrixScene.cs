using RayTracerChallenge.Core;
using System;

namespace RayTracerChallenge.ConsoleApplication.Scenes.PrintMatrix
{
    public class PrintMatrixScene : IScene
    {
        public string Name => "Print Matrix";

        public void Render(ICanvasRenderer canvasRenderer)
        {
            Console.WriteLine("Generating 4x4 Matrix: ");
            var matrix = new Matrix(4, 4);
            matrix[0, 0] = -6f;
            matrix[0, 1] = 1f;
            matrix[0, 2] = 1f;
            matrix[0, 3] = 6f;
            matrix[1, 0] = -8f;
            matrix[1, 1] = 5f;
            matrix[1, 2] = 8f;
            matrix[1, 3] = 6f;
            matrix[2, 0] = -1f;
            matrix[2, 1] = 0f;
            matrix[2, 2] = 8f;
            matrix[2, 3] = 2f;
            matrix[3, 0] = -7f;
            matrix[3, 1] = 1f;
            matrix[3, 2] = -1f;
            matrix[3, 3] = 1f;

            Console.WriteLine(matrix);

            Console.WriteLine("Generating subMatrix:");
            var submatrix = matrix.Submatrix(2, 1);

            Console.WriteLine(submatrix);
        }
    }
}
