using RayTracerChallenge.Core;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Color = RayTracerChallenge.Core.Color;

namespace RayTracerChallenge.Integration.ImageSharp;

public class ImageSharpGifRenderer
{
    private readonly Image<Rgb24> _innerImage;
    private readonly int _frameDelay;

    public ImageSharpGifRenderer(int width, int height, int frameDelay = 1000/60)
    {
        _innerImage = new Image<Rgb24>(width, height);
        _frameDelay = frameDelay;
    }

    public void RenderFrame(Canvas canvas)
    {
        using var image = new Image<Rgb24>(canvas.Width, canvas.Height);

        for (var x = 0; x < canvas.Width; x++)
        {
            for (var y = 0; y < canvas.Height; y++)
            {
                var index = (y * canvas.Width) + x;
                var color = canvas.Pixels[index];

                var r = (int)MathF.Ceiling(Utilities.ClampColor(color.R) * Color.MaximumColorValue);
                var g = (int)MathF.Ceiling(Utilities.ClampColor(color.G) * Color.MaximumColorValue);
                var b = (int)MathF.Ceiling(Utilities.ClampColor(color.B) * Color.MaximumColorValue);

                image[x, y] = new Rgb24(Convert.ToByte(r), Convert.ToByte(g), Convert.ToByte(b));
            }
        }


        // Set the delay until the next image is displayed.
        var metadata = image.Frames.RootFrame.Metadata.GetGifMetadata();
        metadata.FrameDelay = _frameDelay;

        // Add the color image to the gif.
        _innerImage.Frames.AddFrame(image.Frames.RootFrame);
    }

    public void Render(string path)
    {
        if (!path.EndsWith(".gif", StringComparison.CurrentCultureIgnoreCase))
        {
            path = $"{path}.gif";
        }

        _innerImage.SaveAsGif(path);
    }
}