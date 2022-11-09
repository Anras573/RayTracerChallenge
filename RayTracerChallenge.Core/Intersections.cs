using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RayTracerChallenge.Core;

public class Intersections : IEnumerable<Intersection>
{
    private readonly List<Intersection> _sortedIntersections;

    public Intersections(params Intersection[] intersections)
    {
        Array.Sort(intersections, (a, b) => Comparer<float>.Default.Compare(a.TimeValue, b.TimeValue));
        _sortedIntersections = new List<Intersection>(intersections);
    }

    public Intersection Hit()
    {
        if (!_sortedIntersections.Any(i => i.TimeValue > 0.0f))
            return null;

        return _sortedIntersections
            .FirstOrDefault(i => i.TimeValue > 0.0f);
    }

    public int Length => _sortedIntersections.Count;
    public Intersection this[int index] => _sortedIntersections[index];
    public IEnumerator<Intersection> GetEnumerator()
    {
        return _sortedIntersections.AsEnumerable().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}