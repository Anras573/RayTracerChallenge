using RayTracerChallenge.Core.Test.Comparers;
using System;
using Xunit;

namespace RayTracerChallenge.Core.Test
{
    [Trait("Category", "Tupple")]
    public class TuppleTest
    {
        private ApproximateComparer DefaultComparer => new ApproximateComparer(.000001f);

        [Fact]
        [Trait("Category", "Vector")]
        public void GivenTuppleWithVectorValue_WhenCallingConstructor_ThenVectorIsCreated()
        {
            // Arrange
            var x = 4f;
            var y = -4f;
            var z = 3f;

            // Act
            var vector = new Tupple(x, y, z, Tupple.VectorIndicator);

            // Assert
            Assert.Equal(x, vector.X);
            Assert.Equal(y, vector.Y);
            Assert.Equal(z, vector.Z);
            Assert.Equal(Tupple.VectorIndicator, vector.W);
        }

        [Fact]
        [Trait("Category", "Point")]
        public void GivenTuppleWithPointValue_WhenCallingConstructor_ThenPointIsCreated()
        {
            // Arrange
            var x = 4f;
            var y = -4f;
            var z = 3f;

            // Act
            var point = Tupple.Point(x, y, z);

            // Assert
            Assert.Equal(x, point.X);
            Assert.Equal(y, point.Y);
            Assert.Equal(z, point.Z);
            Assert.Equal(Tupple.PointIndicator, point.W);
        }

        [Fact]
        [Trait("Category", "Point")]
        public void GivenTupple_WhenCallingTupplePoint_ThenPointIsReturned()
        {
            // Arrange
            var x = 4f;
            var y = -4f;
            var z = 3f;
            
            // Act
            var point = Tupple.Point(x, y, z);

            // Assert
            Assert.Equal(x, point.X);
            Assert.Equal(y, point.Y);
            Assert.Equal(z, point.Z);
            Assert.Equal(Tupple.PointIndicator, point.W);
        }

        [Fact]
        [Trait("Category", "Vector")]
        public void GivenTupple_WhenCallingTuppleVector_ThenVectorIsReturned()
        {
            // Arrange
            var x = 4f;
            var y = -4f;
            var z = 3f;

            // Act
            var vector = Tupple.Vector(x, y, z);

            // Assert
            Assert.Equal(x, vector.X);
            Assert.Equal(y, vector.Y);
            Assert.Equal(z, vector.Z);
            Assert.Equal(Tupple.VectorIndicator, vector.W);
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
            var firstPoint = Tupple.Point(x, y, z);
            var secondPoint = Tupple.Point(x, y, z);

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
            var firstVector = Tupple.Vector(x, y, z);
            var secondVector = Tupple.Vector(x, y, z);

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
            var firstPoint = Tupple.Point(4f, -4f, 3f);
            var secondPoint = Tupple.Point(-4f, 4f, -3f);

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
            var firstVector = Tupple.Vector(4f, -4f, 3f);
            var secondVector = Tupple.Vector(-4f, 4f, -3f);

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
            var vector = Tupple.Vector(x, y, z);
            var point = Tupple.Point(x, y, z);

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
            var vector = Tupple.Vector(4f, -4f, 3f);
            var point = Tupple.Point(-4f, 4f, -3f);

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
            var vector = Tupple.Vector(3f, -2f, 5f);
            var point = Tupple.Point(-2f, 3f, 1f);

            // Act
            var tupple = vector + point;

            // Assert
            Assert.Equal(1f, tupple.X);
            Assert.Equal(1f, tupple.Y);
            Assert.Equal(6f, tupple.Z);
            Assert.Equal(Tupple.PointIndicator, tupple.W);
        }

        [Fact]
        [Trait("Category", "Addition")]
        [Trait("Category", "Vector")]
        public void GivenTwoVectors_WhenAddingThemTogether_ThenAVectorIsReturned()
        {
            // Arrange
            var firstVector = Tupple.Vector(3f, -2f, 5f);
            var secondVector = Tupple.Vector(-2f, 3f, 1f);

            // Act
            var newVector = firstVector + secondVector;

            // Assert
            Assert.Equal(1f, newVector.X);
            Assert.Equal(1f, newVector.Y);
            Assert.Equal(6f, newVector.Z);
            Assert.Equal(Tupple.VectorIndicator, newVector.W);
        }

