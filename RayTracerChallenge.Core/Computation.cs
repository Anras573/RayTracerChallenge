﻿using System;
using System.Collections.Generic;
using System.Linq;
using RayTracerChallenge.Core.Shapes;

namespace RayTracerChallenge.Core;

public class Computation
{
    public readonly float TimeValue;
    public readonly Shape Object;
    public readonly Point Point;
    public readonly Point OverPoint;
    public readonly Point UnderPoint;
    public readonly Vector EyeVector;
    public readonly Vector NormalVector;
    public readonly Vector ReflectVector;
    public readonly bool Inside;
    public readonly float N1;
    public readonly float N2;

    public Computation(Intersection intersection, Ray ray, Intersections intersections = null)
    {
        TimeValue = intersection.TimeValue;
        Object = intersection.Object;

        Point = ray.Position(TimeValue);
        EyeVector = -ray.Direction;
        NormalVector = Object.NormalAt(Point);

        Inside = NormalVector.Dot(EyeVector) < 0.0f;

        if (Inside)
        {
            NormalVector = -NormalVector;
        }

        ReflectVector = ray.Direction.Reflect(NormalVector);

        OverPoint = Point + NormalVector * Utilities.Epsilon;
        UnderPoint = Point - NormalVector * Utilities.Epsilon;

        intersections ??= new Intersections(intersection);
        
        var containers = new List<Shape>();

        foreach (var i in intersections)
        {
            if (i == intersection)
            {
                N1 = containers.Any() ? containers.Last().Material.RefractiveIndex : 1.0f;
            }

            if (containers.Contains(i.Object))
            {
                containers.Remove(i.Object);
            }
            else
            {
                containers.Add(i.Object);
            }

            if (i != intersection)
                continue;
                
            N2 = containers.Any() ? containers.Last().Material.RefractiveIndex : 1.0f;
            break;
        }
    }

    public float Schlick()
    {
        var cos = EyeVector.Dot(NormalVector);

        if (N1 > N2)
        {
            var n = N1 / N2;
            var sin2T = MathF.Pow(n, 2.0f) * (1.0f - MathF.Pow(cos, 2.0f));

            if (sin2T > 1.0f)
            {
                return 1.0f;
            }

            var cosT = MathF.Sqrt(1.0f - sin2T);
            cos = cosT;
        }

        var r0 = MathF.Pow((N1 - N2) / (N1 + N2), 2.0f);
            
        return r0 + (1.0f - r0) * MathF.Pow(1.0f - cos, 5.0f);
    }
}