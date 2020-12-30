namespace RayTracerChallenge.Core
{
    public class Ray
    {
        public Point Origin;
        public Vector Direction;

        public Ray(Point origin, Vector direction)
        {
            Origin = origin;
            Direction = direction;
        }

        public Point Position(float time) => Origin + Direction * time;

        public Ray Transform(Matrix translation)
        {
            var origin = Origin * translation;
            var direction = Direction * translation;

            return new Ray(origin, direction);
        }
    }
}
