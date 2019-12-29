using RayTracerChallenge.Core.Test.Comparers;
using System;
using Xunit;

namespace RayTracerChallenge.Core.Test
{
    [Trait("Category", "Tuple")]
    public class TupleTest
    {
        private ApproximateComparer DefaultComparer => new ApproximateComparer(.000001f);

        [Fact]
        [Trait("Category", "Vector")]
        public void GivenTupleWithVectorValue_WhenCallingConstructor_ThenVectorIsCreated()
        {
            // Arrange
            var x = 4f;
            var y = -4f;
            var z = 3f;

            // Act
            var vector = new Tuple(x, y, z, Tuple.VectorIndicator);

            // Assert
            Assert.Equal(x, vector.X);
            Assert.Equal(y, vector.Y);
            Assert.Equal(z, vector.Z);
            Assert.Equal(Tuple.VectorIndicator, vector.W);
        }

        [Fact]
        [Trait("Category", "Point")]
        public void GivenTupleWithPointValue_WhenCallingConstructor_ThenPointIsCreated()
        {
            // Arrange
            var x = 4f;
            var y = -4f;
            var z = 3f;

            // Act
            var point = Tuple.Point(x, y, z);

            // Assert
            Assert.Equal(x, point.X);
            Assert.Equal(y, point.Y);
            Assert.Equal(z, point.Z);
            Assert.Equal(Tuple.PointIndicator, point.W);
        }

        [Fact]
        [Trait("Category", "Point")]
        public void GivenTuple_WhenCallingTuplePoint_ThenPointIsReturned()
        {
            // Arrange
            var x = 4f;
            var y = -4f;
            var z = 3f;
            
            // Act
            var point = Tuple.Point(x, y, z);

            // Assert
            Assert.Equal(x, point.X);
            Assert.Equal(y, point.Y);
            Assert.Equal(z, point.Z);
            Assert.Equal(Tuple.PointIndicator, point.W);
        }

        [Fact]
        [Trait("Category", "Vector")]
        public void GivenTuple_WhenCallingTupleVector_ThenVectorIsReturned()
        {
            // Arrange
            var x = 4f;
            var y = -4f;
            var z = 3f;

            // Act
            var vector = Tuple.Vector(x, y, z);

            // Assert
            Assert.Equal(x, vector.X);
            Assert.Equal(y, vector.Y);
            Assert.Equal(z, vector.Z);
            Assert.Equal(Tuple.VectorIndicator, vector.W);
        }

        [Fact]
        [Trait("Category", "Equal")]
        [Trait("Category", "Point")]
        public void GivenTwoIdenticalPoints_WhenCallingEqual_ThenTrueIsReturned()
        {
            // Arrange
            var x = 4f;
            var y = -4f;
            var z = 3f;
            var firstPoint = Tuple.Point(x, y, z);
            var secondPoint = Tuple.Point(x, y, z);

            // Act
            var isEqual = firstPoint.Equals(secondPoint);

            // Assert
            Assert.True(isEqual);
        }

        [Fact]
        [Trait("Category", "Equal")]
        [Trait("Category", "Vector")]
        public void GivenTwoIdenticalVectors_WhenCallingEqual_ThenTrueIsReturned()
        {
            // Arrange
            var x = 4f;
            var y = -4f;
            var z = 3f;
            var firstVector = Tuple.Vector(x, y, z);
            var secondVector = Tuple.Vector(x, y, z);

            // Act
            var isEqual = firstVector.Equals(secondVector);

            // Assert
            Assert.True(isEqual);
        }

        [Fact]
        [Trait("Category", "Equal")]
        [Trait("Category", "Point")]
        public void GivenTwoNonIdenticalPoints_WhenCallingEqual_ThenFalseIsReturned()
        {
            // Arrange
            var firstPoint = Tuple.Point(4f, -4f, 3f);
            var secondPoint = Tuple.Point(-4f, 4f, -3f);

            // Act
            var isEqual = firstPoint.Equals(secondPoint);

            // Assert
            Assert.False(isEqual);
        }

        [Fact]
        [Trait("Category", "Equal")]
        [Trait("Category", "Vector")]
        public void GivenTwoNonIdenticalVectors_WhenCallingEqual_ThenFalseIsReturned()
        {
            // Arrange
            var firstVector = Tuple.Vector(4f, -4f, 3f);
            var secondVector = Tuple.Vector(-4f, 4f, -3f);

            // Act
            var isEqual = firstVector.Equals(secondVector);

            // Assert
            Assert.False(isEqual);
        }

