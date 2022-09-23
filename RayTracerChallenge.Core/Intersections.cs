using System;
using System.Collections.Generic;
using System.Linq;

namespace RayTracerChallenge.Core
{
    public class Intersections
    {
        private readonly Intersection[] _sortedIntersections;

        public Intersections(params Intersection[] intersections)
        {
            Array.Sort(intersections, (a, b) => Comparer<float>.Default.Compare(a.TimeValue, b.TimeValue));
            _sortedIntersections = intersections;
        }

        public Intersection Hit()
        {
            if (!_sortedIntersections.Any(i => i.TimeValue > 0f))
                return null;

            return _sortedIntersections
                .FirstOrDefault(i => i.TimeValue > 0f);
        }

        public int Length => _sortedIntersections.Length;
        public Intersection this[int index]
        {
            get
            {
                return _sortedIntersections[index];
            }
        }
    }
}
