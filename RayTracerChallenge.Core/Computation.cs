namespace RayTracerChallenge.Core
{
    public class Computation
    {
        public float TimeValue;
        public Sphere Object;
        public Point Point;
        public Vector EyeVector;
        public Vector NormalVector;
        public bool Inside;

        public Computation(Intersection intersection, Ray ray)
        {
            TimeValue = intersection.TimeValue;
            Object = intersection.Object;

            Point = ray.Position(TimeValue);
            EyeVector = -ray.Direction;
            NormalVector = Object.NormalAt(Point);

            Inside = NormalVector.Dot(EyeVector) < 0f;

            if (Inside)
            {
                NormalVector = -NormalVector;
            }
        }
    }
}