        [Fact]
        [Trait("Category", "Equal")]
        [Trait("Category", "Point")]
        [Trait("Category", "Vector")]
        public void GivenIdenticalPointAndVector_WhenCallingEqual_ThenFalseIsReturned()
        {
            // Arrange
            var x = 4f;
            var y = -4f;
            var z = 3f;
            var vector = Tuple.Vector(x, y, z);
            var point = Tuple.Point(x, y, z);

            // Act
            var isEqual = vector.Equals(point);

            // Assert
            Assert.False(isEqual);
        }

        [Fact]
        [Trait("Category", "Equal")]
        [Trait("Category", "Point")]
        [Trait("Category", "Vector")]
        public void GivenNonIdenticalPointAndVector_WhenCallingEqual_ThenFalseIsReturned()
        {
            // Arrange
            var vector = Tuple.Vector(4f, -4f, 3f);
            var point = Tuple.Point(-4f, 4f, -3f);

            // Act
            var isEqual = vector.Equals(point);

            // Assert
            Assert.False(isEqual);
        }

        [Fact]
        [Trait("Category", "Addition")]
        [Trait("Category", "Point")]
        [Trait("Category", "Vector")]
        public void GivenAVectorAndAPoint_WhenAddingThemTogether_ThenAPointIsReturned()
        {
            // Arrange
            var vector = Tuple.Vector(3f, -2f, 5f);
            var point = Tuple.Point(-2f, 3f, 1f);

            // Act
            var tuple = vector + point;

            // Assert
            Assert.Equal(1f, tuple.X);
            Assert.Equal(1f, tuple.Y);
            Assert.Equal(6f, tuple.Z);
            Assert.Equal(Tuple.PointIndicator, tuple.W);
        }

        [Fact]
        [Trait("Category", "Addition")]
        [Trait("Category", "Vector")]
        public void GivenTwoVectors_WhenAddingThemTogether_ThenAVectorIsReturned()
        {
            // Arrange
            var firstVector = Tuple.Vector(3f, -2f, 5f);
            var secondVector = Tuple.Vector(-2f, 3f, 1f);

            // Act
            var newVector = firstVector + secondVector;

            // Assert
            Assert.Equal(1f, newVector.X);
            Assert.Equal(1f, newVector.Y);
            Assert.Equal(6f, newVector.Z);
            Assert.Equal(Tuple.VectorIndicator, newVector.W);
        }

        [Fact]
        [Trait("Category", "Addition")]
        [Trait("Category", "Point")]
        [Trait("Category", "Exception")]
        public void GivenTwoPoints_WhenAddingThemTogether_ThenAnArithmeticExceptionIsThrown()
        {
            // Arrange
            var firstPoint = Tuple.Point(3f, -2f, 5f);
            var secondPoint = Tuple.Point(-2f, 3f, 1f);

            // Act
            ArithmeticException ex = Assert.Throws<ArithmeticException>(() => firstPoint + secondPoint);

            // Assert
            Assert.Equal("You can't add two Points together!", ex.Message);
        }

        [Fact]
        [Trait("Category", "Substraction")]
        [Trait("Category", "Point")]
        public void GivenTwoPoints_WhenSubstracingThem_ThenAVectorIsReturned()
        {
            // Arrange
            var firstPoint = Tuple.Point(3f, 2f, 1f);
            var secondPoint = Tuple.Point(5f, 6f, 7f);

            // Act
            var tuple = firstPoint - secondPoint;

            // Assert
            Assert.Equal(-2f, tuple.X);
            Assert.Equal(-4f, tuple.Y);
            Assert.Equal(-6f, tuple.Z);
            Assert.Equal(Tuple.VectorIndicator, tuple.W);
        }

        [Fact]
        [Trait("Category", "Substraction")]
        [Trait("Category", "Vector")]
        public void GivenTwoVectors_WhenSubstracingThem_ThenAVectorIsReturned()
        {
            // Arrange
            var firstVector = Tuple.Vector(3f, 2f, 1f);
            var secondVector = Tuple.Vector(5f, 6f, 7f);

            // Act
            var tuple = firstVector - secondVector;

            // Assert
            Assert.Equal(-2f, tuple.X);
            Assert.Equal(-4f, tuple.Y);
            Assert.Equal(-6f, tuple.Z);
            Assert.Equal(Tuple.VectorIndicator, tuple.W);
        }

