using RayTracerChallenge.Core.Test.Comparers;
using System;
using Xunit;

namespace RayTracerChallenge.Core.Test
{
    [Trait("Category", "Matrix")]
    public class MatrixTest
    {
        private ApproximateComparer DefaultComparer => new ApproximateComparer(.0001f);

        [Fact]
        public void GivenRowAndColumnOf4_WhenCreatingAnMatrix_ThenA4x4MatrixIsCreated()
        {
            // Arrange
            var rows = 4;
            var columns = 4;
            var matrix = new Matrix(rows, columns);

            // Act
            matrix[0, 0] = 1f;
            matrix[0, 1] = 2f;
            matrix[0, 2] = 3f;
            matrix[0, 3] = 4f;
            matrix[1, 0] = 5.5f;
            matrix[1, 1] = 6.5f;
            matrix[1, 2] = 7.5f;
            matrix[1, 3] = 8.5f;
            matrix[2, 0] = 9f;
            matrix[2, 1] = 10f;
            matrix[2, 2] = 11f;
            matrix[2, 3] = 12f;
            matrix[3, 0] = 13.5f;
            matrix[3, 1] = 14.5f;
            matrix[3, 2] = 15.5f;
            matrix[3, 3] = 16.5f;

            // Assert
            Assert.Equal(1f, matrix[0, 0]);
            Assert.Equal(4f, matrix[0, 3]);
            Assert.Equal(5.5f, matrix[1, 0]);
            Assert.Equal(7.5f, matrix[1, 2]);
            Assert.Equal(11f, matrix[2, 2]);
            Assert.Equal(13.5f, matrix[3, 0]);
            Assert.Equal(15.5f, matrix[3, 2]);
        }

        [Fact]
        public void GivenRowAndColumnOf2_WhenCreatingAnMatrix_ThenA2x2MatrixIsCreated()
        {
            // Arrange
            var rows = 2;
            var columns = 2;
            var matrix = new Matrix(rows, columns);

            // Act
            matrix[0, 0] = -3f;
            matrix[0, 1] = 5f;
            matrix[1, 0] = 1f;
            matrix[1, 1] = -2f;

            // Assert
            Assert.Equal(-3f, matrix[0, 0]);
            Assert.Equal(5f, matrix[0, 1]);
            Assert.Equal(1f, matrix[1, 0]);
            Assert.Equal(-2f, matrix[1, 1]);
        }

        [Fact]
        public void GivenRowAndColumnOf3_WhenCreatingAnMatrix_ThenA3x3MatrixIsCreated()
        {
            // Arrange
            var rows = 3;
            var columns = 3;
            var matrix = new Matrix(rows, columns);

            // Act
            matrix[0, 0] = -3f;
            matrix[0, 1] = 5f;
            matrix[0, 2] = 0f;
            matrix[1, 0] = 1f;
            matrix[1, 1] = -2f;
            matrix[1, 2] = -7f;
            matrix[2, 0] = 0f;
            matrix[2, 1] = 1f;
            matrix[2, 2] = 1f;

            // Assert
            Assert.Equal(-3f, matrix[0, 0]);
            Assert.Equal(-2f, matrix[1, 1]);
            Assert.Equal(1f, matrix[2, 2]);
        }

        [Fact]
        [Trait("Category", "Equal")]
        public void GivenTwoIdenticalMatrices_WhenCallingEqual_ThenTrueIsReturned()
        {
            // Arrange
            var rows = 4;
            var columns = 4;
            var firstMatrix = new Matrix(rows, columns);
            firstMatrix[0, 0] = 1f;
            firstMatrix[0, 1] = 2f;
            firstMatrix[0, 2] = 3f;
            firstMatrix[0, 3] = 4f;
            firstMatrix[1, 0] = 5f;
            firstMatrix[1, 1] = 6f;
            firstMatrix[1, 2] = 7f;
            firstMatrix[1, 3] = 8f;
            firstMatrix[2, 0] = 9f;
            firstMatrix[2, 1] = 8f;
            firstMatrix[2, 2] = 7f;
            firstMatrix[2, 3] = 6f;
            firstMatrix[3, 0] = 5f;
            firstMatrix[3, 1] = 4f;
            firstMatrix[3, 2] = 3f;
            firstMatrix[3, 3] = 2f;
            var secondMatrix = new Matrix(rows, columns);
            secondMatrix[0, 0] = 1f;
            secondMatrix[0, 1] = 2f;
            secondMatrix[0, 2] = 3f;
            secondMatrix[0, 3] = 4f;
            secondMatrix[1, 0] = 5f;
            secondMatrix[1, 1] = 6f;
            secondMatrix[1, 2] = 7f;
            secondMatrix[1, 3] = 8f;
            secondMatrix[2, 0] = 9f;
            secondMatrix[2, 1] = 8f;
            secondMatrix[2, 2] = 7f;
            secondMatrix[2, 3] = 6f;
            secondMatrix[3, 0] = 5f;
            secondMatrix[3, 1] = 4f;
            secondMatrix[3, 2] = 3f;
            secondMatrix[3, 3] = 2f;

            // Act
            var isEqual = firstMatrix.Equals(secondMatrix);

            // Assert
            Assert.True(isEqual);
        }

        [Fact]
        [Trait("Category", "Equal")]
        public void GivenTwoNonIdenticalMatrices_WhenCallingEqual_ThenFalseIsReturned()
        {
            // Arrange
            var rows = 4;
            var columns = 4;
            var firstMatrix = new Matrix(rows, columns);
            firstMatrix[0, 0] = 1f;
            firstMatrix[0, 1] = 2f;
            firstMatrix[0, 2] = 3f;
            firstMatrix[0, 3] = 4f;
            firstMatrix[1, 0] = 5f;
            firstMatrix[1, 1] = 6f;
            firstMatrix[1, 2] = 7f;
            firstMatrix[1, 3] = 8f;
            firstMatrix[2, 0] = 9f;
            firstMatrix[2, 1] = 8f;
            firstMatrix[2, 2] = 7f;
            firstMatrix[2, 3] = 6f;
            firstMatrix[3, 0] = 5f;
            firstMatrix[3, 1] = 4f;
            firstMatrix[3, 2] = 3f;
            firstMatrix[3, 3] = 2f;
            var secondMatrix = new Matrix(rows, columns);
            secondMatrix[0, 0] = 2f;
            secondMatrix[0, 1] = 3f;
            secondMatrix[0, 2] = 4f;
            secondMatrix[0, 3] = 5f;
            secondMatrix[1, 0] = 6f;
            secondMatrix[1, 1] = 7f;
            secondMatrix[1, 2] = 8f;
            secondMatrix[1, 3] = 9f;
            secondMatrix[2, 0] = 8f;
            secondMatrix[2, 1] = 7f;
            secondMatrix[2, 2] = 6f;
            secondMatrix[2, 3] = 5f;
            secondMatrix[3, 0] = 4f;
            secondMatrix[3, 1] = 3f;
            secondMatrix[3, 2] = 2f;
            secondMatrix[3, 3] = 1f;

            // Act
            var isEqual = firstMatrix.Equals(secondMatrix);

            // Assert
            Assert.False(isEqual);
        }