        [Fact]
        [Trait("Category", "Addition")]
        [Trait("Category", "Point")]
        [Trait("Category", "Exception")]
        public void GivenTwoPoints_WhenAddingThemTogether_ThenAnArithmeticExceptionIsThrown()
        {
            // Arrange
            var firstPoint = Tupple.Point(3f, -2f, 5f);
            var secondPoint = Tupple.Point(-2f, 3f, 1f);

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
            var firstPoint = Tupple.Point(3f, 2f, 1f);
            var secondPoint = Tupple.Point(5f, 6f, 7f);

            // Act
            var tupple = firstPoint - secondPoint;

            // Assert
            Assert.Equal(-2f, tupple.X);
            Assert.Equal(-4f, tupple.Y);
            Assert.Equal(-6f, tupple.Z);
            Assert.Equal(Tupple.VectorIndicator, tupple.W);
        }

        [Fact]
        [Trait("Category", "Substraction")]
        [Trait("Category", "Vector")]
        public void GivenTwoVectors_WhenSubstracingThem_ThenAVectorIsReturned()
        {
            // Arrange
            var firstVector = Tupple.Vector(3f, 2f, 1f);
            var secondVector = Tupple.Vector(5f, 6f, 7f);

            // Act
            var tupple = firstVector - secondVector;

            // Assert
            Assert.Equal(-2f, tupple.X);
            Assert.Equal(-4f, tupple.Y);
            Assert.Equal(-6f, tupple.Z);
            Assert.Equal(Tupple.VectorIndicator, tupple.W);
        }

        [Fact]
        [Trait("Category", "Substraction")]
        [Trait("Category", "Point")]
        [Trait("Category", "Vector")]
        public void GivenAVectorAndAPoint_WhenSubstracingVectorFromPoint_ThenAPointIsReturned()
        {
            // Arrange
            var point = Tupple.Point(3f, 2f, 1f);
            var vector = Tupple.Vector(5f, 6f, 7f);

            // Act
            var tupple = point - vector;

            // Assert
            Assert.Equal(-2f, tupple.X);
            Assert.Equal(-4f, tupple.Y);
            Assert.Equal(-6f, tupple.Z);
            Assert.Equal(Tupple.PointIndicator, tupple.W);
        }

        [Fact]
        [Trait("Category", "Substraction")]
        [Trait("Category", "Point")]
        [Trait("Category", "Vector")]
        [Trait("Category", "Exception")]
        public void GivenAVectorAndAPoint_WhenSubstracingPointFromVector_ThenAnArithmeticExceptionIsThrown()
        {
            // Arrange
            var vector = Tupple.Vector(3f, 2f, 1f);
            var point = Tupple.Point(5f, 6f, 7f);

            // Act
            ArithmeticException ex = Assert.Throws<ArithmeticException>(() => vector - point);

            // Assert
            Assert.Equal("You can't substract a Vector from a Point!", ex.Message);
        }

        [Fact]
        [Trait("Category", "Negation")]
        public void GivenATupple_WhenNegating_ThenANewTuppleIsReturned()
        {
            // Arrange
            var tupple = new Tupple(1f, -2f, 3f, -4f);

            // Act
            var negatedTupple = !tupple;

            // Assert
            Assert.Equal(-1f, negatedTupple.X);
            Assert.Equal(2f, negatedTupple.Y);
            Assert.Equal(-3f, negatedTupple.Z);
            Assert.Equal(4f, negatedTupple.W);
        }