        [Fact]
        [Trait("Category", "Substraction")]
        [Trait("Category", "Point")]
        [Trait("Category", "Vector")]
        public void GivenAVectorAndAPoint_WhenSubstracingVectorFromPoint_ThenAPointIsReturned()
        {
            // Arrange
            var point = Tuple.Point(3f, 2f, 1f);
            var vector = Tuple.Vector(5f, 6f, 7f);

            // Act
            var tuple = point - vector;

            // Assert
            Assert.Equal(-2f, tuple.X);
            Assert.Equal(-4f, tuple.Y);
            Assert.Equal(-6f, tuple.Z);
            Assert.Equal(Tuple.PointIndicator, tuple.W);
        }

        [Fact]
        [Trait("Category", "Substraction")]
        [Trait("Category", "Point")]
        [Trait("Category", "Vector")]
        [Trait("Category", "Exception")]
        public void GivenAVectorAndAPoint_WhenSubstracingPointFromVector_ThenAnArithmeticExceptionIsThrown()
        {
            // Arrange
            var vector = Tuple.Vector(3f, 2f, 1f);
            var point = Tuple.Point(5f, 6f, 7f);

            // Act
            void func() { var result = vector - point; }

            // Assert
            Assert.Throws<ArithmeticException>(func);
        }

        [Fact]
        [Trait("Category", "Negation")]
        public void GivenATuple_WhenNegating_ThenANewTupleIsReturned()
        {
            // Arrange
            var tuple = new Tuple(1f, -2f, 3f, -4f);

            // Act
            var negatedTuple = !tuple;

            // Assert
            Assert.Equal(-1f, negatedTuple.X);
            Assert.Equal(2f, negatedTuple.Y);
            Assert.Equal(-3f, negatedTuple.Z);
            Assert.Equal(4f, negatedTuple.W);
        }

        [Fact]
        [Trait("Category", "Multiplication")]
        public void GivenATuple_WhenMultiplyingWithAScalar_ThenABiggerTupleIsReturned()
        {
            // Arrange
            var tuple = new Tuple(1f, -2f, 3f, -4f);
            var scalar = 3.5f;

            // Act
            var scalarTuple = tuple * scalar;

            // Assert
            Assert.Equal(3.5f, scalarTuple.X);
            Assert.Equal(-7f, scalarTuple.Y);
            Assert.Equal(10.5f, scalarTuple.Z);
            Assert.Equal(-14f, scalarTuple.W);
        }

        [Fact]
        [Trait("Category", "Multiplication")]
        public void GivenATuple_WhenMultiplyingWithAFraction_ThenASmallerTupleIsReturned()
        {
            // Arrange
            var tuple = new Tuple(1f, -2f, 3f, -4f);
            var fraction = .5f;

            // Act
            var fractionTuple = tuple * fraction;

            // Assert
            Assert.Equal(.5f, fractionTuple.X);
            Assert.Equal(-1f, fractionTuple.Y);
            Assert.Equal(1.5f, fractionTuple.Z);
            Assert.Equal(-2f, fractionTuple.W);
        }

        [Fact]
        [Trait("Category", "Division")]
        public void GivenATuple_WhenDividing_ThenANewTupleIsReturned()
        {
            // Arrange
            var tuple = new Tuple(1f, -2f, 3f, -4f);
            var division = 2f;

            // Act
            var fractionTuple = tuple / division;

            // Assert
            Assert.Equal(.5f, fractionTuple.X);
            Assert.Equal(-1f, fractionTuple.Y);
            Assert.Equal(1.5f, fractionTuple.Z);
            Assert.Equal(-2f, fractionTuple.W);
        }

        [Fact]
        [Trait("Category", "Magnitude")]
        [Trait("Category", "Point")]
        [Trait("Category", "Exception")]
        public void GivenAPoint_WhenGettingMagnitude_ThenAnArithmeticExceptionIsThrown()
        {
            // Arrange
            var point = Tuple.Point(1f, -2f, 3f);

            // Act
            void func() { var result = point.Magnitude(); }

            // Assert
            Assert.Throws<ArithmeticException>(func);
        }

        [Fact]
        [Trait("Category", "Magnitude")]
        [Trait("Category", "Vector")]
        public void GivenAVectorWithAValueOf1InXOrYOrZ_WhenGettingMagnitude_Then1IsReturned()
        {
            // Arrange
            var xVector = Tuple.Vector(1f, 0f, 0f);
            var yVector = Tuple.Vector(0f, 1f, 0f);
            var zVector = Tuple.Vector(0f, 0f, 1f);

            // Act
            var xMagnitude = xVector.Magnitude();
            var yMagnitude = yVector.Magnitude();
            var zMagnitude = zVector.Magnitude();

            // Assert
            Assert.Equal(1f, xMagnitude);
            Assert.Equal(1f, yMagnitude);
            Assert.Equal(1f, zMagnitude);
        }

