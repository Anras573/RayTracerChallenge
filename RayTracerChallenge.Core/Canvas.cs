using System;
using System.Text;

namespace RayTracerChallenge.Core
{
    public class Canvas
    {
        private const string PpmIdentifier = "P3";
        private const int MaximumPpmLineLength = 70;

        public Tuple[] Pixels { get; }
        public int Width { get; }
        public int Height { get; }

        public Canvas(int width, int height)
        {
            Width = width;
            Height = height;
            Pixels = new Tuple[Width * Height];
            for (var i = 0; i < Pixels.Length; i++)
            {
                Pixels[i] = Tuple.Color(0f, 0f, 0f);
            }
        }

        public void WritePixel(int x, int y, Tuple color)
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

        public string ToPpm()
        {
            var stringBuilder = new StringBuilder();
            var maxColorsPerLine = Width;
            var line = string.Empty;
            var redString = string.Empty;
            var greenString = string.Empty;
            var blueString = string.Empty;
            var colorString = string.Empty;
            var r = 0;
            var g = 0;
            var b = 0;
            var numberOfColors = 0;

            //Create ppm header
            stringBuilder.AppendLine(PpmIdentifier);
            stringBuilder.AppendLine($"{Width} {Height}");
            stringBuilder.AppendLine($"{Tuple.MaximumColorValue}");

            foreach (var pixel in Pixels)
            {
                r = (int)MathF.Ceiling(ClampColor(pixel.X) * Tuple.MaximumColorValue);
                g = (int)MathF.Ceiling(ClampColor(pixel.Y) * Tuple.MaximumColorValue);
                b = (int)MathF.Ceiling(ClampColor(pixel.Z) * Tuple.MaximumColorValue);
                redString = $"{r} ";
                greenString = $"{g} ";
                blueString = $"{b} ";

                if (numberOfColors == maxColorsPerLine)
                {
                    stringBuilder.AppendLine(line);
                    line = string.Empty;
                    numberOfColors = 0;
                }

                if (line.Length + redString.Length > MaximumPpmLineLength)
                {
                    stringBuilder.AppendLine(line);
                    line = $"{redString}{greenString}{blueString}";
                }
                else if (line.Length + redString.Length + greenString.Length > MaximumPpmLineLength)
                {
                    line += redString;
                    stringBuilder.AppendLine(line);
                    line = $"{greenString}{blueString}";
                }
                else if (line.Length + redString.Length + greenString.Length + blueString.Length > MaximumPpmLineLength)
                {
                    line += $"{redString}{greenString}";
                    stringBuilder.AppendLine(line);
                    line = blueString;
                }
                else
                {
                    line += $"{redString}{greenString}{blueString}";
                }

                numberOfColors++;
            }

            stringBuilder.AppendLine(line);
            stringBuilder.AppendLine(); // End on an empty line!
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Makes sure the value is not greater than 1, or less than 0.
        /// </summary>
        /// <param name="color">The R, G, or B value of a color</param>
        /// <returns></returns>
        private float ClampColor(float color)
        {
            if (color > 1f) return 1f;
            if (color < 0f) return 0f;
            return color;
        }
    }
}
