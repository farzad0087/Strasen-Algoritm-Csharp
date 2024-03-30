using System;

class Program
{
    static int[,] Multiply(int n, int[,] a, int[,] b)
    {
        if (n == 1)
        {
            int[,] c = new int[1, 1];
            c[0, 0] = a[0, 0] * b[0, 0];
            return c;
        }
        else
        {
            int[,] c = new int[n, n];
            int[,] a11 = new int[n / 2, n / 2];
            int[,] a12 = new int[n / 2, n / 2];
            int[,] a21 = new int[n / 2, n / 2];
            int[,] a22 = new int[n / 2, n / 2];
            int[,] b11 = new int[n / 2, n / 2];
            int[,] b12 = new int[n / 2, n / 2];
            int[,] b21 = new int[n / 2, n / 2];
            int[,] b22 = new int[n / 2, n / 2];

            Partition(n, a, a11, 0, 0);
            Partition(n, a, a12, 0, n / 2);
            Partition(n, a, a21, n / 2, 0);
            Partition(n, a, a22, n / 2, n / 2);
            Partition(n, b, b11, 0, 0);
            Partition(n, b, b12, 0, n / 2);
            Partition(n, b, b21, n / 2, 0);
            Partition(n, b, b22, n / 2, n / 2);

            int[,] m1 = Multiply(n / 2, Add(n / 2, a11, a22), Add(n / 2, b11, b22));
            int[,] m2 = Multiply(n / 2, Add(n / 2, a21, a22), b11);
            int[,] m3 = Multiply(n / 2, a11, Subtract(n / 2, b12, b22));
            int[,] m4 = Multiply(n / 2, a22, Subtract(n / 2, b21, b11));
            int[,] m5 = Multiply(n / 2, Add(n / 2, a11, a12), b22);
            int[,] m6 = Multiply(n / 2, Subtract(n / 2, a21, a11), Add(n / 2, b11, b12));
            int[,] m7 = Multiply(n / 2, Subtract(n / 2, a12, a22), Add(n / 2, b21, b22));

            int[,] c11 = Add(n / 2, Subtract(n / 2, Add(n / 2, m1, m4), m5), m7);
            int[,] c12 = Add(n / 2, m3, m5);
            int[,] c21 = Add(n / 2, m2, m4);
            int[,] c22 = Add(n / 2, Add(n / 2, Subtract(n / 2, m1, m2), m3), m6);

            Combine(n, c, c11, 0, 0);
            Combine(n, c, c12, 0, n / 2);
            Combine(n, c, c21, n / 2, 0);
            Combine(n, c, c22, n / 2, n / 2);

            return c;
        }
    }

    static void Partition(int n, int[,] a, int[,] b, int iB, int jB)
    {
        for (int i1 = 0, i2 = iB; i1 < b.GetLength(0); i1++, i2++)
        {
            for (int j1 = 0, j2 = jB; j1 < b.GetLength(1); j1++, j2++)
            {
                b[i1, j1] = a[i2, j2];
            }
        }
    }

    static void Combine(int n, int[,] c, int[,] a, int iA, int jA)
    {
        for (int i1 = 0, i2 = iA; i1 < a.GetLength(0); i1++, i2++)
        {
            for (int j1 = 0, j2 = jA; j1 < a.GetLength(1); j1++, j2++)
            {
                c[i2, j2] = a[i1, j1];
            }
        }
    }

    static int[,] Add(int n, int[,] a, int[,] b)
    {
        int[,] result = new int[n, n];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                result[i, j] = a[i, j] + b[i, j];
            }
        }
        return result;
    }

    static int[,] Subtract(int n, int[,] a, int[,] b)
    {
        int[,] result = new int[n, n];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                result[i, j] = a[i, j] - b[i, j];
            }
        }
        return result;
    }

    static void PrintMatrix(int[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write(matrix[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    static void Main()
    {
        int[,] a = { { 2, 1, 1, 2 }, { 1, 3, 2, 1 }, { 1, 2, 3, 1 }, { 2, 1, 1, 2 } };
        int[,] b = { { 0, 1, 1, 0 }, { 1, 0, 1, 1 }, { 1, 1, 0, 1 }, { 0, 1, 1, 0 } };

        int[,] result = Multiply(4, a, b);
        PrintMatrix(result);
    }
}