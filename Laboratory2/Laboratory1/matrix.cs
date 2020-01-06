using System;
using System.Diagnostics;
using System.Collections;

namespace Laboratory2
{
    public class Matrix
    {
        public double[,] matrix;
        //private double[,] _matrix;
        /*
        public double[,] Matrix
        {
            get { return _matrix; }
            set { _matrix = value; }
        }
        */

        private int rows;
        private int cols;

        public Matrix(int rowCount, int colCount)
        {
            rows = rowCount;
            cols = colCount;

            matrix = new double[rows, cols];
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    matrix[i, j] = 0;
                }
            }
        }


        public static double[] ToArray(Matrixs matrix)
        {
            double[] arr = new double[matrix.Rows() + matrix.Cols()]; 
            for (int i = 0; i < matrix.Rows(); i++)
            {
                for (int j = 0; j < matrix.Cols(); j++)
                {
                    arr[i] = matrix.Data()[i, j];
                }
            }
            return arr;
        }

        public static Matrixs FromArray(int [] array)
        {
            var m = new Matrixs(array.GetLength(0), 1);
            for(int i = 0; i < array.GetLength(0); i++)
            {
                m.Data()[i, 0] = array[i];
            }
            return m;
        }


        public double[,] NewMatrix(int rows, int cols)
        {
            matrix = new double[rows, rows];

            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    matrix[i, j] = 0;
                }

            }
            return matrix;
        }

        public double[,] Multiply(double n)
        {
            for (var i = 0; i < Rows(); i++)
            {
                for (var j = 0; j < Cols(); j++)
                {
                    this.matrix[i, j] *= n;
                }

            }
            return matrix;
        }

        public double[,] Multiply(double[,] matrix)
        {
            int m = this.matrix.GetLength(0);
            int n = this.matrix.GetLength(1);

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
                            c[i, j] += this.matrix[i, k] * matrix[k, j];
                        }
                    }

                }
                this.matrix = c;
                return c;
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
            else
            {
                throw new System.ArgumentException("Matrices dimensions do not match for multiply", "matrix");

            }
        }


        public static double[,] Multiply(Matrixs a, Matrixs b)
        {

            int m = a.Rows();
            int n = a.Cols();

            int p = b.Rows();
            int q = b.Cols();

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
                            c[i, j] += a.Data()[i, k] * b.Data()[k, j];
                        }
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
            for (var i = 0; i < Rows(); i++)
            {
                for (var j = 0; j < Cols(); j++)
                {
                    matrix[i, j] += n.Data()[i, j];
                }

            }
        }


        public void Add(double[,] n)
        {
            for (var i = 0; i < Rows(); i++)
            {
                for (var j = 0; j < Cols(); j++)
                {
                    matrix[i, j] += n[i, j];
                }

            }
        }

        public void Add(int n)
        {
            for (var i = 0; i < Rows(); i++)
            {
                for (var j = 0; j < Cols(); j++)
                {
                    matrix[i, j] += n;
                }
            }
        }

        public static double[,] Add(double[,] a, double[,] b)
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

        public static Matrixs Add(Matrixs a, Matrixs b)
        {
            if (SubtractableDimensions(a, b))
            { 
            for (var i = 0; i < a.Rows(); i++)
            {
                for (var j = 0; j < a.Cols(); j++)
                {
                    a.Data()[i, j] += b.Data()[i, j];
                }
            }
            return a;
            }
            else throw new System.ArgumentException("Matrices dimensions do not match for substraction");
        }

        private static bool SubtractableDimensions(Matrixs a, Matrixs b)
        {
            if (a.Rows() == b.Rows() && a.Cols() == b.Cols()) return true;
            else return false;
        }
        private static bool SubtractableDimensions(double[,] a, double[,] b)
        {
            if (Rows(a) == Rows(b) && Cols(a) == Cols(b)) return true;
            else return false;
        }

        /*
        public delegate double CalcSigmoid(double number);

        public static double AddIntN(double number)
        {
            return 1 / (1 + Math.Exp(-number));
        }
        */






        public void Randomize()
        {
            matrix = RandomizeLogic(Data());
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
                    if(r.NextDouble() > 0.5) matrix[i, j] = r.NextDouble();
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
                    double val = this.Data()[i, j];
                    this.Data()[i, j] = f(val);
                }
            }

        }

        public static void DisplayMatrix(Matrixs a)
        {
            int m = a.Rows();
            int n = a.Cols();

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(a.Data()[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        public void DisplayMatrix(double [,] a)
        {
            DisplayMatrixLogic(a);
        }

        public void DisplayMatrix()
        {
            DisplayMatrixLogic(Data());
        }

        private static void DisplayMatrixLogic(double[,] a)
        {
            int m = Rows(a);
            int n = Cols(a);

            Console.WriteLine();
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
        }

        public int Rows()
        {
            return Data().GetLength(0);
        }

        public int Cols()
        {
            return Data().GetLength(1);
        }


        public double[,] Data()
        {
            return matrix;
        }

        public static int Rows(double[,] matrix)
        {
            return matrix.GetLength(1);
        }


        public static int Cols(double[,] matrix)
        {
            return matrix.GetLength(1);
        }



    }
}