        [Fact]
        [Trait("Category", "Magnitude")]
        [Trait("Category", "Vector")]
        public void GivenAPositiveVector_WhenGettingMagnitude_ThenAPositiveMagnitudeIsReturned()
        {
            // Arrange
            var vector = Tuple.Vector(1f, 2f, 3f);

            // Act
            var magnitude = vector.Magnitude();

            // Assert
            Assert.Equal(MathF.Sqrt(14f), magnitude);
        }

        [Fact]
        [Trait("Category", "Magnitude")]
        [Trait("Category", "Vector")]
        public void GivenANegativeVector_WhenGettingMagnitude_ThenAPositiveMagnitudeIsReturned()
        {
            // Arrange
            var vector = Tuple.Vector(-1f, -2f, -3f);

            // Act
            var magnitude = vector.Magnitude();

            // Assert
            Assert.Equal(MathF.Sqrt(14f), magnitude);
        }

        [Fact]
        [Trait("Category", "Normalize")]
        [Trait("Category", "Vector")]
        public void GivenAVector_WhenNormalizing_ThenANewVectorIsReturned()
        {
            // Arrange
            var vector = Tuple.Vector(4f, 0f, 0f);

            // Act
            var normalizedVector = vector.Normalize();

            // Assert
            Assert.Equal(1f, normalizedVector.X);
            Assert.Equal(0f, normalizedVector.Y);
            Assert.Equal(0f, normalizedVector.Z);
        }

        [Fact]
        [Trait("Category", "Normalize")]
        [Trait("Category", "Vector")]
        public void GivenAnotherVector_WhenNormalizing_ThenANewVectorIsReturned()
        {
            // Arrange
            var vector = Tuple.Vector(1f, 2f, 3f);

            // Act
            var normalizedVector = vector.Normalize();

            // Assert
            var magnitude = vector.Magnitude();
            var normalizedX = vector.X / magnitude;
            var normalizedY = vector.Y / magnitude;
            var normalizedZ = vector.Z / magnitude;
            Assert.Equal(normalizedX, normalizedVector.X);
            Assert.Equal(normalizedY, normalizedVector.Y);
            Assert.Equal(normalizedZ, normalizedVector.Z);
        }

        [Fact]
        [Trait("Category", "Normalize")]
        [Trait("Category", "Magnitude")]
        [Trait("Category", "Vector")]
        public void GivenANormalizedVector_WhenGettingMagnitude_Then1IsReturned()
        {
            // Arrange
            var vector = Tuple.Vector(1f, 2f, 3f);

            // Act
            var normalizedVector = vector.Normalize();
            var magnitude = normalizedVector.Magnitude();

            // Assert
            Assert.Equal(1f, magnitude, DefaultComparer);
        }

        [Fact]
        [Trait("Category", "Dot Product")]
        [Trait("Category", "Vector")]
        public void GivenTwoVectors_WhenGettingDotProduct_ThenDotProductIsReturned()
        {
            // Arrange
            var firstVector = Tuple.Vector(1f, 2f, 3f);
            var secondVector = Tuple.Vector(2f, 3f, 4f);

            // Act
            var dotProduct = Tuple.Dot(firstVector, secondVector);

            // Assert
            Assert.Equal(20f, dotProduct);
        }

        [Fact]
        [Trait("Category", "Dot Product")]
        [Trait("Category", "Point")]
        [Trait("Category", "Vector")]
        [Trait("Category", "Exception")]
        public void GivenAVectorAndAPoint_WhenGettingDotProduct_ThenAnArithmeticExceptionIsThrown()
        {
            // Arrange
            var vector = Tuple.Vector(1f, 2f, 3f);
            var point = Tuple.Point(1f, 2f, 3f);

            // Act
            void func() { var result = Tuple.Dot(vector, point); }
            void func2() { var result = Tuple.Dot(point, vector); }

            // Assert
            Assert.Throws<ArithmeticException>(func);
            Assert.Throws<ArithmeticException>(func2);
        }

