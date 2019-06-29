using RayTracerChallenge.Core;
using System;
using System.IO;

namespace RayTracerChallenge.ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var filename = "projectile.ppm";

            //var projectile = new Projectile {
            //    Position = Tupple.Point(0f, 1f, 0f),
            //    Velocity = Tupple.Vector(1f, 1.8f, 0f).Normalize() * 11.25f
            //};

            //var environment = new Environment {
            //    Gravity = Tupple.Vector(0f, -.1f, 0f),
            //    Wind = Tupple.Vector(-.01f, 0f, 0f)
            //};

            //var canvas = new Canvas(900, 550);

            //var numberOfTicks = 0;

            //Console.WriteLine("Shooting bullet!");
            //Console.WriteLine($"Bullets position: X: {projectile.Position.X} Y: {projectile.Position.Y} Z: {projectile.Position.Z}");
            //DrawProjectile(projectile, canvas);

            //while (projectile.Position.Y >= 0f)
            //{
            //    projectile = Tick(environment, projectile);
            //    Console.WriteLine($"Bullets position: X: {projectile.Position.X} Y: {projectile.Position.Y} Z: {projectile.Position.Z}");
            //    numberOfTicks++;
            //    DrawProjectile(projectile, canvas);
            //}

            //Console.WriteLine("Bullet hit the ground!");
            //Console.WriteLine($"It took {numberOfTicks} ticks to hit the ground!");
            //DrawProjectile(projectile, canvas);

            //var imageAsString = canvas.ToPpm();
            //File.WriteAllText(filename, imageAsString);

            //Console.WriteLine($"Image saved as {filename}");
            //Console.ReadKey();

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
            var submatrix = Matrix.Submatrix(matrix, 2, 1);

            Console.WriteLine(submatrix);
            Console.ReadKey();
        }

        private static Projectile Tick(Environment environment, Projectile projectile)
        {
            var position = projectile.Position + projectile.Velocity;
            var velocity = projectile.Velocity + environment.Gravity + environment.Wind;
            return new Projectile
            {
                Position = position,
                Velocity = velocity
            };
        }

        private static void DrawProjectile(Projectile projectile, Canvas canvas)
        {
            var projectileColor = Tupple.Color(.2f, .4f, .6f);
            var y = canvas.Height - projectile.Position.Y;
            var x = projectile.Position.X;

            x = ClampValue(x, canvas.Width - 1);
            y = ClampValue(y, canvas.Height - 1);

            canvas.WritePixel((int)x, (int)y, projectileColor);
        }

        private static float ClampValue(float value, float max, float min = 0f)
        {
            if (value > max) return max;
            if (value < min) return min;
            return value;
        }
    }
}