        [Fact]
        [Trait("Category", "Exception")]
        public void GivenMatrix_WhenSettingValueWithRowOutOfRange_ThenArgumentOutOfBoundExceptionIsThrown()
        {
            // Arrange
            var rows = 3;
            var columns = 3;
            var matrix = new Matrix(rows, columns);

            // Act
            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => matrix[rows + 1, 0] = 1f);

            // Assert
            Assert.Equal("row", ex.ParamName);
            var expectedException = new ArgumentOutOfRangeException("row", $"row can't be greater than {rows - 1}");
            Assert.Equal(expectedException.Message, ex.Message);
            
        }

        [Fact]
        [Trait("Category", "Exception")]
        public void GivenMatrix_WhenSettingValueWithColumnOutOfRange_ThenArgumentOutOfBoundExceptionIsThrown()
        {
            // Arrange
            var rows = 3;
            var columns = 3;
            var matrix = new Matrix(rows, columns);

            // Act
            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => matrix[0, columns + 1] = 1f);

            // Assert
            Assert.Equal("column", ex.ParamName);
            var expectedException = new ArgumentOutOfRangeException("column", $"column can't be greater than {columns - 1}");
            Assert.Equal(expectedException.Message, ex.Message);
        }

        [Fact]
        [Trait("Category", "Exception")]
        public void GivenMatrix_WhenGettingValueWithRowOutOfRange_ThenArgumentOutOfBoundExceptionIsThrown()
        {
            // Arrange
            var rows = 3;
            var columns = 3;
            var matrix = new Matrix(rows, columns);

            // Act
            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var value = matrix[rows + 1, 0];
            });

            // Assert
            Assert.Equal("row", ex.ParamName);
            var expectedException = new ArgumentOutOfRangeException("row", $"row can't be greater than {rows - 1}");
            Assert.Equal(expectedException.Message, ex.Message);
        }

        [Fact]
        [Trait("Category", "Exception")]
        public void GivenMatrix_WhenGettingValueWithColumnOutOfRange_ThenArgumentOutOfBoundExceptionIsThrown()
        {
            // Arrange
            var rows = 3;
            var columns = 3;
            var matrix = new Matrix(rows, columns);

            // Act
            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var value = matrix[0, columns + 1];
            });

            // Assert
            Assert.Equal("column", ex.ParamName);
            var expectedException = new ArgumentOutOfRangeException("column", $"column can't be greater than {columns - 1}");
            Assert.Equal(expectedException.Message, ex.Message);
        }

        [Fact]
        [Trait("Category", "Exception")]
        [Trait("Category", "Multiplication")]
        public void GivenTwoMatricesWithDifferentRows_WhenMultiplying_ThenArithmeticExceptionIsThrown()
        {
            // Arrange
            var columns = 3;
            var firstMatrix = new Matrix(3, columns);
            var secondMatrix = new Matrix(4, columns);

            // Act
            ArithmeticException ex = Assert.Throws<ArithmeticException>(() =>
            {
                var value = firstMatrix * secondMatrix;
            });

            // Assert
            Assert.Equal("Both matrices should have the same number of rows!", ex.Message);
        }

        [Fact]
        [Trait("Category", "Exception")]
        [Trait("Category", "Multiplication")]
        public void GivenTwoMatricesWithDifferentColumns_WhenMultiplying_ThenArithmeticExceptionIsThrown()
        {
            // Arrange
            var rows = 3;
            var firstMatrix = new Matrix(rows, 3);
            var secondMatrix = new Matrix(rows, 4);

            // Act
            ArithmeticException ex = Assert.Throws<ArithmeticException>(() =>
            {
                var value = firstMatrix * secondMatrix;
            });

            // Assert
            Assert.Equal("Both matrices should have the same number of columns!", ex.Message);
        }

        [Fact]
        [Trait("Category", "Exception")]
        [Trait("Category", "Multiplication")]
        public void GivenTwo5x5Matrices_WhenMultiplying_ThenArithmeticExceptionIsThrown()
        {
            // Arrange
            var rows = 5;
            var columns = 5;
            var firstMatrix = new Matrix(rows, columns);
            var secondMatrix = new Matrix(rows, columns);

            // Act
            ArithmeticException ex = Assert.Throws<ArithmeticException>(() =>
            {
                var value = firstMatrix * secondMatrix;
            });

            // Assert
            Assert.Equal("This operation only supports 4x4 Matrices!", ex.Message);
        }

        [Fact]
        [Trait("Category", "Multiplication")]
        public void GivenTwoMatrices_WhenMultiplying_ThenAMatrixIsReturned()
        {
            // Arrange
            var rows = 4;
            var columns = 4;
            var firstMatrix = new Matrix(rows, columns);
            var secondMatrix = new Matrix(rows, columns);

            firstMatrix[0, 0] = 1;
            firstMatrix[0, 1] = 2;
            firstMatrix[0, 2] = 3;
            firstMatrix[0, 3] = 4;
            firstMatrix[1, 0] = 5;
            firstMatrix[1, 1] = 6;
            firstMatrix[1, 2] = 7;
            firstMatrix[1, 3] = 8;
            firstMatrix[2, 0] = 9;
            firstMatrix[2, 1] = 8;
            firstMatrix[2, 2] = 7;
            firstMatrix[2, 3] = 6;
            firstMatrix[3, 0] = 5;
            firstMatrix[3, 1] = 4;
            firstMatrix[3, 2] = 3;
            firstMatrix[3, 3] = 2;

            secondMatrix[0, 0] = -2;
            secondMatrix[0, 1] = 1;
            secondMatrix[0, 2] = 2;
            secondMatrix[0, 3] = 3;
            secondMatrix[1, 0] = 3;
            secondMatrix[1, 1] = 2;
            secondMatrix[1, 2] = 1;
            secondMatrix[1, 3] = -1;
            secondMatrix[2, 0] = 4;
            secondMatrix[2, 1] = 3;
            secondMatrix[2, 2] = 6;
            secondMatrix[2, 3] = 5;
            secondMatrix[3, 0] = 1;
            secondMatrix[3, 1] = 2;
            secondMatrix[3, 2] = 7;
            secondMatrix[3, 3] = 8;

            // Act
            var newMatrix = firstMatrix * secondMatrix;

            // Assert
            Assert.Equal(20, newMatrix[0, 0]);
            Assert.Equal(22, newMatrix[0, 1]);
            Assert.Equal(50, newMatrix[0, 2]);
            Assert.Equal(48, newMatrix[0, 3]);
            Assert.Equal(44, newMatrix[1, 0]);
            Assert.Equal(54, newMatrix[1, 1]);
            Assert.Equal(114, newMatrix[1, 2]);
            Assert.Equal(108, newMatrix[1, 3]);
            Assert.Equal(40, newMatrix[2, 0]);
            Assert.Equal(58, newMatrix[2, 1]);
            Assert.Equal(110, newMatrix[2, 2]);
            Assert.Equal(102, newMatrix[2, 3]);
            Assert.Equal(16, newMatrix[3, 0]);
            Assert.Equal(26, newMatrix[3, 1]);
            Assert.Equal(46, newMatrix[3, 2]);
            Assert.Equal(42, newMatrix[3, 3]);
        }

        [Fact]
        [Trait("Category", "Multiplication")]
        [Trait("Category", "Tuple")]
        public void GivenAMatrixAndATuple_WhenMultiplying_ThenATupleIsReturned()
        {
            // Arrange
            var rows = 4;
            var columns = 4;
            var matrix = new Matrix(rows, columns);
            var tuple = new Tuple(1, 2, 3, 1);

            matrix[0, 0] = 1;
            matrix[0, 1] = 2;
            matrix[0, 2] = 3;
            matrix[0, 3] = 4;
            matrix[1, 0] = 2;
            matrix[1, 1] = 4;
            matrix[1, 2] = 4;
            matrix[1, 3] = 2;
            matrix[2, 0] = 8;
            matrix[2, 1] = 6;
            matrix[2, 2] = 4;
            matrix[2, 3] = 1;
            matrix[3, 0] = 0;
            matrix[3, 1] = 0;
            matrix[3, 2] = 0;
            matrix[3, 3] = 1;

            // Act
            var newTuple = matrix * tuple;

            // Assert
            var expectedTuple = new Tuple(18, 24, 33, 1);
            Assert.True(newTuple.Equals(expectedTuple));
        }

        [Fact]
        [Trait("Category", "Multiplication")]
        [Trait("Category", "Tuple")]
        [Trait("Category", "Exception")]
        public void GivenA5x5MatrixAndATuple_WhenMultiplying_ThenArithmeticExceptionIsThrown()
        {
            // Arrange
            var rows = 5;
            var columns = 5;
            var matrix = new Matrix(rows, columns);
            var tuple = new Tuple(1, 2, 3, 1);

            // Act
            ArithmeticException ex = Assert.Throws<ArithmeticException>(() =>
            {
                var value = matrix * tuple;
            });

            // Assert
            Assert.Equal("This operation only supports 4x4 Matrices!", ex.Message);
        }

        [Fact]
        [Trait("Category", "Multiplication")]
        [Trait("Category", "Identity Matrix")]
        public void GivenMatrix_WhenMultiplyingWithIdentityMatrix_ThenOriginalMatrixIsReturned()
        {
            // Arrange
            var rows = 4;
            var columns = 4;
            var matrix = new Matrix(rows, columns);

            matrix[0, 0] = 0;
            matrix[0, 1] = 1;
            matrix[0, 2] = 2;
            matrix[0, 3] = 4;
            matrix[1, 0] = 1;
            matrix[1, 1] = 2;
            matrix[1, 2] = 4;
            matrix[1, 3] = 8;
            matrix[2, 0] = 2;
            matrix[2, 1] = 4;
            matrix[2, 2] = 8;
            matrix[2, 3] = 16;
            matrix[3, 0] = 4;
            matrix[3, 1] = 8;
            matrix[3, 2] = 16;
            matrix[3, 3] = 32;

            // Act
            var newMatrix = matrix * Matrix.IdentityMatrix();

            // Assert
            Assert.True(matrix.Equals(newMatrix));
        }

        [Fact]
        [Trait("Category", "Multiplication")]
        [Trait("Category", "Tuple")]
        [Trait("Category", "Identity Matrix")]
        public void GivenTuple_WhenMultiplyingWithIdentityMatrix_ThenOriginalTupleIsReturned()
        {
            // Arrange
            var tuple = new Tuple(1, 2, 3, 4);

            // Act
            var newTuple = tuple * Matrix.IdentityMatrix();

            // Assert
            Assert.True(tuple.Equals(newTuple));
        }

        [Fact]
        [Trait("Category", "Transpose")]
        public void GivenMatrix_WhenTransposing_ThenTransposedMatrixIsReturned()
        {
            // Arrange
            var rows = 4;
            var columns = 4;
            var matrix = new Matrix(rows, columns);

            matrix[0, 0] = 0;
            matrix[0, 1] = 9;
            matrix[0, 2] = 3;
            matrix[0, 3] = 0;
            matrix[1, 0] = 9;
            matrix[1, 1] = 8;
            matrix[1, 2] = 0;
            matrix[1, 3] = 8;
            matrix[2, 0] = 1;
            matrix[2, 1] = 8;
            matrix[2, 2] = 5;
            matrix[2, 3] = 3;
            matrix[3, 0] = 0;
            matrix[3, 1] = 0;
            matrix[3, 2] = 5;
            matrix[3, 3] = 8;

            // Act
            var transposedMatrix = matrix.Transpose();

            // Assert
            var expectedMatrix = new Matrix(columns, rows);

            expectedMatrix[0, 0] = 0;
            expectedMatrix[0, 1] = 9;
            expectedMatrix[0, 2] = 1;
            expectedMatrix[0, 3] = 0;
            expectedMatrix[1, 0] = 9;
            expectedMatrix[1, 1] = 8;
            expectedMatrix[1, 2] = 8;
            expectedMatrix[1, 3] = 0;
            expectedMatrix[2, 0] = 3;
            expectedMatrix[2, 1] = 0;
            expectedMatrix[2, 2] = 5;
            expectedMatrix[2, 3] = 5;
            expectedMatrix[3, 0] = 0;
            expectedMatrix[3, 1] = 8;
            expectedMatrix[3, 2] = 3;
            expectedMatrix[3, 3] = 8;

            Assert.True(expectedMatrix.Equals(transposedMatrix));
        }

        [Fact]
        [Trait("Category", "Transpose")]
        [Trait("Category", "Identity Matrix")]
        public void GivenIdentityMatrix_WhenTransposing_ThenIdentityMatrixIsReturned()
        {
            // Arrange

            // Act
            var transposedMatrix = Matrix.IdentityMatrix().Transpose();

            // Assert
            Assert.True(transposedMatrix.Equals(Matrix.IdentityMatrix()));
        }

        [Fact]
        [Trait("Category", "Determinant")]
        public void GivenMatrix_WhenCalculatingDeterminant_ThenDeterminantValueIsReturned()
        {
            // Arrange
            var matrix = new Matrix(2, 2);
            matrix[0, 0] = 1f;
            matrix[0, 1] = 5f;
            matrix[1, 0] = -3f;
            matrix[1, 1] = 2f;

            // Act
            var determinant = matrix.Determinant();

            // Assert
            Assert.Equal(17f, determinant);
        }

        [Fact]
        [Trait("Category", "Submatrix")]
        public void Given3x3Matrix_WhenGetting2x2Submatrix_Then2x2SubmatrixIsReturned()
        {
            // Arrange
            var matrix = new Matrix(3, 3);
            matrix[0, 0] = 1f;
            matrix[0, 1] = 5f;
            matrix[0, 2] = 0f;
            matrix[1, 0] = -3f;
            matrix[1, 1] = 2f;
            matrix[1, 2] = 7f;
            matrix[2, 0] = 0f;
            matrix[2, 1] = 6f;
            matrix[2, 2] = -3f;

            // Act
            var submatrix = matrix.Submatrix(0, 2);

            // Assert
            Assert.Equal(2, submatrix.Columns);
            Assert.Equal(2, submatrix.Rows);
            Assert.Equal(-3f, submatrix[0, 0]);
            Assert.Equal(2f, submatrix[0, 1]);
            Assert.Equal(0f, submatrix[1, 0]);
            Assert.Equal(6f, submatrix[1, 1]);
        }

        [Fact]
        [Trait("Category", "Submatrix")]
        public void Given4x4Matrix_WhenGetting3x3Submatrix_Then3x3SubmatrixIsReturned()
        {
            // Arrange
            var matrix = new Matrix(4, 4);
            matrix[0, 0] = -6f;
            matrix[0, 1] = 1f;
            matrix[0, 2] = 1f;
            matrix[0, 3] = 6f;
            matrix[1, 0] = -8f;
            matrix[1, 1] = 5f;
            matrix[1, 2] = 8f;
            matrix[1, 3] = 6f;
            matrix[2, 0] = -1f;
            matrix[2, 1] = 0f;
            matrix[2, 2] = 8f;
            matrix[2, 3] = 2f;
            matrix[3, 0] = -7f;
            matrix[3, 1] = 1f;
            matrix[3, 2] = -1f;
            matrix[3, 3] = 1f;

            // Act
            var submatrix = matrix.Submatrix(2, 1);

            // Assert
            Assert.Equal(3, submatrix.Columns);
            Assert.Equal(3, submatrix.Rows);
            Assert.Equal(-6f, submatrix[0, 0]);
            Assert.Equal(1f, submatrix[0, 1]);
            Assert.Equal(6f, submatrix[0, 2]);
            Assert.Equal(-8f, submatrix[1, 0]);
            Assert.Equal(8f, submatrix[1, 1]);
            Assert.Equal(6f, submatrix[1, 2]);
            Assert.Equal(-7f, submatrix[2, 0]);
            Assert.Equal(-1f, submatrix[2, 1]);
            Assert.Equal(1f, submatrix[2, 2]);
        }

        [Fact]
        [Trait("Category", "Submatrix")]
        [Trait("Category", "Determinant")]
        [Trait("Category", "Minor")]
        public void Given3x3Matrix_WhenCalulatingMinor_ThenMinorIsReturned()
        {
            // Arrange
            var matrix = new Matrix(3, 3);
            matrix[0, 0] = 3f;
            matrix[0, 1] = 5f;
            matrix[0, 2] = 0f;
            matrix[1, 0] = 2f;
            matrix[1, 1] = -1f;
            matrix[1, 2] = -7f;
            matrix[2, 0] = 6f;
            matrix[2, 1] = -1f;
            matrix[2, 2] = 5f;

            var subMatrix = matrix.Submatrix(1, 0);
            var determinant = subMatrix.Determinant();

            // Act
            var minor = matrix.Minor(1, 0);

            // Assert
            Assert.Equal(minor, determinant);
            Assert.Equal(25f, minor);
            Assert.Equal(25f, determinant);
        }

        [Fact]
        [Trait("Category", "Cofactor")]
        [Trait("Category", "Minor")]
        public void Given3x3Matrix_WhenCalulatingCofactor_ThenCofactorIsReturned()
        {
            // Arrange
            var matrix = new Matrix(3, 3);
            matrix[0, 0] = 3f;
            matrix[0, 1] = 5f;
            matrix[0, 2] = 0f;
            matrix[1, 0] = 2f;
            matrix[1, 1] = -1f;
            matrix[1, 2] = -7f;
            matrix[2, 0] = 6f;
            matrix[2, 1] = -1f;
            matrix[2, 2] = 5f;

            var firstMinor = matrix.Minor(0, 0);
            var secondMinor = matrix.Minor(1, 0);

            // Act
            var firstCofactor = matrix.Cofactor(0, 0);
            var secondCofactor = matrix.Cofactor(1, 0);

            // Assert
            Assert.Equal(-12f, firstMinor);
            Assert.Equal(25f, secondMinor);
            Assert.Equal(-12f, firstCofactor);
            Assert.Equal(-25f, secondCofactor);
        }

        [Fact]
        [Trait("Category", "Cofactor")]
        [Trait("Category", "Determinant")]
        public void Given3x3Matrix_WhenCalulatingDeterminant_ThenDeterminantIsReturned()
        {
            // Arrange
            var matrix = new Matrix(3, 3);
            matrix[0, 0] = 1f;
            matrix[0, 1] = 2f;
            matrix[0, 2] = 6f;
            matrix[1, 0] = -5f;
            matrix[1, 1] = 8f;
            matrix[1, 2] = -4f;
            matrix[2, 0] = 2f;
            matrix[2, 1] = 6f;
            matrix[2, 2] = 4f;

            var firstCofactor = matrix.Cofactor(0, 0);
            var secondCofactor = matrix.Cofactor(0, 1);
            var thirdCofactor = matrix.Cofactor(0, 2);

            // Act
            var determinant = matrix.Determinant();

            // Assert
            Assert.Equal(56f, firstCofactor);
            Assert.Equal(12f, secondCofactor);
            Assert.Equal(-46f, thirdCofactor);
            Assert.Equal(-196f, determinant);
        }

        [Fact]
        [Trait("Category", "Cofactor")]
        [Trait("Category", "Determinant")]
        public void Given4x4Matrix_WhenCalulatingDeterminant_ThenDeterminantIsReturned()
        {
            // Arrange
            var matrix = new Matrix(4, 4);
            matrix[0, 0] = -2f;
            matrix[0, 1] = -8f;
            matrix[0, 2] = 3f;
            matrix[0, 3] = 5f;
            matrix[1, 0] = -3f;
            matrix[1, 1] = 1f;
            matrix[1, 2] = 7f;
            matrix[1, 3] = 3f;
            matrix[2, 0] = 1f;
            matrix[2, 1] = 2f;
            matrix[2, 2] = -9f;
            matrix[2, 3] = 6f;
            matrix[3, 0] = -6f;
            matrix[3, 1] = 7f;
            matrix[3, 2] = 7f;
            matrix[3, 3] = -9f;

            var firstCofactor = matrix.Cofactor(0, 0);
            var secondCofactor = matrix.Cofactor(0, 1);
            var thirdCofactor = matrix.Cofactor(0, 2);
            var fourthCofactor = matrix.Cofactor(0, 3);

            // Act
            var determinant = matrix.Determinant();

            // Assert
            Assert.Equal(690f, firstCofactor);
            Assert.Equal(447f, secondCofactor);
            Assert.Equal(210f, thirdCofactor);
            Assert.Equal(51f, fourthCofactor);
            Assert.Equal(-4071f, determinant);
        }

        [Fact]
        [Trait("Category", "Inverse")]
        [Trait("Category", "Determinant")]
        public void GivenInvertibleMatrix_WhenTestingForInvertibility_ThenReturnTrue()
        {
            // Arrange
            var matrix = new Matrix(4, 4);
            matrix[0, 0] = 6f;
            matrix[0, 1] = 4f;
            matrix[0, 2] = 4f;
            matrix[0, 3] = 4f;
            matrix[1, 0] = 5f;
            matrix[1, 1] = 5f;
            matrix[1, 2] = 7f;
            matrix[1, 3] = 6f;
            matrix[2, 0] = 4f;
            matrix[2, 1] = -9f;
            matrix[2, 2] = 3f;
            matrix[2, 3] = -7f;
            matrix[3, 0] = 9f;
            matrix[3, 1] = 1f;
            matrix[3, 2] = 7f;
            matrix[3, 3] = -6f;

            var determinant = matrix.Determinant();

            // Act
            var isInvertible = matrix.IsInvertible;

            // Assert
            Assert.True(isInvertible);
            Assert.Equal(-2120f, determinant);
        }

        [Fact]
        [Trait("Category", "Inverse")]
        [Trait("Category", "Determinant")]
        public void GivenNonInvertibleMatrix_WhenTestingForInvertibility_ThenReturnFalse()
        {
            // Arrange
            var matrix = new Matrix(4, 4);
            matrix[0, 0] = -4f;
            matrix[0, 1] = 2f;
            matrix[0, 2] = -2f;
            matrix[0, 3] = -3f;
            matrix[1, 0] = 9f;
            matrix[1, 1] = 6f;
            matrix[1, 2] = 2f;
            matrix[1, 3] = 6f;
            matrix[2, 0] = 0f;
            matrix[2, 1] = -5f;
            matrix[2, 2] = 1f;
            matrix[2, 3] = -5f;
            matrix[3, 0] = 0f;
            matrix[3, 1] = 0f;
            matrix[3, 2] = 0f;
            matrix[3, 3] = 0f;

            var determinant = matrix.Determinant();

            // Act
            var isInvertible = matrix.IsInvertible;

            // Assert
            Assert.False(isInvertible);
            Assert.Equal(0f, determinant);
        }

        [Fact]
        [Trait("Category", "Inverse")]
        [Trait("Category", "Cofactor")]
        [Trait("Category", "Determinant")]
        public void Given4x4Matrix_WhenCalculatingInverse_ThenInverseMatrixIsReturned()
        {
            // Arrange
            var matrix = new Matrix(4, 4);
            matrix[0, 0] = -5f;
            matrix[0, 1] = 2f;
            matrix[0, 2] = 6f;
            matrix[0, 3] = -8f;
            matrix[1, 0] = 1f;
            matrix[1, 1] = -5f;
            matrix[1, 2] = 1f;
            matrix[1, 3] = 8f;
            matrix[2, 0] = 7f;
            matrix[2, 1] = 7f;
            matrix[2, 2] = -6f;
            matrix[2, 3] = -7f;
            matrix[3, 0] = 1f;
            matrix[3, 1] = -3f;
            matrix[3, 2] = 7f;
            matrix[3, 3] = 4f;

            var determinant = matrix.Determinant();
            var firstCofactor = matrix.Cofactor(2, 3);
            var secondCofactor = matrix.Cofactor(3, 2);

            // Act
            var inversedMatrix = matrix.Inverse();

            // Asserts
            Assert.Equal(532f, determinant);
            Assert.Equal(-160f, firstCofactor);
            Assert.Equal(105f, secondCofactor);
            Assert.Equal(firstCofactor / determinant, inversedMatrix[3, 2]);
            Assert.Equal(secondCofactor / determinant, inversedMatrix[2, 3]);

            Assert.Equal(.21805f, inversedMatrix[0, 0], DefaultComparer);
            Assert.Equal(.45113f, inversedMatrix[0, 1], DefaultComparer);
            Assert.Equal(.24060f, inversedMatrix[0, 2], DefaultComparer);
            Assert.Equal(-.04511f, inversedMatrix[0, 3], DefaultComparer);
            Assert.Equal(-.80827f, inversedMatrix[1, 0], DefaultComparer);
            Assert.Equal(-1.45677f, inversedMatrix[1, 1], DefaultComparer);
            Assert.Equal(-.44361f, inversedMatrix[1, 2], DefaultComparer);
            Assert.Equal(.52068f, inversedMatrix[1, 3], DefaultComparer);
            Assert.Equal(-.07895f, inversedMatrix[2, 0], DefaultComparer);
            Assert.Equal(-.22368f, inversedMatrix[2, 1], DefaultComparer);
            Assert.Equal(-.05263f, inversedMatrix[2, 2], DefaultComparer);
            Assert.Equal(.19737f, inversedMatrix[2, 3], DefaultComparer);
            Assert.Equal(-.52256f, inversedMatrix[3, 0], DefaultComparer);
            Assert.Equal(-.81391f, inversedMatrix[3, 1], DefaultComparer);
            Assert.Equal(-.30075f, inversedMatrix[3, 2], DefaultComparer);
            Assert.Equal(.30639f, inversedMatrix[3, 3], DefaultComparer);
        }

        [Fact]
        [Trait("Category", "Inverse")]
        [Trait("Category", "Cofactor")]
        [Trait("Category", "Determinant")]
        public void GivenASecond4x4Matrix_WhenCalculatingInverse_ThenInverseMatrixIsReturned()
        {
            // Arrange
            var matrix = new Matrix(4, 4);
            matrix[0, 0] = 8f;
            matrix[0, 1] = -5f;
            matrix[0, 2] = 9f;
            matrix[0, 3] = 2f;
            matrix[1, 0] = 7f;
            matrix[1, 1] = 5f;
            matrix[1, 2] = 6f;
            matrix[1, 3] = 1f;
            matrix[2, 0] = -6f;
            matrix[2, 1] = 0f;
            matrix[2, 2] = 9f;
            matrix[2, 3] = 6f;
            matrix[3, 0] = -3f;
            matrix[3, 1] = 0f;
            matrix[3, 2] = -9f;
            matrix[3, 3] = -4f;

            // Act
            var inversedMatrix = matrix.Inverse();

            // Asserts
            Assert.Equal(-.15385f, inversedMatrix[0, 0], DefaultComparer);
            Assert.Equal(-.15385f, inversedMatrix[0, 1], DefaultComparer);
            Assert.Equal(-.28205f, inversedMatrix[0, 2], DefaultComparer);
            Assert.Equal(-.53846f, inversedMatrix[0, 3], DefaultComparer);

            Assert.Equal(-.07692f, inversedMatrix[1, 0], DefaultComparer);
            Assert.Equal(.12308f, inversedMatrix[1, 1], DefaultComparer);
            Assert.Equal(.02564f, inversedMatrix[1, 2], DefaultComparer);
            Assert.Equal(.03077f, inversedMatrix[1, 3], DefaultComparer);

            Assert.Equal(.35897f, inversedMatrix[2, 0], DefaultComparer);
            Assert.Equal(.35897f, inversedMatrix[2, 1], DefaultComparer);
            Assert.Equal(.43590f, inversedMatrix[2, 2], DefaultComparer);
            Assert.Equal(.92308f, inversedMatrix[2, 3], DefaultComparer);

            Assert.Equal(-.69231f, inversedMatrix[3, 0], DefaultComparer);
            Assert.Equal(-.69231f, inversedMatrix[3, 1], DefaultComparer);
            Assert.Equal(-.76923f, inversedMatrix[3, 2], DefaultComparer);
            Assert.Equal(-1.92308f, inversedMatrix[3, 3], DefaultComparer);
        }

        [Fact]
        [Trait("Category", "Inverse")]
        [Trait("Category", "Cofactor")]
        [Trait("Category", "Determinant")]
        public void GivenAThird4x4Matrix_WhenCalculatingInverse_ThenInverseMatrixIsReturned()
        {
            // Arrange
            var matrix = new Matrix(4, 4);
            matrix[0, 0] = 9f;
            matrix[0, 1] = 3f;
            matrix[0, 2] = 0f;
            matrix[0, 3] = 9f;
            matrix[1, 0] = -5f;
            matrix[1, 1] = -2f;
            matrix[1, 2] = -6f;
            matrix[1, 3] = -3f;
            matrix[2, 0] = -4f;
            matrix[2, 1] = 9f;
            matrix[2, 2] = 6f;
            matrix[2, 3] = 4f;
            matrix[3, 0] = -7f;
            matrix[3, 1] = 6f;
            matrix[3, 2] = 6f;
            matrix[3, 3] = 2f;

            // Act
            var inversedMatrix = matrix.Inverse();

            // Asserts
            Assert.Equal(-.04074f, inversedMatrix[0, 0], DefaultComparer);
            Assert.Equal(-.07778f, inversedMatrix[0, 1], DefaultComparer);
            Assert.Equal(.14444f, inversedMatrix[0, 2], DefaultComparer);
            Assert.Equal(-.22222f, inversedMatrix[0, 3], DefaultComparer);

            Assert.Equal(-.07778f, inversedMatrix[1, 0], DefaultComparer);
            Assert.Equal(.03333334f, inversedMatrix[1, 1], DefaultComparer);
            Assert.Equal(.36667f, inversedMatrix[1, 2], DefaultComparer);
            Assert.Equal(-.33333f, inversedMatrix[1, 3], DefaultComparer);

            Assert.Equal(-.02901f, inversedMatrix[2, 0], DefaultComparer);
            Assert.Equal(-.14630f, inversedMatrix[2, 1], DefaultComparer);
            Assert.Equal(-.10926f, inversedMatrix[2, 2], DefaultComparer);
            Assert.Equal(.12963f, inversedMatrix[2, 3], DefaultComparer);

            Assert.Equal(.17778f, inversedMatrix[3, 0], DefaultComparer);
            Assert.Equal(.06667f, inversedMatrix[3, 1], DefaultComparer);
            Assert.Equal(-.26667f, inversedMatrix[3, 2], DefaultComparer);
            Assert.Equal(.33333f, inversedMatrix[3, 3], DefaultComparer);
        }

        [Fact]
        [Trait("Category", "Inverse")]
        [Trait("Category", "Exception")]
        [Trait("Category", "Determinant")]
        public void GivenNonInvertibleMatrix_WhenCalculatingInverse_ThenArithmeticExceptionIsThrown()
        {
            // Arrange
            var matrix = new Matrix(4, 4);
            matrix[0, 0] = -4f;
            matrix[0, 1] = 2f;
            matrix[0, 2] = -2f;
            matrix[0, 3] = -3f;
            matrix[1, 0] = 9f;
            matrix[1, 1] = 6f;
            matrix[1, 2] = 2f;
            matrix[1, 3] = 6f;
            matrix[2, 0] = 0f;
            matrix[2, 1] = -5f;
            matrix[2, 2] = 1f;
            matrix[2, 3] = -5f;
            matrix[3, 0] = 0f;
            matrix[3, 1] = 0f;
            matrix[3, 2] = 0f;
            matrix[3, 3] = 0f;

            var determinant = matrix.Determinant();

            // Act
            ArithmeticException ex = Assert.Throws<ArithmeticException>(() =>
            {
                var value = matrix.Inverse();
            });

            // Assert
            Assert.Equal("This matrix is not invertible!", ex.Message);
            Assert.Equal(0f, determinant);
        }

        [Fact]
        [Trait("Category", "Inverse")]
        [Trait("Category", "Multiplication")]
        public void GivenAProductOfMultiplication_WhenMultiplyingByInverse_ThenOriginalMatrixIsReturned()
        {
            // Arrange
            var firstMatrix = new Matrix(4, 4);
            firstMatrix[0, 0] = 3f;
            firstMatrix[0, 1] = -9f;
            firstMatrix[0, 2] = 7f;
            firstMatrix[0, 3] = 3f;
            firstMatrix[1, 0] = 3f;
            firstMatrix[1, 1] = -8f;
            firstMatrix[1, 2] = 2f;
            firstMatrix[1, 3] = -9f;
            firstMatrix[2, 0] = -4f;
            firstMatrix[2, 1] = 4f;
            firstMatrix[2, 2] = 4f;
            firstMatrix[2, 3] = 1f;
            firstMatrix[3, 0] = -6f;
            firstMatrix[3, 1] = 5f;
            firstMatrix[3, 2] = -1f;
            firstMatrix[3, 3] = 1f;

            var secondMatrix = new Matrix(4, 4);
            secondMatrix[0, 0] = 8f;
            secondMatrix[0, 1] = 2f;
            secondMatrix[0, 2] = 2f;
            secondMatrix[0, 3] = 2f;
            secondMatrix[1, 0] = 3f;
            secondMatrix[1, 1] = -1f;
            secondMatrix[1, 2] = 7f;
            secondMatrix[1, 3] = 0f;
            secondMatrix[2, 0] = 7f;
            secondMatrix[2, 1] = 0f;
            secondMatrix[2, 2] = 5f;
            secondMatrix[2, 3] = 4f;
            secondMatrix[3, 0] = 6f;
            secondMatrix[3, 1] = -2f;
            secondMatrix[3, 2] = 0f;
            secondMatrix[3, 3] = 5f;

            var product = firstMatrix * secondMatrix;

            // Act
            var firstMatrixRecreated = product * secondMatrix.Inverse();

            // Asserts
            Assert.Equal(firstMatrix[0, 0], firstMatrixRecreated[0, 0], DefaultComparer);
            Assert.Equal(firstMatrix[0, 1], firstMatrixRecreated[0, 1], DefaultComparer);
            Assert.Equal(firstMatrix[0, 2], firstMatrixRecreated[0, 2], DefaultComparer);
            Assert.Equal(firstMatrix[0, 3], firstMatrixRecreated[0, 3], DefaultComparer);
            Assert.Equal(firstMatrix[1, 0], firstMatrixRecreated[1, 0], DefaultComparer);
            Assert.Equal(firstMatrix[1, 1], firstMatrixRecreated[1, 1], DefaultComparer);
            Assert.Equal(firstMatrix[1, 2], firstMatrixRecreated[1, 2], DefaultComparer);
            Assert.Equal(firstMatrix[1, 3], firstMatrixRecreated[1, 3], DefaultComparer);
            Assert.Equal(firstMatrix[2, 0], firstMatrixRecreated[2, 0], DefaultComparer);
            Assert.Equal(firstMatrix[2, 1], firstMatrixRecreated[2, 1], DefaultComparer);
            Assert.Equal(firstMatrix[2, 2], firstMatrixRecreated[2, 2], DefaultComparer);
            Assert.Equal(firstMatrix[2, 3], firstMatrixRecreated[2, 3], DefaultComparer);
            Assert.Equal(firstMatrix[3, 0], firstMatrixRecreated[3, 0], DefaultComparer);
            Assert.Equal(firstMatrix[3, 1], firstMatrixRecreated[3, 1], DefaultComparer);
            Assert.Equal(firstMatrix[3, 2], firstMatrixRecreated[3, 2], DefaultComparer);
            Assert.Equal(firstMatrix[3, 3], firstMatrixRecreated[3, 3], DefaultComparer);
        }

        [Fact]
        [Trait("Category", "Translation")]
        [Trait("Category", "Multiplication")]
        [Trait("Category", "Point")]
        public void GivenAPoint_WhenMultiplyingByATranslationMatrix_ThenTranslatedPointIsReturned()
        {
            // Arrange
            var transform = Matrix.Translate(5, -3, 2);
            var point = new Point(-3, 4, 5);

            // Act
            var translatedPoint = transform * point;

            // Asserts
            var expectedPoint = new Point(2, 1, 7);

            Assert.True(expectedPoint.Equals(translatedPoint));
        }

        [Fact]
        [Trait("Category", "Translation")]
        [Trait("Category", "Inverse")]
        [Trait("Category", "Multiplication")]
        [Trait("Category", "Point")]
        public void GivenAPoint_WhenMultiplyingByTheInverseOfATranslationMatrix_ThenTranslatedPointIsReturned()
        {
            // Arrange
            var transform = Matrix.Translate(5, -3, 2);
            var inverse = transform.Inverse();
            var point = new Point(-3, 4, 5);

            // Act
            var translatedPoint = inverse * point;

            // Asserts
            var expectedPoint = new Point(-8, 7, 3);

            Assert.True(expectedPoint.Equals(translatedPoint));
        }

        [Fact]
        [Trait("Category", "Translation")]
        [Trait("Category", "Vector")]
        [Trait("Category", "Multiplication")]
        public void GivenAVector_WhenMultiplyingByTheInverseOfATranslationMatrix_ThenTranslationDoesNotAffectVectors()
        {
            // Arrange
            var transform = Matrix.Translate(5, -3, 2);
            var vector = new Vector(-3, 4, 5);

            // Act
            var translatedVector = transform * vector;

            // Asserts            
            Assert.True(vector.Equals(translatedVector));
        }

        [Fact]
        [Trait("Category", "Scaling")]
        [Trait("Category", "Point")]
        [Trait("Category", "Multiplication")]
        public void GivenAPoint_WhenApplyingAScalingMatrix_ThenAScaledPointIsReturned()
        {
            // Arrange
            var transform = Matrix.Scale(2, 3, 4);
            var point = new Point(-4, 6, 8);

            // Act
            var scaledPoint = transform * point;

            // Asserts
            var expectedPoint = new Point(-8, 18, 32);

            Assert.True(expectedPoint.Equals(scaledPoint));
        }

        [Fact]
        [Trait("Category", "Scaling")]
        [Trait("Category", "Vector")]
        [Trait("Category", "Multiplication")]
        public void GivenAVector_WhenApplyingAScalingMatrix_ThenAScaledVectorIsReturned()
        {
            // Arrange
            var transform = Matrix.Scale(2, 3, 4);
            var vector = new Vector(-4, 6, 8);

            // Act
            var scaledVector = transform * vector;

            // Asserts
            var expectedVector = new Vector(-8, 18, 32);

            Assert.True(expectedVector.Equals(scaledVector));
        }

        [Fact]
        [Trait("Category", "Translation")]
        [Trait("Category", "Inverse")]
        [Trait("Category", "Multiplication")]
        [Trait("Category", "Point")]
        public void GivenAVector_WhenMultiplyingByTheInverseOfAScalingMatrix_ThenScaledVectorIsReturned()
        {
            // Arrange
            var transform = Matrix.Scale(2, 3, 4);
            var inverse = transform.Inverse();
            var vector = new Vector(-4, 6, 8);

            // Act
            var scaledVector = inverse * vector;

            // Asserts
            var expectedVector = new Vector(-2, 2, 2);

            Assert.True(expectedVector.Equals(scaledVector));
        }

        [Fact]
        [Trait("Category", "Scaling")]
        [Trait("Category", "Point")]
        [Trait("Category", "Reflection")]
        [Trait("Category", "Multiplication")]
        public void GivenAPoint_WhenApplyingANegativeScalingMatrix_ThenAReflectedPointIsReturned()
        {
            // Arrange
            var transform = Matrix.Scale(-1, 1, 1);
            var point = new Point(2, 3, 4);

            // Act
            var scaledPoint = transform * point;

            // Asserts
            var expectedPoint = new Point(-2, 3, 4);

            Assert.True(expectedPoint.Equals(scaledPoint));
        }

        [Fact]
        [Trait("Category", "Rotate")]
        [Trait("Category", "Point")]
        [Trait("Category", "Multiplication")]
        public void GivenAPoint_WhenRotatingAroundTheXAxis_ThenRotatedPointIsReturned()
        {
            // Arrange
            var point = new Point(0, 1, 0);

            var halfQuarterRotation = Matrix.RotateX(MathF.PI / 4f);
            var quarterRotation = Matrix.RotateX(MathF.PI / 2f);

            // Act
            var halfQuarterPoint = halfQuarterRotation * point;
            var quarterPoint = quarterRotation * point;

            // Asserts
            var expectedHalfQuarterPoint = new Point(0, MathF.Sqrt(2) / 2, MathF.Sqrt(2) / 2);
            var expectedQuarterPoint = new Point(0, 0, 1);

            Assert.True(expectedHalfQuarterPoint.Equals(halfQuarterPoint, 0));
            Assert.True(expectedQuarterPoint.Equals(quarterPoint, 0));
        }

        [Fact]
        [Trait("Category", "Rotate")]
        [Trait("Category", "Inverse")]
        [Trait("Category", "Point")]
        [Trait("Category", "Multiplication")]
        public void GivenAPoint_WhenRotatingAroundTheInvertedXAxis_ThenRotatedPointIsReturned()
        {
            // Arrange
            var point = new Point(0, 1, 0);

            var halfQuarterRotation = Matrix.RotateX(MathF.PI / 4);
            var invertedHalfQuarterRotation = halfQuarterRotation.Inverse();

            // Act
            var halfQuarterPoint = invertedHalfQuarterRotation * point;

            // Asserts
            var expectedHalfQuarterPoint = new Point(0, MathF.Sqrt(2) / 2, -(MathF.Sqrt(2) / 2));

            Assert.Equal(expectedHalfQuarterPoint.X, halfQuarterPoint.X, DefaultComparer);
            Assert.Equal(expectedHalfQuarterPoint.Y, halfQuarterPoint.Y, DefaultComparer);
            Assert.Equal(expectedHalfQuarterPoint.Z, halfQuarterPoint.Z, DefaultComparer);
        }

        [Fact]
        [Trait("Category", "Rotate")]
        [Trait("Category", "Point")]
        [Trait("Category", "Multiplication")]
        public void GivenAPoint_WhenRotatingAroundTheYAxis_ThenRotatedPointIsReturned()
        {
            // Arrange
            var point = new Point(0, 0, 1);

            var halfQuarterRotation = Matrix.RotateY(MathF.PI / 4f);
            var quarterRotation = Matrix.RotateY(MathF.PI / 2f);

            // Act
            var halfQuarterPoint = halfQuarterRotation * point;
            var quarterPoint = quarterRotation * point;

            // Asserts
            var expectedHalfQuarterPoint = new Point(MathF.Sqrt(2) / 2, 0, MathF.Sqrt(2) / 2);
            var expectedQuarterPoint = new Point(1, 0, 0);

            Assert.True(expectedHalfQuarterPoint.Equals(halfQuarterPoint, 0));
            Assert.True(expectedQuarterPoint.Equals(quarterPoint, 0));
        }

        [Fact]
        [Trait("Category", "Rotate")]
        [Trait("Category", "Point")]
        [Trait("Category", "Multiplication")]
        public void GivenAPoint_WhenRotatingAroundTheZAxis_ThenRotatedPointIsReturned()
        {
            // Arrange
            var point = new Point(0, 1, 0);

            var halfQuarterRotation = Matrix.RotateZ(MathF.PI / 4f);
            var quarterRotation = Matrix.RotateZ(MathF.PI / 2f);

            // Act
            var halfQuarterPoint = halfQuarterRotation * point;
            var quarterPoint = quarterRotation * point;

            // Asserts
            var expectedHalfQuarterPoint = new Point(-MathF.Sqrt(2) / 2, MathF.Sqrt(2) / 2, 0);
            var expectedQuarterPoint = new Point(-1, 0, 0);

            Assert.True(expectedHalfQuarterPoint.Equals(halfQuarterPoint, 0));
            Assert.True(expectedQuarterPoint.Equals(quarterPoint, 0));
        }

        [Fact]
        [Trait("Category", "Shear")]
        [Trait("Category", "Point")]
        [Trait("Category", "Multiplication")]
        public void GivenAShearingTransformation_WhenXMovesInProportionToY_ThenAPointIsReturned()
        {
            // Arrange
            var transform = Matrix.Shear(1, 0, 0, 0, 0, 0);
            var point = new Point(2, 3, 4);

            // Act
            var transformedPoint = transform * point;

            // Asserts
            var expectedPoint = new Point(5, 3, 4);

            Assert.True(expectedPoint.Equals(transformedPoint));
        }

        [Fact]
        [Trait("Category", "Shear")]
        [Trait("Category", "Point")]
        [Trait("Category", "Multiplication")]
        public void GivenAShearingTransformation_WhenXMovesInProportionToZ_ThenAPointIsReturned()
        {
            // Arrange
            var transform = Matrix.Shear(0, 1, 0, 0, 0, 0);
            var point = new Point(2, 3, 4);

            // Act
            var transformedPoint = transform * point;

            // Asserts
            var expectedPoint = new Point(6, 3, 4);

            Assert.True(expectedPoint.Equals(transformedPoint));
        }

        [Fact]
        [Trait("Category", "Shear")]
        [Trait("Category", "Point")]
        [Trait("Category", "Multiplication")]
        public void GivenAShearingTransformation_WhenYMovesInProportionToX_ThenAPointIsReturned()
        {
            // Arrange
            var transform = Matrix.Shear(0, 0, 1, 0, 0, 0);
            var point = new Point(2, 3, 4);

            // Act
            var transformedPoint = transform * point;

            // Asserts
            var expectedPoint = new Point(2, 5, 4);

            Assert.True(expectedPoint.Equals(transformedPoint));
        }

        [Fact]
        [Trait("Category", "Shear")]
        [Trait("Category", "Point")]
        [Trait("Category", "Multiplication")]
        public void GivenAShearingTransformation_WhenYMovesInProportionToZ_ThenAPointIsReturned()
        {
            // Arrange
            var transform = Matrix.Shear(0, 0, 0, 1, 0, 0);
            var point = new Point(2, 3, 4);

            // Act
            var transformedPoint = transform * point;

            // Asserts
            var expectedPoint = new Point(2, 7, 4);

            Assert.True(expectedPoint.Equals(transformedPoint));
        }

        [Fact]
        [Trait("Category", "Shear")]
        [Trait("Category", "Point")]
        [Trait("Category", "Multiplication")]
        public void GivenAShearingTransformation_WhenZMovesInProportionToX_ThenAPointIsReturned()
        {
            // Arrange
            var transform = Matrix.Shear(0, 0, 0, 0, 1, 0);
            var point = new Point(2, 3, 4);

            // Act
            var transformedPoint = transform * point;

            // Asserts
            var expectedPoint = new Point(2, 3, 6);

            Assert.True(expectedPoint.Equals(transformedPoint));
        }

        [Fact]
        [Trait("Category", "Shear")]
        [Trait("Category", "Point")]
        [Trait("Category", "Multiplication")]
        public void GivenAShearingTransformation_WhenZMovesInProportionToY_ThenAPointIsReturned()
        {
            // Arrange
            var transform = Matrix.Shear(0, 0, 0, 0, 0, 1);
            var point = new Point(2, 3, 4);

            // Act
            var transformedPoint = transform * point;

            // Asserts
            var expectedPoint = new Point(2, 3, 7);

            Assert.True(expectedPoint.Equals(transformedPoint));
        }

        [Fact]
        [Trait("Category", "Rotate")]
        [Trait("Category", "Scale")]
        [Trait("Category", "Translate")]
        [Trait("Category", "Point")]
        [Trait("Category", "Multiplication")]
        public void GivenIndividualTransformations_WhenAppliedInSequense_ThenAPointIsReturned()
        {
            // Arrange
            var p = new Point(1, 0, 1);
            var a = Matrix.RotateX(MathF.PI / 2);
            var b = Matrix.Scale(5, 5, 5);
            var c = Matrix.Translate(10, 5, 7);

            // Act
            var p2 = a * p;
            var p3 = b * p2;
            var p4 = c * p3;

            // Asserts
            var expectedP2 = new Point(1, -1, 0);
            var expectedP3 = new Point(5, -5, 0);
            var expectedP4 = new Point(15, 0, 7);

            Assert.True(expectedP2.Equals(p2, 0));
            Assert.True(expectedP3.Equals(p3, 0));
            Assert.True(expectedP4.Equals(p4));
        }

        [Fact]
        [Trait("Category", "Rotate")]
        [Trait("Category", "Scale")]
        [Trait("Category", "Translate")]
        [Trait("Category", "Point")]
        [Trait("Category", "Multiplication")]
        public void GivenChainedTransformations_WhenAppliedInReverseOrder_ThenAPointIsReturned()
        {
            // Arrange
            var p = new Point(1, 0, 1);
            var a = Matrix.RotateX(MathF.PI / 2);
            var b = Matrix.Scale(5, 5, 5);
            var c = Matrix.Translate(10, 5, 7);

            // Act
            var t = c * b * a;
            var tp = t * p;

            // Asserts
            var expectedTp = new Point(15, 0, 7);

            Assert.True(expectedTp.Equals(tp));
        }
    }
}