        [Fact]
        [Trait("Category", "Cross Product")]
        [Trait("Category", "Vector")]
        public void GivenTwoVectors_WhenGettingCrossProduct_ThenANewVectorIsReturned()
        {
            // Arrange
            var firstVector = Tuple.Vector(1f, 2f, 3f);
            var secondVector = Tuple.Vector(2f, 3f, 4f);

            // Act
            var crossVector = Tuple.Cross(firstVector, secondVector);
            var reversedCrossVector = Tuple.Cross(secondVector, firstVector);

            // Assert
            Assert.Equal(-1f, crossVector.X);
            Assert.Equal(2f, crossVector.Y);
            Assert.Equal(-1f, crossVector.Z);
            Assert.Equal(1f, reversedCrossVector.X);
            Assert.Equal(-2f, reversedCrossVector.Y);
            Assert.Equal(1f, reversedCrossVector.Z);
        }

        [Fact]
        [Trait("Category", "Cross Product")]
        [Trait("Category", "Point")]
        [Trait("Category", "Vector")]
        [Trait("Category", "Exception")]
        public void GivenAVectorAndAPoint_WhenGettingCrossProduct_ThenAnArithmeticExceptionIsThrown()
        {
            // Arrange
            var vector = Tuple.Vector(1f, 2f, 3f);
            var point = Tuple.Point(2f, 3f, 4f);

            // Act
            void func() { var result = Tuple.Cross(vector, point); }
            void func2() { var result = Tuple.Cross(point, vector); }

            // Assert
            Assert.Throws<ArithmeticException>(func);
            Assert.Throws<ArithmeticException>(func2);
        }

        [Fact]
        [Trait("Category", "Color")]
        [Trait("Category", "Vector")]
        public void GivenTuple_WhenCallingTupleColor_ThenVectorIsReturned()
        {
            // Arrange
            var x = -.5f;
            var y = .4f;
            var z = 1.7f;

            // Act
            var color = Tuple.Color(x, y, z);

            // Assert
            Assert.Equal(x, color.X);
            Assert.Equal(y, color.Y);
            Assert.Equal(z, color.Z);
            Assert.Equal(Tuple.VectorIndicator, color.W);
        }

        [Fact]
        [Trait("Category", "Color")]
        [Trait("Category", "Addition")]
        public void GivenTwoColors_WhenAddingThemTogether_ThenANewColorIsReturned()
        {
            // Arrange
            var firstColor = Tuple.Color(.9f, .6f, .75f);
            var secondColor = Tuple.Color(.7f, .1f, .25f);

            // Act
            var color = firstColor + secondColor;

            // Assert
            Assert.Equal(1.6f, color.X, DefaultComparer);
            Assert.Equal(.7f, color.Y, DefaultComparer);
            Assert.Equal(1f, color.Z, DefaultComparer);
            Assert.Equal(Tuple.VectorIndicator, color.W);
        }

        [Fact]
        [Trait("Category", "Color")]
        [Trait("Category", "Substraction")]
        public void GivenTwoColors_WhenSubstractingThem_ThenANewColorIsReturned()
        {
            // Arrange
            var firstColor = Tuple.Color(.9f, .6f, .75f);
            var secondColor = Tuple.Color(.7f, .1f, .25f);

            // Act
            var color = firstColor - secondColor;

            // Assert
            Assert.Equal(.2f, color.X, DefaultComparer);
            Assert.Equal(.5f, color.Y, DefaultComparer);
            Assert.Equal(.5f, color.Z, DefaultComparer);
            Assert.Equal(Tuple.VectorIndicator, color.W);
        }

        [Fact]
        [Trait("Category", "Color")]
        [Trait("Category", "Multiplication")]
        public void GivenAColor_WhenMultiplyingWithAScalar_ThenANewColorIsReturned()
        {
            // Arrange
            var color = Tuple.Color(.2f, .3f, .4f);
            var scalar = 2f;

            // Act
            var newColor = color * scalar;

            // Assert
            Assert.Equal(.4f, newColor.X);
            Assert.Equal(.6f, newColor.Y);
            Assert.Equal(.8f, newColor.Z);
            Assert.Equal(Tuple.VectorIndicator, newColor.W);
        }

        [Fact]
        [Trait("Category", "Color")]
        [Trait("Category", "Multiplication")]
        public void GivenTwoColors_WhenMultiplyingThem_ThenANewColorIsReturned()
        {
            // Arrange
            var firstColor = Tuple.Color(1f, .2f, .4f);
            var secondColor = Tuple.Color(.9f, 1f, .1f);

            // Act
            var newColor = Tuple.HadamardProduct(firstColor, secondColor);

            // Assert
            Assert.Equal(.9f, newColor.X);
            Assert.Equal(.2f, newColor.Y);
            Assert.Equal(.04f, newColor.Z, DefaultComparer);
            Assert.Equal(Tuple.VectorIndicator, newColor.W);
        }
    }
}
