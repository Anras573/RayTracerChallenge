using RayTracerChallenge.Core.Shapes;

namespace RayTracerChallenge.Core
{
    public class Intersection
    {
        public float TimeValue;
        public Shape Object;

        public Intersection(float timeValue, Shape obj)
        {
            TimeValue = timeValue;
            Object = obj;
        }
    }
}
