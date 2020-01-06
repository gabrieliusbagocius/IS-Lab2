using System;
using System.Diagnostics;
using System.Collections;

namespace Laboratory2
{
    public class Matrixs
    {
        public double[,] data;

        public Matrixs()
        {

        }

        public Matrixs(int rows, int cols)
        {
            data = new double[rows, cols];

            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    data[i, j] = 0;
                }
            }
        }

        public Matrixs(double[,] matrix)
        {
            data = matrix;
        }


        public static double[] ToArray(Matrixs matrix)
        {
            double[] arr = new double[matrix.Rows() + matrix.Cols()];
            for (int i = 0; i < matrix.Rows(); i++)
            {
                for (int j = 0; j < matrix.Cols(); j++)
                {
                    arr[i] = matrix.data[i, j];
                }
            }
            return arr;
        }

        public static Matrixs FromArray(int[] array)
        {
            var m = new Matrixs(array.GetLength(0), 1);
            for (int i = 0; i < array.GetLength(0); i++)
            {
                m.data[i, 0] = array[i];
            }
            return m;
        }

        public static Matrixs FromArray(double[] array)
        {
            var m = new Matrixs(array.GetLength(0), 1);
            for (int i = 0; i < array.GetLength(0); i++)
            {
                m.data[i, 0] = array[i];
            }
            return m;
        }
        /*
        public static double[,] FromArray(int[] array, bool empty)
        {
            double[,] result = new double[array.GetLength(0), 1];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                result[i, 0] = array[i];
            }
            return result;

        }
        */



        public double[,] NewMatrix(int rows, int cols)
        {
            data = new double[rows, rows];

            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    data[i, j] = 0;
                }

            }
            return data;
        }

        public double[,] Multiply(double n)
        {
            for (var i = 0; i < Rows(); i++)
            {
                for (var j = 0; j < Cols(); j++)
                {
                    data[i, j] *= n;
                }

            }
            return data;
        }

        public void Multiply(double[,] matrix)
        {
            // Hadamard product
            int m = data.GetLength(0);
            int n = data.GetLength(1);

            int p = matrix.GetLength(0);
            int q = matrix.GetLength(1);

            if (n == p)
            {
                double[,] c = new double[m, q];
                for (var i = 0; i < m; i++)
                {
                    for (var j = 0; j < q; j++)
                    {
                        c[i, j] = 0;
                        for (int k = 0; k < n; k++)
                        {
                            //Console.WriteLine(k + " " + n);
                            c[i, j] += data[i, k] * matrix[k, j];
                            //Console.WriteLine(c[i, j]);
                        }
                    }

                }
                data = c;
            }
            else if (m == p && n == q) // Element wise multiplication
            {
                double[,] c = new double[m, q];
                for (var i = 0; i < m; i++)
                {
                    for (var j = 0; j < q; j++)
                    {
                        c[i, j] = data[i, j] * matrix[i, j];


                    }

                }
                data = c;

            }
            else
            {
                throw new System.ArgumentException("Matrices dimensions do not match for multiply", "matrix");

            }
        }

        public static double[,] Multiply(double[,] a, double[,] b)
        {
            int m = a.GetLength(0);
            int n = a.GetLength(1);

            int p = b.GetLength(0);
            int q = b.GetLength(1);


            if (n == p)
            {
                double[,] c = new double[m, q];
                for (var i = 0; i < m; i++)
                {
                    for (var j = 0; j < q; j++)
                    {
                        c[i, j] = 0;
                        for (int k = 0; k < n; k++)
                        {
                            c[i, j] += a[i, k] * b[k, j];
                        }
                    }

                }
                return c;
            }
            else if (m == p && n == q) // Element wise multiplication
            {
                double[,] c = new double[m, q];
                for (var i = 0; i < m; i++)
                {
                    for (var j = 0; j < q; j++)
                    {
                        c[i, j] = a[i, j] * b[i, j];


                    }
                }
                return c;

            }
            else
            {
                throw new System.ArgumentException("Matrices dimensions do not match for multiply", "matrix");

            }
        }


        public static Matrixs Multiply(Matrixs a, Matrixs b)
        {

            int m = a.Rows();
            int n = a.Cols();

            int p = b.Rows();
            int q = b.Cols();

            if (n == p)
            {
                Matrixs c = new Matrixs(m, q);
                for (var i = 0; i < m; i++)
                {
                    for (var j = 0; j < q; j++)
                    {
                        c.data[i, j] = 0;
                        for (var k = 0; k < n; k++)
                        {
                            c.data[i, j] += a.data[i, k] * b.data[k, j];
                        }
                    }

                }
                return c;
            }
            else if (m == p && n == q) // Element wise multiplication
            {
                Matrixs c = new Matrixs(m, q);
                for (var i = 0; i < m; i++)
                {
                    for (var j = 0; j < q; j++)
                    {
                        c.data[i, j] = a.data[i, j] * b.data[i, j];


                    }
                }
                return c;

            }
            else
            {
                throw new System.ArgumentException("Matrices dimensions do not match for multiply", "matrix");

            }
        }

        public void Add(Matrixs n)
        {
            if (SubtractableDimensions(data, n.data))
            {
                for (var i = 0; i < Rows(); i++)
                {
                    for (var j = 0; j < Cols(); j++)
                    {
                        data[i, j] += n.data[i, j];
                    }

                }
            }
            else throw new System.ArgumentException("Matrices dimensions do not match for substraction");
        }


        public void Add(double[,] n)
        {
            if (SubtractableDimensions(data, n))
            {
                for (var i = 0; i < Rows(); i++)
                {
                    for (var j = 0; j < Cols(); j++)
                    {
                        data[i, j] += n[i, j];
                    }

                }

            }
            else throw new System.ArgumentException("Matrices dimensions do not match for substraction");
        }

        public void Add(int n)
        {
            for (var i = 0; i < Rows(); i++)
            {
                for (var j = 0; j < Cols(); j++)
                {
                    data[i, j] += n;
                }
            }
        }

        public static double[,] Add(double[,] a, double[,] b)
        {
            if (SubtractableDimensions(a, b))
            {
                for (var i = 0; i < a.GetLength(0); i++)
                {
                    for (var j = 0; j < a.GetLength(1); j++)
                    {
                        a[i, j] += b[i, j];
                    }
                }
                return a;
            }
            else throw new System.ArgumentException("Matrices dimensions do not match for substraction");
        }

        public static Matrixs Add(Matrixs a, Matrixs b)
        {
            if (SubtractableDimensions(a, b))
            {
                for (var i = 0; i < a.Rows(); i++)
                {
                    for (var j = 0; j < a.Cols(); j++)
                    {
                        a.data[i, j] += b.data[i, j];
                    }
                }
                return a;
            }
            else throw new System.ArgumentException("Matrices dimensions do not match for substraction");
        }

        public static Matrixs Subtract(Matrixs a, Matrixs b)
        {
            if (SubtractableDimensions(a, b))
            {
                var result = new Matrixs(a.Rows(), b.Cols());
                if (b.Rows() >= 1 && b.Cols() >= 1)
                {
                    for (var i = 0; i < a.Rows(); i++)
                    {
                        for (var j = 0; j < a.Cols(); j++)
                        {
                            result.data[i, j] = a.data[i, j] - b.data[i, j];
                        }
                    }
                    return result;
                }
                else
                {
                    for (var i = 0; i < a.Rows(); i++)
                    {
                        for (var j = 0; j < a.Cols(); j++)
                        {
                            result.data[i, j] = a.data[i, j] - b.data[0, 0];
                        }
                    }
                    return result;
                }
            }
            else throw new System.ArgumentException("Matrices dimensions do not match for substraction");
        }

        private static bool SubtractableDimensions(Matrixs a, Matrixs b)
        {
            if (a.Rows() == b.Rows() && a.Cols() == b.Cols() || b.Rows() == 0 && b.Cols() == 0) return true;
            else return false;
        }
        private static bool SubtractableDimensions(double[,] a, double[,] b)
        {
            if (Rows(a) == Rows(b) && Cols(a) == Cols(b)) return true;
            else return false;
        }


        public static Matrixs Transpose(Matrixs matrix)
        {
            var result = new Matrixs(matrix.Cols(), matrix.Rows());
            for (var i = 0; i < matrix.Rows(); i++)
            {
                for (var j = 0; j < matrix.Cols(); j++)
                {
                    result.data[j, i] += matrix.data[i, j];
                }
            }
            return result;
        }


        public void Randomize()
        {
            RandomizeLogic(data);
        }

        public static double[,] Randomize(double[,] matrix)
        {
            matrix = RandomizeLogic(matrix);
            return matrix;
        }



        private static double[,] RandomizeLogic(double[,] matrix)
        {
            Manager manager = new Manager();
            Random r = manager.random;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (r.NextDouble() > 0.5) matrix[i, j] = r.NextDouble();
                    else matrix[i, j] = r.NextDouble() * (-1);
                }
            }
            return matrix;
        }



        public void Map(Func<double, double> f)
        {
            //Console.WriteLine("Rows(): " + Rows() + " Cols(): " + Cols());
            for (int i = 0; i < Rows(); i++)
            {
                for (int j = 0; j < Cols(); j++)
                {
                    double val = data[i, j];
                    data[i, j] = f(val);
                }
            }
        }

        public static Matrixs Map(Matrixs matrix, Func<double, double> f)
        {
            var result = new Matrixs(matrix.Rows(), matrix.Cols());
            //Console.WriteLine("Rows(): " + Rows() + " Cols(): " + Cols());
            for (int i = 0; i < matrix.Rows(); i++)
            {
                for (int j = 0; j < matrix.Cols(); j++)
                {
                    double val = matrix.data[i, j];
                    result.data[i, j] = f(val);
                }
            }
            return result;
        }

        public static void DisplayMatrix(Matrixs a)
        {
            DisplayMatrixLogic(a.data);
        }

        public static void DisplayMatrix(int[] a)
        {
            var b = FromArray(a);
            DisplayMatrixLogic(b.data);
        }

        public static void DisplayMatrix(double[,] a)
        {
            DisplayMatrixLogic(a);
        }

        public void DisplayMatrix()
        {
            DisplayMatrixLogic(data);
        }

        private static void DisplayMatrixLogic(double[,] a)
        {
            int m = Rows(a);
            int n = Cols(a);

            Console.Write("Rows: " + m);
            Console.WriteLine("  Cols: " + n);

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(a[i, j] + " ");
                }
                Console.WriteLine();
            }


            Console.WriteLine();
        }

        public int Rows()
        {
            return data.GetLength(0);
        }

        public int Cols()
        {
            return data.GetLength(1);
        }


        public static int Rows(double[,] matrix)
        {
            return matrix.GetLength(0);
        }


        public static int Cols(double[,] matrix)
        {
            return matrix.GetLength(1);
        }

    }
}
