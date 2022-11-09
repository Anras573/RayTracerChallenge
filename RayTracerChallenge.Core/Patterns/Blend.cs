namespace RayTracerChallenge.Core.Patterns;

public class Blend : Pattern
{
    private readonly Pattern _firstPattern;
    private readonly Pattern _secondPattern;
    
    public Blend(Pattern first, Pattern second) : base(first.First, second.Second)
    {
        _firstPattern = first;
        _secondPattern = second;
    }

    public override Color ColorAt(Point point)
    {
        var color1 = _firstPattern.ColorAt(point);
        var color2 = _secondPattern.ColorAt(point);

        return (color1 + color2) / 2f;
    }
}