using System;
using System.Collections;
using System.Collections.Generic;

namespace RayTracerChallenge.Core.Shapes;

public class Group : Shape, IList<Shape>
{
    private readonly List<Shape> _shapes = new ();
    public override Intersections LocalIntersects(Ray localRay)
    {
        var i = new Intersections();

        if (!GetBounds().Intersects(localRay)) return i;
        
        foreach (var shape in _shapes)
        {
            i.AddRange(shape.Intersects(localRay));
        }

        return i;
    }

    public override Vector LocalNormalAt(Point localPoint)
    {
        throw new LocalNormalAtNotImplementedException();
    }

    public override Bounds GetBounds()
    {
        var bounds = Bounds.Empty;

        foreach (var shape in _shapes)
            bounds += shape.GetParentSpaceBounds;

        return bounds;
    }

    #region IList<Shape>
    public IEnumerator<Shape> GetEnumerator() => _shapes.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void Add(Shape item)
    {
        _shapes.Add(item);

        if (item is not null)
        {
            item.Parent = this;
        }
    }

    public void Clear() => _shapes.Clear();

    public bool Contains(Shape item) => _shapes.Contains(item);

    public void CopyTo(Shape[] array, int arrayIndex) => _shapes.CopyTo(array, arrayIndex);

    public bool Remove(Shape item) => _shapes.Remove(item);

    public int Count => _shapes.Count;
    public bool IsReadOnly => true;
    
    public int IndexOf(Shape item) => _shapes.IndexOf(item);

    public void Insert(int index, Shape item) => _shapes.Insert(index, item);

    public void RemoveAt(int index) => _shapes.RemoveAt(index);

    public Shape this[int index]
    {
        get => _shapes[index];
        set => _shapes[index] = value;
    }
    #endregion
}

internal class LocalNormalAtNotImplementedException : Exception
{

}