        [Fact]
        [Trait("Category", "Multiplication")]
        public void GivenATupple_WhenMultiplyingWithAScalar_ThenABiggerTuppleIsReturned()
        {
            // Arrange
            var tupple = new Tupple(1f, -2f, 3f, -4f);
            var scalar = 3.5f;

            // Act
            var scalarTupple = tupple * scalar;

            // Assert
            Assert.Equal(3.5f, scalarTupple.X);
            Assert.Equal(-7f, scalarTupple.Y);
            Assert.Equal(10.5f, scalarTupple.Z);
            Assert.Equal(-14f, scalarTupple.W);
        }

        [Fact]
        [Trait("Category", "Multiplication")]
        public void GivenATupple_WhenMultiplyingWithAFraction_ThenASmallerTuppleIsReturned()
        {
            // Arrange
            var tupple = new Tupple(1f, -2f, 3f, -4f);
            var fraction = .5f;

            // Act
            var fractionTupple = tupple * fraction;

            // Assert
            Assert.Equal(.5f, fractionTupple.X);
            Assert.Equal(-1f, fractionTupple.Y);
            Assert.Equal(1.5f, fractionTupple.Z);
            Assert.Equal(-2f, fractionTupple.W);
        }

        [Fact]
        [Trait("Category", "Division")]
        public void GivenATupple_WhenDividing_ThenANewTuppleIsReturned()
        {
            // Arrange
            var tupple = new Tupple(1f, -2f, 3f, -4f);
            var division = 2f;

            // Act
            var fractionTupple = tupple / division;

            // Assert
            Assert.Equal(.5f, fractionTupple.X);
            Assert.Equal(-1f, fractionTupple.Y);
            Assert.Equal(1.5f, fractionTupple.Z);
            Assert.Equal(-2f, fractionTupple.W);
        }

        [Fact]
        [Trait("Category", "Magnitude")]
        [Trait("Category", "Point")]
        [Trait("Category", "Exception")]
        public void GivenAPoint_WhenGettingMagnitude_ThenAnArithmeticExceptionIsThrown()
        {
            // Arrange
            var point = Tupple.Point(1f, -2f, 3f);

            // Act
            ArithmeticException ex = Assert.Throws<ArithmeticException>(() => point.Magnitude());

            // Assert
            Assert.Equal("This operation can only be used on a Vector!", ex.Message);
        }

        [Fact]
        [Trait("Category", "Magnitude")]
        [Trait("Category", "Vector")]
        public void GivenAVectorWithAValueOf1InXOrYOrZ_WhenGettingMagnitude_Then1IsReturned()
        {
            // Arrange
            var xVector = Tupple.Vector(1f, 0f, 0f);
            var yVector = Tupple.Vector(0f, 1f, 0f);
            var zVector = Tupple.Vector(0f, 0f, 1f);

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
            var vector = Tupple.Vector(1f, 2f, 3f);

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
            var vector = Tupple.Vector(-1f, -2f, -3f);

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
            var vector = Tupple.Vector(4f, 0f, 0f);

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
            var vector = Tupple.Vector(1f, 2f, 3f);

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
            var vector = Tupple.Vector(1f, 2f, 3f);

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
            var firstVector = Tupple.Vector(1f, 2f, 3f);
            var secondVector = Tupple.Vector(2f, 3f, 4f);

            // Act
            var dotProduct = Tupple.Dot(firstVector, secondVector);

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
            var vector = Tupple.Vector(1f, 2f, 3f);
            var point = Tupple.Point(1f, 2f, 3f);

            // Act
            var ex = Assert.Throws<ArithmeticException>(() => Tupple.Dot(vector, point));
            var ex2 = Assert.Throws<ArithmeticException>(() => Tupple.Dot(point, vector));

            // Assert
            Assert.Equal("This operation can only be used on Vectors!", ex.Message);
            Assert.Equal("This operation can only be used on Vectors!", ex2.Message);
        }

