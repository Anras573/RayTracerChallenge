using System;
using System.Linq;
using System.Text;

namespace RayTracerChallenge.Core
{
    public class Matrix : IEquatable<Matrix>
    {
        public int Rows { get; }
        public int Columns { get; }

        public bool IsInvertible => Determinant() != 0;

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

        private int CalculateIndex(int row, int column) => (row * Columns) + column;

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

        public static Tuple operator *(Tuple tuple, Matrix matrix) => matrix * tuple;

        public static Point operator *(Matrix matrix, Point point)
        {
            if (matrix.Rows != 4 || matrix.Columns != 4)
            {
                throw new ArithmeticException("This operation only supports 4x4 Matrices!");
            }

            var x = matrix[0, 0] * point.X +
                   matrix[0, 1] * point.Y +
                   matrix[0, 2] * point.Z +
                   matrix[0, 3] * point.W;

            var y = matrix[1, 0] * point.X +
                    matrix[1, 1] * point.Y +
                    matrix[1, 2] * point.Z +
                    matrix[1, 3] * point.W;

            var z = matrix[2, 0] * point.X +
                    matrix[2, 1] * point.Y +
                    matrix[2, 2] * point.Z +
                    matrix[2, 3] * point.W;

            return new Point(x, y, z);
        }

        public static Point operator *(Point point, Matrix matrix) => matrix * point;

        public static Vector operator *(Matrix matrix, Vector vector)
        {
            if (matrix.Rows != 4 || matrix.Columns != 4)
            {
                throw new ArithmeticException("This operation only supports 4x4 Matrices!");
            }

            var x = matrix[0, 0] * vector.X +
                   matrix[0, 1] * vector.Y +
                   matrix[0, 2] * vector.Z +
                   matrix[0, 3] * vector.W;

            var y = matrix[1, 0] * vector.X +
                    matrix[1, 1] * vector.Y +
                    matrix[1, 2] * vector.Z +
                    matrix[1, 3] * vector.W;

            var z = matrix[2, 0] * vector.X +
                    matrix[2, 1] * vector.Y +
                    matrix[2, 2] * vector.Z +
                    matrix[2, 3] * vector.W;

            return new Vector(x, y, z);
        }

        public static Vector operator *(Vector vector, Matrix matrix) => matrix * vector;

        public static Matrix IdentityMatrix()
        {
            var matrix = new Matrix(4, 4);

            matrix[0, 0] = 1f;
            matrix[1, 1] = 1f;
            matrix[2, 2] = 1f;
            matrix[3, 3] = 1f;

            return matrix;
        }

        public Matrix Transpose()
        {
            var transposedMatrix = new Matrix(Columns, Rows);

            for (var row = 0; row < Rows; row++)
            {
                for (var column = 0; column < Columns; column++)
                {
                    transposedMatrix[column, row] = this[row, column];
                }
            }

            return transposedMatrix;
        }

        public float Determinant()
        {
            if (Rows == 2 && Columns == 2)
            {
                return this[0, 0] * this[1, 1] - this[0, 1] * this[1, 0];
            }

            float determinant = 0f;

            for (var column = 0; column < Columns; column++)
            {
                determinant += this[0, column] * Cofactor(0, column);
            }

            return determinant;
        }

        public Matrix Submatrix(int rowToRemove, int columnToRemove)
        {
            var subMatrix = new Matrix(Rows - 1, Columns - 1);
            var currentRow = 0;
            
            for (var row = 0; row < Rows; row++)
            {
                var currentColumn = 0;

                if (row == rowToRemove) continue;

                for (var column = 0; column < Columns; column++)
                {
                    if (column == columnToRemove) continue;

                    subMatrix[currentRow, currentColumn] = this[row, column];

                    currentColumn++;
                }

                currentRow++;
            }

            return subMatrix;
        }

        public float Minor(int rowToRemove, int columnToRemove)
        {
            var subMatrix = Submatrix(rowToRemove, columnToRemove);
            var determinant = subMatrix.Determinant();
            return determinant;
        }

