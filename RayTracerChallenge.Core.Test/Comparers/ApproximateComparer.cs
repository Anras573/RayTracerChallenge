using System;
using System.Collections.Generic;

namespace RayTracerChallenge.Core.Test.Comparers
{
    public class ApproximateComparer : IEqualityComparer<float>
    {
        private readonly float _marginOfError;

        public ApproximateComparer(float marginOfError)
        {
            _marginOfError = marginOfError;
        }

        public bool Equals(float x, float y)
        {
            if (x != 0f || y != 0f)
            {
                var margin = MathF.Abs((x - y) / x);

                return margin <= _marginOfError;
            }

            return x == y;
        }

        public int GetHashCode(float obj)
        {
            throw new NotImplementedException();
        }
    }
}
