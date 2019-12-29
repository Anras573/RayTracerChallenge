using System;
using System.Text;

namespace RayTracerChallenge.Core
{
    public struct Matrix
    {
        public int Rows { get; }
        public int Columns { get; }

        public bool IsInvertible => Determinant(this) != 0;

        private float[] Matrices { get; }

        public Matrix(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Matrices = new float[rows * columns];
        }

        public float this[int row, int column]
        {
            get
            {
                if (row > Rows - 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(row), $"row can't be greater than {Rows - 1}");
                }

                if (column > Columns - 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(column), $"column can't be greater than {Columns - 1}");
                }

                var index = CalculateIndex(row, column);
                return Matrices[index];
            }
            set
            {
                if (row > Rows - 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(row), $"row can't be greater than {Rows - 1}");
                }

                if (column > Columns - 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(column), $"column can't be greater than {Columns - 1}");
                }

                var index = CalculateIndex(row, column);
                Matrices[index] = value;
            }
        }

        private int CalculateIndex(int row, int column)
        {
            return (row * Columns) + column;
        }

        public bool Equals(Matrix other)
        {
            if (other.Rows != Rows || other.Columns != Columns)
            {
                return false;
            }

            for (var row = 0; row < Rows; row++)
            {
                for (var column = 0; column < Columns; column++)
                {
                    if (other[row, column] != this[row, column])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static Matrix operator *(Matrix left, Matrix right)
        {
            if (left.Rows != right.Rows)
            {
                throw new ArithmeticException("Both matrices should have the same number of rows!");
            }

            if (left.Columns != right.Columns)
            {
                throw new ArithmeticException("Both matrices should have the same number of columns!");
            }

            // No need to check for right, as we already assured they're of the same size!
            if (left.Rows != 4 || left.Columns != 4)
            {
                throw new ArithmeticException("This operation only supports 4x4 Matrices!");
            }

            var matrix = new Matrix(left.Rows, left.Columns);

            for (var row = 0; row < left.Rows; row++)
            {
                for (var column = 0; column < left.Columns; column++)
                {
                    matrix[row, column] = left[row, 0] * right[0, column] +
                                          left[row, 1] * right[1, column] +
                                          left[row, 2] * right[2, column] +
                                          left[row, 3] * right[3, column];
                }
            }

            return matrix;
        }

        public static Tuple operator *(Matrix matrix, Tuple tuple)
        {
            if (matrix.Rows != 4 || matrix.Columns != 4)
            {
                throw new ArithmeticException("This operation only supports 4x4 Matrices!");
            }

            var x = matrix[0, 0] * tuple.X +
                    matrix[0, 1] * tuple.Y +
                    matrix[0, 2] * tuple.Z +
                    matrix[0, 3] * tuple.W;

            var y = matrix[1, 0] * tuple.X +
                    matrix[1, 1] * tuple.Y +
                    matrix[1, 2] * tuple.Z +
                    matrix[1, 3] * tuple.W;

            var z = matrix[2, 0] * tuple.X +
                    matrix[2, 1] * tuple.Y +
                    matrix[2, 2] * tuple.Z +
                    matrix[2, 3] * tuple.W;

            var w = matrix[3, 0] * tuple.X +
                    matrix[3, 1] * tuple.Y +
                    matrix[3, 2] * tuple.Z +
                    matrix[3, 3] * tuple.W;

            return new Tuple(x, y, z, w);
        }

        public static Tuple operator *(Tuple tuple, Matrix matrix)
        {
            return matrix * tuple;
        }

        public static Matrix IdentityMatrix()
        {
            var matrix = new Matrix(4, 4);

            matrix[0, 0] = 1f;
            matrix[1, 1] = 1f;
            matrix[2, 2] = 1f;
            matrix[3, 3] = 1f;

            return matrix;
        }

        public static Matrix Transpose(Matrix matrix)
        {
            var transposedMatrix = new Matrix(matrix.Columns, matrix.Rows);

            for (var row = 0; row < matrix.Rows; row++)
            {
                for (var column = 0; column < matrix.Columns; column++)
                {
                    transposedMatrix[column, row] = matrix[row, column];
                }
            }

            return transposedMatrix;
        }

        public static float Determinant(Matrix matrix)
        {
            if (matrix.Rows == 2 && matrix.Columns == 2)
            {
                return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
            }

            float determinant = 0f;

            for (var column = 0; column < matrix.Columns; column++)
            {
                determinant += matrix[0, column] * Cofactor(matrix, 0, column);
            }

            return determinant;
        }

        public static Matrix Submatrix(Matrix matrix, int rowToRemove, int columnToRemove)
        {
            var subMatrix = new Matrix(matrix.Rows - 1, matrix.Columns - 1);
            var currentRow = 0;

            for (var row = 0; row < matrix.Rows; row++)
            {
                var currentColumn = 0;

                if (row == rowToRemove)
                {
                    continue;
                }

                for (var column = 0; column < matrix.Columns; column++)
                {
                    if (column == columnToRemove)
                    {
                        continue;
                    }

                    subMatrix[currentRow, currentColumn] = matrix[row, column];

                    currentColumn++;
                }

                currentRow++;
            }

            return subMatrix;
        }

        public static float Minor(Matrix matrix, int rowToRemove, int columnToRemove)
        {
            var subMatrix = Submatrix(matrix, rowToRemove, columnToRemove);
            var determinant = Determinant(subMatrix);
            return determinant;
        }

        public static float Cofactor(Matrix matrix, int rowToRemove, int columnToRemove)
        {
            var minor = Minor(matrix, rowToRemove, columnToRemove);

            return (rowToRemove + columnToRemove) % 2 == 0 ?
                minor : minor * -1;
        }

        public static Matrix Inverse(Matrix matrix)
        {
            if (!matrix.IsInvertible)
            {
                throw new ArithmeticException("This matrix is not invertible!");
            }

            var inversedMatrix = new Matrix(matrix.Rows, matrix.Columns);
            var determinant = Determinant(matrix);

            for (var row = 0; row < matrix.Rows; row++)
            {
                for (var column = 0; column < matrix.Columns; column++)
                {
                    var cofactor = Cofactor(matrix, row, column);

                    // note that "col, row" here, instead of "row, col", accomplishes the transpose operation!
                    inversedMatrix[column, row] = cofactor / determinant;
                }
            }

            return inversedMatrix;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder($"Matrix {Rows}x{Columns}{Environment.NewLine}");

            for (var row = 0; row < Rows; row++)
            {
                for (var column = 0; column < Columns; column++)
                {
                    var value = this[row, column];
                    var padding = value < 0 ? " " : "  ";

                    stringBuilder.Append($"|{padding}{value} ");
                }
                stringBuilder.Append($"|{Environment.NewLine}");
            }

            return stringBuilder.ToString();
        }
    }
}