        public float Cofactor(int rowToRemove, int columnToRemove)
        {
            var minor = Minor(rowToRemove, columnToRemove);

            return (rowToRemove + columnToRemove) % 2 == 0
                ? minor
                : minor * -1;
        }

        public Matrix Inverse()
        {
            if (!IsInvertible)
            {
                throw new ArithmeticException("This matrix is not invertible!");
            }

            var inversedMatrix = new Matrix(Rows, Columns);
            var determinant = Determinant();

            for (var row = 0; row < Rows; row++)
            {
                for (var column = 0; column < Columns; column++)
                {
                    var cofactor = Cofactor(row, column);

                    // note that "col, row" here, instead of "row, col", accomplishes the transpose operation!
                    inversedMatrix[column, row] = cofactor / determinant;
                }
            }

            return inversedMatrix;
        }

        public static Matrix Translate(float x, float y, float z)
        {
            var matrix = IdentityMatrix();

            matrix[0, 3] = x;
            matrix[1, 3] = y;
            matrix[2, 3] = z;

            return matrix;
        }

        public static Matrix Scale(float x, float y, float z)
        {
            var matrix = IdentityMatrix();

            matrix[0, 0] = x;
            matrix[1, 1] = y;
            matrix[2, 2] = z;

            return matrix;
        }

        public static Matrix RotateX(double r)
        {
            var matrix = IdentityMatrix();

            var cos = Math.Cos(r);
            var sin = Math.Sin(r);

            matrix[1, 1] = (float)cos;
            matrix[1, 2] = (float)-sin;
            matrix[2, 1] = (float)sin;
            matrix[2, 2] = (float)cos;

            return matrix;
        }

        public static Matrix RotateY(float r)
        {
            var matrix = IdentityMatrix();

            var cos = MathF.Cos(r);
            var sin = MathF.Sin(r);

            matrix[0, 0] = cos;
            matrix[0, 2] = sin;
            matrix[2, 0] = -sin;
            matrix[2, 2] = cos;

            return matrix;
        }

        public static Matrix RotateZ(float r)
        {
            var matrix = IdentityMatrix();

            var cos = MathF.Cos(r);
            var sin = MathF.Sin(r);

            matrix[0, 0] = cos;
            matrix[0, 1] = -sin;
            matrix[1, 0] = sin;
            matrix[1, 1] = cos;

            return matrix;
        }

        public static Matrix Shear(float xy, float xz, float yx, float yz, float zx, float zy)
        {
            var matrix = IdentityMatrix();

            matrix[0, 1] = xy;
            matrix[0, 2] = xz;
            matrix[1, 0] = yx;
            matrix[1, 2] = yz;
            matrix[2, 0] = zx;
            matrix[2, 1] = zy;

            return matrix;
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

        public override bool Equals(object obj)
        {
            if (!(obj is Matrix other))
            {
                return false;
            }

            return Equals(other);
        }

        public override int GetHashCode()
        {
            return Matrices.Sum(m => m.GetHashCode());
        }

        public static Matrix ViewTransformation(Point from, Point to, Vector up)
        {
            var forward = (to - from).Normalize();
            var upn = up.Normalize();
            var left = forward.Cross(upn);
            var trueUp = left.Cross(forward);

            var orientation = new Matrix(4, 4)
            {
                [0, 0] = left.X,
                [0, 1] = left.Y,
                [0, 2] = left.Z,
                [0, 3] = 0f,
                [1, 0] = trueUp.X,
                [1, 1] = trueUp.Y,
                [1, 2] = trueUp.Z,
                [1, 3] = 0f,
                [2, 0] = -forward.X,
                [2, 1] = -forward.Y,
                [2, 2] = -forward.Z,
                [2, 3] = 0f,
                [3, 0] = 0f,
                [3, 1] = 0f,
                [3, 2] = 0f,
                [3, 3] = 1f
            };

            return orientation * Translate(-from.X, -from.Y, -from.Z);
        }
    }
}
