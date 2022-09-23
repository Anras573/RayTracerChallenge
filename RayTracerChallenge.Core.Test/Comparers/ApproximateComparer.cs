using System;
using System.Collections.Generic;

namespace RayTracerChallenge.Core.Test.Comparers
{
    public class ApproximateComparer : IEqualityComparer<float>
    {
        public static ApproximateComparer Default => new(0.0001f);

        private readonly float _marginOfError;

        public ApproximateComparer(float marginOfError)
        {
            _marginOfError = marginOfError;
        }

        public bool Equals(float x, float y)
        {

            return MathF.Abs(x - y) < _marginOfError;
        }

        public int GetHashCode(float obj)
        {
            throw new NotImplementedException();
        }
    }
}
