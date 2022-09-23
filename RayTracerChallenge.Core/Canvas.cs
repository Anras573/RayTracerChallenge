using System;

namespace RayTracerChallenge.Core
{
    public class Canvas
    {
        public Color[] Pixels { get; }
        public int Width { get; }
        public int Height { get; }

        public Canvas(int width, int height)
        {
            Width = width;
            Height = height;
            Pixels = new Color[Width * Height];

            for (var i = 0; i < Pixels.Length; i++)
            {
                Pixels[i] = Color.Black;
            }
        }

        public void WritePixel(int x, int y, Color color)
        {
            if (x >= Width)
            {
                throw new ArgumentOutOfRangeException(nameof(x), $"x can't be greater than {Width - 1}");
            }

            if (y >= Height)
            {
                throw new ArgumentOutOfRangeException(nameof(y), $"y can't be greater than {Height - 1}");
            }

            var index = (y * Width) + x;
            Pixels[index] = color;
        }

        public Color GetPixel(int x, int y)
        {
            if (x >= Width)
            {
                throw new ArgumentOutOfRangeException(nameof(x), $"x can't be greater than {Width - 1}");
            }

            if (y >= Height)
            {
                throw new ArgumentOutOfRangeException(nameof(y), $"y can't be greater than {Height - 1}");
            }

            var index = (y * Width) + x;
            return Pixels[index];
        }
    }
}
