using RayTracerChallenge.Core;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Color = RayTracerChallenge.Core.Color;

namespace RayTracerChallenge.Integration.ImageSharp
{
    public class ImageSharpCanvasRenderer : ICanvasRenderer
    {
        private readonly string _fileType;

        public ImageSharpCanvasRenderer(string fileType)
        {
            _fileType = fileType.StartsWith(".") ? fileType : $".{fileType}";
        }

        public void Render(Canvas canvas, string path)
        {
            var image = new Image<Rgb24>(canvas.Width, canvas.Height);

            for (int x = 0; x < canvas.Width; x++)
            {
                for(int y = 0; y < canvas.Height; y++)
                {
                    var index = (y * canvas.Width) + x;
                    var color = canvas.Pixels[index];

                    var r = (int)MathF.Ceiling(ClampColor(color.R) * Color.MaximumColorValue);
                    var g = (int)MathF.Ceiling(ClampColor(color.G) * Color.MaximumColorValue);
                    var b = (int)MathF.Ceiling(ClampColor(color.B) * Color.MaximumColorValue);

                    image[x, y] = new Rgb24(Convert.ToByte(r), Convert.ToByte(g), Convert.ToByte(b));
                }
            }

            if (!path.EndsWith(_fileType, StringComparison.CurrentCultureIgnoreCase))
            {
                path = $"{path}{_fileType}";
            }

            image.Save(path);
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