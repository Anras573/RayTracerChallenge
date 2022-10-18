namespace RayTracerChallenge.Core.Patterns;

public class Blend : Pattern
{
    public Pattern FirstPattern;
    public Pattern SecondPattern;
    
    public Blend(Pattern first, Pattern second) : base(first.First, second.Second)
    {
        FirstPattern = first;
        SecondPattern = second;
    }

    public override Color ColorAt(Point point)
    {
        var color1 = FirstPattern.ColorAt(point);
        var color2 = SecondPattern.ColorAt(point);

        return (color1 + color2) / 2f;
    }
}