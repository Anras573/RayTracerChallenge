using RayTracerChallenge.Core;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Color = RayTracerChallenge.Core.Color;

namespace RayTracerChallenge.Integration.ImageSharp;

public class ImageSharpCanvasRenderer : ICanvasRenderer
{
    private readonly string _fileType;

    public ImageSharpCanvasRenderer(string fileType)
    {
        _fileType = fileType.StartsWith(".") ? fileType : $".{fileType}";
    }

    public void Render(Canvas canvas, string path)
    {
        using var image = new Image<Rgb24>(canvas.Width, canvas.Height);

        for (var x = 0; x < canvas.Width; x++)
        {
            for(var y = 0; y < canvas.Height; y++)
            {
                var index = (y * canvas.Width) + x;
                var color = canvas.Pixels[index];

                var r = (int)MathF.Ceiling(Utilities.ClampColor(color.R) * Color.MaximumColorValue);
                var g = (int)MathF.Ceiling(Utilities.ClampColor(color.G) * Color.MaximumColorValue);
                var b = (int)MathF.Ceiling(Utilities.ClampColor(color.B) * Color.MaximumColorValue);

                image[x, y] = new Rgb24(Convert.ToByte(r), Convert.ToByte(g), Convert.ToByte(b));
            }
        }

        if (!path.EndsWith(_fileType, StringComparison.CurrentCultureIgnoreCase))
        {
            path = $"{path}{_fileType}";
        }

        image.Save(path);
    }
}