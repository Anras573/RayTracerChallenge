using System;
using System.IO;
using System.Text;

namespace RayTracerChallenge.Core
{
    public class PpmCanvasRenderer : ICanvasRenderer
    {
        private const string PpmIdentifier = "P3";
        private const int MaximumPpmLineLength = 70;

        public void Render(Canvas canvas, string path)
        {
            var stringBuilder = new StringBuilder();
            var maxColorsPerLine = canvas.Width;
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
            stringBuilder.AppendLine($"{canvas.Width} {canvas.Height}");
            stringBuilder.AppendLine($"{Color.MaximumColorValue}");

            foreach (var pixel in canvas.Pixels)
            {
                r = (int)MathF.Ceiling(ClampColor(pixel.X) * Color.MaximumColorValue);
                g = (int)MathF.Ceiling(ClampColor(pixel.Y) * Color.MaximumColorValue);
                b = (int)MathF.Ceiling(ClampColor(pixel.Z) * Color.MaximumColorValue);
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
            
            if (!path.EndsWith(".ppm", StringComparison.CurrentCultureIgnoreCase))
            {
                path = $"{path}.ppm";
            }

            File.WriteAllText(path, stringBuilder.ToString());
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