        [Fact]
        [Trait("Category", "Cross Product")]
        [Trait("Category", "Vector")]
        public void GivenTwoVectors_WhenGettingCrossProduct_ThenANewVectorIsReturned()
        {
            // Arrange
            var firstVector = Tupple.Vector(1f, 2f, 3f);
            var secondVector = Tupple.Vector(2f, 3f, 4f);

            // Act
            var crossVector = Tupple.Cross(firstVector, secondVector);
            var reversedCrossVector = Tupple.Cross(secondVector, firstVector);

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
            var vector = Tupple.Vector(1f, 2f, 3f);
            var point = Tupple.Point(2f, 3f, 4f);

            // Act
            var ex = Assert.Throws<ArithmeticException>(() => Tupple.Dot(vector, point));
            var ex2 = Assert.Throws<ArithmeticException>(() => Tupple.Dot(point, vector));

            // Assert
            Assert.Equal("This operation can only be used on Vectors!", ex.Message);
            Assert.Equal("This operation can only be used on Vectors!", ex2.Message);
        }

        [Fact]
        [Trait("Category", "Color")]
        [Trait("Category", "Vector")]
        public void GivenTupple_WhenCallingTuppleColor_ThenVectorIsReturned()
        {
            // Arrange
            var x = -.5f;
            var y = .4f;
            var z = 1.7f;

            // Act
            var color = Tupple.Color(x, y, z);

            // Assert
            Assert.Equal(x, color.X);
            Assert.Equal(y, color.Y);
            Assert.Equal(z, color.Z);
            Assert.Equal(Tupple.VectorIndicator, color.W);
        }

        [Fact]
        [Trait("Category", "Color")]
        [Trait("Category", "Addition")]
        public void GivenTwoColors_WhenAddingThemTogether_ThenANewColorIsReturned()
        {
            // Arrange
            var firstColor = Tupple.Color(.9f, .6f, .75f);
            var secondColor = Tupple.Color(.7f, .1f, .25f);

            // Act
            var color = firstColor + secondColor;

            // Assert
            Assert.Equal(1.6f, color.X, DefaultComparer);
            Assert.Equal(.7f, color.Y, DefaultComparer);
            Assert.Equal(1f, color.Z, DefaultComparer);
            Assert.Equal(Tupple.VectorIndicator, color.W);
        }

        [Fact]
        [Trait("Category", "Color")]
        [Trait("Category", "Substraction")]
        public void GivenTwoColors_WhenSubstractingThem_ThenANewColorIsReturned()
        {
            // Arrange
            var firstColor = Tupple.Color(.9f, .6f, .75f);
            var secondColor = Tupple.Color(.7f, .1f, .25f);

            // Act
            var color = firstColor - secondColor;

            // Assert
            Assert.Equal(.2f, color.X, DefaultComparer);
            Assert.Equal(.5f, color.Y, DefaultComparer);
            Assert.Equal(.5f, color.Z, DefaultComparer);
            Assert.Equal(Tupple.VectorIndicator, color.W);
        }

        [Fact]
        [Trait("Category", "Color")]
        [Trait("Category", "Multiplication")]
        public void GivenAColor_WhenMultiplyingWithAScalar_ThenANewColorIsReturned()
        {
            // Arrange
            var color = Tupple.Color(.2f, .3f, .4f);
            var scalar = 2f;

            // Act
            var newColor = color * scalar;

            // Assert
            Assert.Equal(.4f, newColor.X);
            Assert.Equal(.6f, newColor.Y);
            Assert.Equal(.8f, newColor.Z);
            Assert.Equal(Tupple.VectorIndicator, newColor.W);
        }

        [Fact]
        [Trait("Category", "Color")]
        [Trait("Category", "Multiplication")]
        public void GivenTwoColors_WhenMultiplyingThem_ThenANewColorIsReturned()
        {
            // Arrange
            var firstColor = Tupple.Color(1f, .2f, .4f);
            var secondColor = Tupple.Color(.9f, 1f, .1f);

            // Act
            var newColor = Tupple.HadamardProduct(firstColor, secondColor);

            // Assert
            Assert.Equal(.9f, newColor.X);
            Assert.Equal(.2f, newColor.Y);
            Assert.Equal(.04f, newColor.Z, DefaultComparer);
            Assert.Equal(Tupple.VectorIndicator, newColor.W);
        }
    }
}
