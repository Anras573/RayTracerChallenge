namespace RayTracerChallenge.Core
{
    public class Intersection
    {
        public float TimeValue;
        public Sphere Object;

        public Intersection(float timeValue, Sphere obj)
        {
            TimeValue = timeValue;
            Object = obj;
        }
    }
}
