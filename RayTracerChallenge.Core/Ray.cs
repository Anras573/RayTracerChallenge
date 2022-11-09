namespace RayTracerChallenge.Core;

public record Ray(Point Origin, Vector Direction)
{
    public Point Position(float time) => Origin + Direction * time;

    public Ray Transform(Matrix translation)
    {
        var origin = Origin * translation;
        var direction = Direction * translation;

        return new Ray(origin, direction);
    }
}